﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Caliburn.Micro {
    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class FrameAdapter : INavigationService, IDisposable {
        private const string FrameStateKey = "FrameState";
        private const string ParameterKey = "ParameterKey";

        private static readonly ILog Log = LogManager.GetLog(typeof(FrameAdapter));

        private readonly Frame frame;
        private readonly bool treatViewAsLoaded;
#if WINDOWS_UWP
        private readonly SystemNavigationManager navigationManager;
#endif

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameAdapter"/> class.
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

#if WINDOWS_UWP

            // This could leak memory if we're creating and destorying navigation services regularly.
            // Another unlikely scenario though
            navigationManager = SystemNavigationManager.GetForCurrentView();

            navigationManager.BackRequested += OnBackRequested;
#endif
        }

        /// <summary>
        ///   Raised after navigation.
        /// </summary>
        public event NavigatedEventHandler Navigated {
            add => frame.Navigated += value;
            remove => frame.Navigated -= value;
        }

        /// <summary>
        ///   Raised prior to navigation.
        /// </summary>
        public event NavigatingCancelEventHandler Navigating {
            add => ExternalNavigatingHandler += value;
            remove => ExternalNavigatingHandler -= value;
        }

        /// <summary>
        ///   Raised when navigation fails.
        /// </summary>
        public event NavigationFailedEventHandler NavigationFailed {
            add => frame.NavigationFailed += value;
            remove => frame.NavigationFailed -= value;
        }

        /// <summary>
        ///   Raised when navigation is stopped.
        /// </summary>
        public event NavigationStoppedEventHandler NavigationStopped {
            add => frame.NavigationStopped += value;
            remove => frame.NavigationStopped -= value;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Occurs when the user presses the hardware Back button.
        /// </summary>
        public event EventHandler<BackRequestedEventArgs> BackRequested = (sender, e) => { };
#endif

        private event NavigatingCancelEventHandler ExternalNavigatingHandler = (sender, e) => { };

        /// <summary>
        /// Gets the data type of the content that is currently displayed.
        /// </summary>
        public virtual Type CurrentSourcePageType
            => frame.CurrentSourcePageType;

        /// <summary>
        /// Gets a value indicating whether the navigator can navigate forward.
        /// </summary>
        public virtual bool CanGoForward
            => frame.CanGoForward;

        /// <summary>
        /// Gets a value indicating whether the navigator can navigate back.
        /// </summary>
        public virtual bool CanGoBack
            => frame.CanGoBack;

        /// <summary>
        /// Gets or sets the data type of the current content, or the content that should be navigated to.
        /// </summary>
        public virtual Type SourcePageType {
            get => frame.SourcePageType;
            set => frame.SourcePageType = value;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the backward navigation history of the Frame.
        /// </summary>
        public virtual IList<PageStackEntry> BackStack
            => frame.BackStack;

        /// <summary>
        /// Gets a collection of PageStackEntry instances representing the forward navigation history of the Frame.
        /// </summary>
        public virtual IList<PageStackEntry> ForwardStack
            => frame.ForwardStack;
#endif

        /// <summary>
        /// Gets or sets the parameter to the current view.
        /// </summary>
        protected object CurrentParameter { get; set; }

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        public virtual void GoForward()
            => frame.GoForward();

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public virtual void GoBack()
            => frame.GoBack();

        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="sourcePageType"> The <see cref="System.Type" /> to navigate to. </param>
        /// <returns> Whether or not navigation succeeded. </returns>
        public virtual bool Navigate(Type sourcePageType)
            => frame.Navigate(sourcePageType);

        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="sourcePageType"> The <see cref="System.Type" /> to navigate to. </param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <returns> Whether or not navigation succeeded. </returns>
        public virtual bool Navigate(Type sourcePageType, object parameter)
            => frame.Navigate(sourcePageType, parameter);

        /// <summary>
        /// Stores the frame navigation state in local settings if it can.
        /// </summary>
        /// <returns>Whether the suspension was sucessful.</returns>
        public virtual bool SuspendState() {
            try {
                ApplicationDataContainer container = GetSettingsContainer();

                container.Values[FrameStateKey] = frame.GetNavigationState();
                container.Values[ParameterKey] = CurrentParameter;

                return true;
            } catch (Exception ex) {
                Log.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Tries to restore the frame navigation state from local settings.
        /// </summary>
        /// <returns>Whether the restoration of successful.</returns>
        public virtual async Task<bool> ResumeStateAsync() {
            ApplicationDataContainer container = GetSettingsContainer();

            if (!container.Values.ContainsKey(FrameStateKey)) {
                return false;
            }

            string frameState = (string)container.Values[FrameStateKey];

            CurrentParameter = container.Values.TryGetValue(ParameterKey, out object value)
                ? value
                : null;

            if (string.IsNullOrEmpty(frameState)) {
                return false;
            }

            frame.SetNavigationState(frameState);

            if (frame.Content is not Page view) {
                return false;
            }

            await BindViewModel(view);

            if (Window.Current.Content == null) {
                Window.Current.Content = frame;
            }

            Window.Current.Activate();

            return true;
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Occurs before navigation.
        /// </summary>
        /// <param name="sender"> The event sender.</param>
        /// <param name="e"> The event args.</param>
        protected virtual async void OnNavigating(object sender, NavigatingCancelEventArgs e) {
            ExternalNavigatingHandler(sender, e);

            if (e.Cancel) {
                return;
            }

            if (frame.Content is not FrameworkElement view) {
                return;
            }

            if (view.DataContext is IGuardClose guard) {
                bool canClose = await guard.CanCloseAsync(CancellationToken.None);

                if (!canClose) {
                    e.Cancel = true;
                    return;
                }
            }

            if (view.DataContext is IDeactivate deactivator) {
                await deactivator.DeactivateAsync(CanCloseOnNavigating(sender, e), CancellationToken.None);
            }
        }

        /// <summary>
        ///   Occurs after navigation.
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual async void OnNavigated(object sender, NavigationEventArgs e) {
            if (e.Content == null) {
                return;
            }

            CurrentParameter = e.Parameter;

            if (e.Content is not Page view) {
                throw new ArgumentException("View '" + e.Content.GetType().FullName +
                                            "' should inherit from Page or one of its descendents.");
            }

            await BindViewModel(view);
        }

        /// <summary>
        /// Binds the view model.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        protected virtual async Task BindViewModel(DependencyObject view, object viewModel = null) {
            ViewLocator.InitializeComponent(view);
            viewModel ??= ViewModelLocator.LocateForView(view);
            if (viewModel == null) {
                return;
            }

            if (treatViewAsLoaded) {
                view.SetValue(View.IsLoadedProperty, true);
            }

            TryInjectParameters(viewModel, CurrentParameter);
            ViewModelBinder.Bind(viewModel, view, null);
            if (viewModel is IActivate activator) {
                await activator.ActivateAsync();
            }
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter) {
            Type viewModelType = viewModel.GetType();
            if (parameter is string stringParameter && stringParameter.StartsWith("caliburn://", StringComparison.OrdinalIgnoreCase)) {
                var uri = new Uri(stringParameter);
                if (string.IsNullOrEmpty(uri.Query)) {
                    return;
                }

                var decorder = new WwwFormUrlDecoder(uri.Query);
                foreach (IWwwFormUrlDecoderEntry pair in decorder) {
                    System.Reflection.PropertyInfo propertyLocal = viewModelType.GetPropertyCaseInsensitive(pair.Name);
                    if (propertyLocal == null) {
                        continue;
                    }

                    propertyLocal.SetValue(viewModel, MessageBinder.CoerceValue(propertyLocal.PropertyType, pair.Value, null));
                }

                return;
            }

            if (parameter is IDictionary<string, object> dictionaryParameter) {
                foreach (KeyValuePair<string, object> pair in dictionaryParameter) {
                    System.Reflection.PropertyInfo prop = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                    if (prop == null) {
                        continue;
                    }

                    prop.SetValue(viewModel, MessageBinder.CoerceValue(prop.PropertyType, pair.Value, null));
                }

                return;
            }

            System.Reflection.PropertyInfo property = viewModelType.GetPropertyCaseInsensitive("Parameter");
            if (property == null) {
                return;
            }

            property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null));
        }

        /// <summary>
        /// Called to check whether or not to close current instance on navigating.
        /// </summary>
        /// <param name="sender"> The event sender from OnNavigating event. </param>
        /// <param name="e"> The event args from OnNavigating event. </param>
        protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e)
            => false;

        /// <summary>
        /// Perform Dispose.
        /// </summary>
        /// <param name="disposing">Dispose managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (_isDisposed) {
                return;
            }

            if (disposing) {
                frame.Navigating -= OnNavigating;
                frame.Navigated -= OnNavigated;
#if WINDOWS_UWP
                navigationManager.BackRequested -= OnBackRequested;
#endif
            }

            _isDisposed = true;
        }

#if WINDOWS_UWP
        /// <summary>
        ///  Occurs when the user presses the hardware Back button. Allows the handlers to cancel the default behavior.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnBackRequested(BackRequestedEventArgs e) => BackRequested(this, e);
#endif

        private static ApplicationDataContainer GetSettingsContainer()
            => ApplicationData.Current.LocalSettings.CreateContainer("Caliburn.Micro", ApplicationDataCreateDisposition.Always);

#if WINDOWS_UWP
        private void OnBackRequested(object sender, BackRequestedEventArgs e) {
            OnBackRequested(e);

            if (e.Handled) {
                return;
            }

            if (CanGoBack) {
                e.Handled = true;
                GoBack();
            }
        }

#endif
    }
}
