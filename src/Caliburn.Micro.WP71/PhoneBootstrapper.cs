namespace Caliburn.Micro {
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// A custom bootstrapper designed to setup phone applications.
    /// </summary>
    public class PhoneBootstrapper : Bootstrapper {
        bool phoneApplicationInitialized;
        PhoneApplicationService phoneService;

        /// <summary>
        /// The root frame used for navigation.
        /// </summary>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected override void PrepareApplication() {
            base.PrepareApplication();

            phoneService = new PhoneApplicationService();
            phoneService.Activated += OnActivate;
            phoneService.Deactivated += OnDeactivate;
            phoneService.Launching += OnLaunch;
            phoneService.Closing += OnClose;

            Application.ApplicationLifetimeObjects.Add(phoneService);

            if (phoneApplicationInitialized) {
                return;
            }

            RootFrame = CreatePhoneApplicationFrame();
            RootFrame.Navigated += OnNavigated;

            phoneApplicationInitialized = true;
        }

        void OnNavigated(object sender, NavigationEventArgs e) {
            if (Application.RootVisual != RootFrame) {
                Application.RootVisual = RootFrame;
            }
        }

        /// <summary>
        /// Creates the root frame used by the application.
        /// </summary>
        /// <returns>The frame.</returns>
        protected virtual PhoneApplicationFrame CreatePhoneApplicationFrame() {
            return new PhoneApplicationFrame();
        }

        /// <summary>
        /// Occurs when a fresh instance of the application is launching.
        /// </summary>
        protected virtual void OnLaunch(object sender, LaunchingEventArgs e) { }

        /// <summary>
        /// Occurs when a previously tombstoned or paused application is resurrected/resumed.
        /// </summary>
        protected virtual void OnActivate(object sender, ActivatedEventArgs e) { }

        /// <summary>
        /// Occurs when the application is being tombstoned or paused.
        /// </summary>
        protected virtual void OnDeactivate(object sender, DeactivatedEventArgs e) { }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        protected virtual void OnClose(object sender, ClosingEventArgs e) { }
    }
}