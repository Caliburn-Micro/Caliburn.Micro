
using Windows.UI.Core;

namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using Windows.Foundation;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

#if WP81
    using Windows.Phone.UI.Input;
#endif

    /// <summary>
    ///   Implemented by services that provide (<see cref="System.Uri" /> based) navigation.
    /// </summary>
    public interface INavigationService {
        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        event NavigatedEventHandler Navigated;

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        event NavigatingCancelEventHandler Navigating;

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        event NavigationFailedEventHandler NavigationFailed;

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        event NavigationStoppedEventHandler NavigationStopped;

        /// <summary>
        /// Gets or sets the data type of the current content, or the content that should be navigated to.
        /// </summary>
        Type SourcePageType { get; set; }

        /// <summary>
        /// Gets the data type of the content that is currently displayed.
        /// </summary>
        Type CurrentSourcePageType { get; }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="sourcePageType"> The <see cref="System.Type" /> to navigate to. </param>
        /// <returns> Whether or not navigation succeeded. </returns>
        bool Navigate(Type sourcePageType);

        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="sourcePageType"> The <see cref="System.Type" /> to navigate to. </param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <returns> Whether or not navigation succeeded. </returns>
        bool Navigate(Type sourcePageType, object parameter);

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        void GoForward();

        /// <summary>
        ///   Navigates back.
        /// </summary>
        void GoBack();

#if WinRT81
        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.
        /// </summary>
        IList<PageStackEntry> BackStack { get; }

        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.
        /// </summary>
        IList<PageStackEntry> ForwardStack { get; }
#endif

#if WP81
        /// <summary>
        /// Occurs when the user presses the hardware Back button.
        /// </summary>
        event EventHandler<BackPressedEventArgs> BackRequested;
#elif WINDOWS_UWP
        /// <summary>
        /// Occurs when the user requests a back navigation via hardware back button or gesture or voice.
        /// </summary>
        event EventHandler<BackRequestedEventArgs> BackRequested;
#endif

        /// <summary>
        /// Stores the frame navigation state in local settings if it can.
        /// </summary>
        /// <returns>Whether the suspension was sucessful</returns>
        bool SuspendState();

        /// <summary>
        /// Tries to restore the frame navigation state from local settings.
        /// </summary>
        /// <returns>Whether the restoration of successful.</returns>
        bool ResumeState();
    }

    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class FrameAdapter : INavigationService {

        private static readonly ILog Log = LogManager.GetLog(typeof(FrameAdapter));
        private const string FrameStateKey = "FrameState";
        private const string ParameterKey = "ParameterKey";

        private readonly Frame frame;
        private readonly bool treatViewAsLoaded;
        private event NavigatingCancelEventHandler ExternalNavigatingHandler = delegate { };

        /// <summary>
        /// Creates an instance of <see cref="FrameAdapter" />.
        /// </summary>
        /// <param name="frame">The frame to represent as a <see cref="INavigationService" />.</param>
        /// <param name="treatViewAsLoaded">
        /// Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called.
        /// This is necessary when using the TransitionFrame.
        /// </param>
        public FrameAdapter(Frame frame, bool treatViewAsLoaded = false) {
            this.frame = frame;
            this.treatViewAsLoaded = treatViewAsLoaded;

            this.frame.Navigating += OnNavigating;
            this.frame.Navigated += OnNavigated;

#if WP81
            // This could be a potential bug if we're registering the navigation service
            // after the frame is loaded. Unlikely scenario given normal WP8.1 app structure

            this.frame.Loaded += (sender, args) => { HardwareButtons.BackPressed += OnBackRequested; };
            this.frame.Unloaded += (sender, args) => { HardwareButtons.BackPressed -= OnBackRequested; };
#elif WINDOWS_UWP

            // This could leak memory if we're creating and destorying navigation services regularly.
            // Another unlikely scenario though

            var navigationManager = SystemNavigationManager.GetForCurrentView();

            navigationManager.BackRequested += OnBackRequested;
#endif
        }

        /// <summary>
        /// The parameter to the current view
        /// </summary>
        protected object CurrentParameter
        {
            get; set;
        }

        /// <summary>
        ///   Occurs before navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual void OnNavigating(object sender, NavigatingCancelEventArgs e) {
            ExternalNavigatingHandler(sender, e);

            if (e.Cancel)
                return;

            var view = frame.Content as FrameworkElement;

            if (view == null)
                return;

            var guard = view.DataContext as IGuardClose;

            if (guard != null) {
                var shouldCancel = false;
                var runningAsync = true;
                guard.CanClose(result => { runningAsync = false; shouldCancel = !result; });
                if (runningAsync)
                    throw new NotSupportedException("Async CanClose is not supported.");

                if (shouldCancel) {
                    e.Cancel = true;
                    return;
                }
            }

            var deactivator = view.DataContext as IDeactivate;

            if (deactivator != null) {
                deactivator.Deactivate(CanCloseOnNavigating(sender, e));
            }
        }

        /// <summary>
        ///   Occurs after navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual void OnNavigated(object sender, NavigationEventArgs e) {

            if (e.Content == null)
                return;

            CurrentParameter = e.Parameter;

            var view = e.Content as Page;

            if (view == null) {
                throw new ArgumentException("View '" + e.Content.GetType().FullName +
                                            "' should inherit from Page or one of its descendents.");
            }

            BindViewModel(view);
        }

        /// <summary>
        /// Binds the view model.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        protected virtual void BindViewModel(DependencyObject view, object viewModel = null)
        {
            ViewLocator.InitializeComponent(view);

            viewModel = viewModel ?? ViewModelLocator.LocateForView(view);

            if (viewModel == null)
                return;

            if (treatViewAsLoaded)
            {
                view.SetValue(View.IsLoadedProperty, true);
            }

            TryInjectParameters(viewModel, CurrentParameter);
            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if (activator != null)
            {
                activator.Activate();
            }
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter) {
            var viewModelType = viewModel.GetType();

            var stringParameter = parameter as string;
            var dictionaryParameter = parameter as IDictionary<string, object>;

            if (stringParameter != null && stringParameter.StartsWith("caliburn://")) {
                var uri = new Uri(stringParameter);

                if (!String.IsNullOrEmpty(uri.Query)) {
                    var decorder = new WwwFormUrlDecoder(uri.Query);

                    foreach (var pair in decorder) {
                        var property = viewModelType.GetPropertyCaseInsensitive(pair.Name);

                        if (property == null) {
                            continue;
                        }

                        property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, pair.Value, null));
                    }
                }
            }
            else if (dictionaryParameter != null) {
                foreach (var pair in dictionaryParameter) {
                    var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                    if (property == null)
                    {
                        continue;
                    }

                    property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, pair.Value, null));
                }
            }
            else {
                var property = viewModelType.GetPropertyCaseInsensitive("Parameter");

                if (property == null)
                    return;

                property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null));
            }
        }

        /// <summary>
        /// Called to check whether or not to close current instance on navigating.
        /// </summary>
        /// <param name="sender"> The event sender from OnNavigating event. </param>
        /// <param name="e"> The event args from OnNavigating event. </param>
        protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e) {
            return false;
        }

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        public event NavigatedEventHandler Navigated {
            add { frame.Navigated += value; }
            remove { frame.Navigated -= value; }
        }

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        public event NavigatingCancelEventHandler Navigating {
            add { ExternalNavigatingHandler += value; }
            remove { ExternalNavigatingHandler -= value; }
        }

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        public event NavigationFailedEventHandler NavigationFailed {
            add { frame.NavigationFailed += value; }
            remove { frame.NavigationFailed -= value; }
        }

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        public event NavigationStoppedEventHandler NavigationStopped {
            add { frame.NavigationStopped += value; }
            remove { frame.NavigationStopped -= value; }
        }

        /// <summary>
        /// Gets or sets the data type of the current content, or the content that should be navigated to.
        /// </summary>
        public virtual Type SourcePageType {
            get { return frame.SourcePageType; }
            set { frame.SourcePageType = value; }
        }

        /// <summary>
        /// Gets the data type of the content that is currently displayed.
        /// </summary>
        public virtual Type CurrentSourcePageType {
            get { return frame.CurrentSourcePageType; }
        }

        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="sourcePageType"> The <see cref="System.Type" /> to navigate to. </param>
        /// <returns> Whether or not navigation succeeded. </returns>
        public virtual bool Navigate(Type sourcePageType) {
            return frame.Navigate(sourcePageType);
        }

        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="sourcePageType"> The <see cref="System.Type" /> to navigate to. </param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <returns> Whether or not navigation succeeded. </returns>
        public virtual bool Navigate(Type sourcePageType, object parameter) {
            return frame.Navigate(sourcePageType, parameter);
        }

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        public virtual void GoForward() {
            frame.GoForward();
        }

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public virtual void GoBack() {
            frame.GoBack();
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        public virtual bool CanGoForward {
            get { return frame.CanGoForward; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        public virtual bool CanGoBack {
            get { return frame.CanGoBack; }
        }

#if WinRT81
        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.
        /// </summary>
        public virtual IList<PageStackEntry> BackStack {
            get { return frame.BackStack; }
        }

        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.
        /// </summary>
        public virtual IList<PageStackEntry> ForwardStack {
            get { return frame.ForwardStack; }
        }
#endif

        /// <summary>
        /// Stores the frame navigation state in local settings if it can.
        /// </summary>
        /// <returns>Whether the suspension was sucessful</returns>
        public virtual bool SuspendState() {
            try {
                var container = GetSettingsContainer();

                container.Values[FrameStateKey] = frame.GetNavigationState();
                container.Values[ParameterKey] = CurrentParameter;

                return true;
            }
            catch (Exception ex) {
                Log.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Tries to restore the frame navigation state from local settings.
        /// </summary>
        /// <returns>Whether the restoration of successful.</returns>
        public virtual bool ResumeState() {
            var container = GetSettingsContainer();

            if (!container.Values.ContainsKey(FrameStateKey))
                return false;

            var frameState = (string) container.Values[FrameStateKey];

            CurrentParameter = container.Values.ContainsKey(ParameterKey) ?
                container.Values[ParameterKey] :
                null;

            if (String.IsNullOrEmpty(frameState))
                return false;

            frame.SetNavigationState(frameState);

            var view = frame.Content as Page;
            if (view == null) {
                return false;
            }

            BindViewModel(view);

            if (Window.Current.Content == null)
                Window.Current.Content = frame;

            Window.Current.Activate();

            return true;
        }

#if WP81
        /// <summary>
        /// Occurs when the user presses the hardware Back button.
        /// </summary>
        public event EventHandler<BackPressedEventArgs> BackRequested = delegate { };

        /// <summary>
        ///  Occurs when the user presses the hardware Back button. Allows the handlers to cancel the default behavior.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnBackRequested(BackPressedEventArgs e) {
            BackRequested(this, e);
        }

        private void OnBackRequested(object sender, BackPressedEventArgs e) {
            OnBackRequested(e);

            if (e.Handled)
                return;

            if (CanGoBack) {
                e.Handled = true;
                GoBack();
            }
        }
#elif WINDOWS_UWP
        /// <summary>
        /// Occurs when the user presses the hardware Back button.
        /// </summary>
        public event EventHandler<BackRequestedEventArgs> BackRequested = delegate { };

        /// <summary>
        ///  Occurs when the user presses the hardware Back button. Allows the handlers to cancel the default behavior.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnBackRequested(BackRequestedEventArgs e)
        {
            BackRequested(this, e);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            OnBackRequested(e);

            if (e.Handled)
                return;

            if (CanGoBack)
            {
                e.Handled = true;
                GoBack();
            }
        }

#endif

        private static ApplicationDataContainer GetSettingsContainer() {
            return ApplicationData.Current.LocalSettings.CreateContainer("Caliburn.Micro",
                ApplicationDataCreateDisposition.Always);
        }
    }
}
