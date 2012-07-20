namespace Caliburn.Micro {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;

    /// <summary>
    ///   Implemented by services that provide <see cref="Uri" /> based navigation.
    /// </summary>
    public interface INavigationService : INavigate {
        /// <summary>
        ///   The <see cref="Uri" /> source.
        /// </summary>
        Uri Source { get; set; }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        ///   The current <see cref="Uri" /> source.
        /// </summary>
        Uri CurrentSource { get; }

        /// <summary>
        ///   The current content.
        /// </summary>
        object CurrentContent { get; }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        void StopLoading();

        /// <summary>
        ///   Navigates back.
        /// </summary>
        void GoBack();

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        void GoForward();

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
        JournalEntry RemoveBackEntry();

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
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        event FragmentNavigationEventHandler FragmentNavigation;
    }

    /// <summary>
    ///   A basic implementation of <see cref="INavigationService" /> designed to adapt the <see cref="Frame" /> control.
    /// </summary>
    public class FrameAdapter : INavigationService {
        readonly Frame frame;
        readonly bool treatViewAsLoaded;
        event NavigatingCancelEventHandler ExternalNavigatingHandler = delegate { };

        /// <summary>
        ///   Creates an instance of <see cref="FrameAdapter" />
        /// </summary>
        /// <param name="frame"> The frame to represent as a <see cref="INavigationService" /> . </param>
        /// <param name="treatViewAsLoaded"> Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called. This is necessary when using the TransitionFrame. </param>
        public FrameAdapter(Frame frame, bool treatViewAsLoaded = false) {
            this.frame = frame;
            this.treatViewAsLoaded = treatViewAsLoaded;
            this.frame.Navigating += OnNavigating;
            this.frame.Navigated += OnNavigated;
        }

        /// <summary>
        ///   Occurs before navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual void OnNavigating(object sender, NavigatingCancelEventArgs e) {
            ExternalNavigatingHandler(sender, e);
            if(e.Cancel) {
                return;
            }

            var fe = frame.Content as FrameworkElement;
            if(fe == null) {
                return;
            }

            var guard = fe.DataContext as IGuardClose;
            if(guard != null && !e.Uri.IsAbsoluteUri) {
                var shouldCancel = false;
                guard.CanClose(result => { shouldCancel = !result; });

                if(shouldCancel) {
                    e.Cancel = true;
                    return;
                }
            }

            var deactivator = fe.DataContext as IDeactivate;

            // If we are navigating to the same page there is no need to deactivate
            // e.g. When the app is activated with Fast Switch
            if(deactivator != null && frame.CurrentSource != e.Uri) {
                deactivator.Deactivate(CanCloseOnNavigating(sender, e));
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
        ///   Occurs after navigation
        /// </summary>
        /// <param name="sender"> The event sender. </param>
        /// <param name="e"> The event args. </param>
        protected virtual void OnNavigated(object sender, NavigationEventArgs e) {
            if (e.Uri.IsAbsoluteUri || e.Content == null) {
                return;
            }

            ViewLocator.InitializeComponent(e.Content);

            var viewModel = ViewModelLocator.LocateForView(e.Content);
            if (viewModel == null) {
                return;
            }

            var page = e.Content as PhoneApplicationPage;
            if (page == null) {
                throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from PhoneApplicationPage or one of its descendents.");
            }

            if (treatViewAsLoaded) {
                page.SetValue(View.IsLoadedProperty, true);
            }

            TryInjectQueryString(viewModel, page);
            ViewModelBinder.Bind(viewModel, page, null);

            var activator = viewModel as IActivate;
            if (activator != null) {
                activator.Activate();
            }

            var viewAware = viewModel as ViewAware;
            if(viewAware != null) {
                EventHandler onLayoutUpdate = null;
                onLayoutUpdate = delegate {
                    viewAware.OnViewReady(page);
                    page.LayoutUpdated -= onLayoutUpdate;
                };
                page.LayoutUpdated += onLayoutUpdate;
            }

            GC.Collect();
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model. </param>
        /// <param name="page"> The page. </param>
        protected virtual void TryInjectQueryString(object viewModel, Page page) {
            var viewModelType = viewModel.GetType();

            foreach(var pair in page.NavigationContext.QueryString) {
                var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);
                if(property == null) {
                    continue;
                }

                property.SetValue(
                    viewModel,
                    MessageBinder.CoerceValue(property.PropertyType, pair.Value, page.NavigationContext),
                    null
                    );
            }
        }

        /// <summary>
        ///   The <see cref="Uri" /> source.
        /// </summary>
        public Uri Source {
            get { return frame.Source; }
            set { frame.Source = value; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate back.
        /// </summary>
        public bool CanGoBack {
            get { return frame.CanGoBack; }
        }

        /// <summary>
        ///   Indicates whether the navigator can navigate forward.
        /// </summary>
        public bool CanGoForward {
            get { return frame.CanGoForward; }
        }

        /// <summary>
        ///   The current <see cref="Uri" /> source.
        /// </summary>
        public Uri CurrentSource {
            get { return frame.CurrentSource; }
        }

        /// <summary>
        ///   The current content.
        /// </summary>
        public object CurrentContent {
            get { return frame.Content; }
        }

        /// <summary>
        ///   Stops the loading process.
        /// </summary>
        public void StopLoading() {
            frame.StopLoading();
        }

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public void GoBack() {
            frame.GoBack();
        }

        /// <summary>
        ///   Navigates forward.
        /// </summary>
        public void GoForward() {
            frame.GoForward();
        }

        /// <summary>
        ///   Navigates to the specified <see cref="Uri" /> .
        /// </summary>
        /// <param name="source"> The <see cref="Uri" /> to navigate to. </param>
        /// <returns> Whether or not navigation succeeded. </returns>
        public bool Navigate(Uri source) {
            return frame.Navigate(source);
        }

        /// <summary>
        ///   Removes the most recent entry from the back stack.
        /// </summary>
        /// <returns> The entry that was removed. </returns>
        public JournalEntry RemoveBackEntry() {
            return ((Page)frame.Content).NavigationService.RemoveBackEntry();
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
        ///   Raised when a fragment navigation occurs.
        /// </summary>
        public event FragmentNavigationEventHandler FragmentNavigation {
            add { frame.FragmentNavigation += value; }
            remove { frame.FragmentNavigation -= value; }
        }
    }
}