namespace Caliburn.Micro
{
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// A custom bootstrapper designed to setup phone applications.
    /// </summary>
    public class PhoneBootstrapper : Bootstrapper
    {
        /// <summary>
        /// Indicates whether or not the phone application has initialized.
        /// </summary>
        protected bool PhoneApplicationInitialized;
        bool isResurrecting = true;

        /// <summary>
        /// The phone application services.
        /// </summary>
        public PhoneApplicationService PhoneService { get; private set; }

        /// <summary>
        /// The root frame used for navigation.
        /// </summary>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected override void PrepareApplication() {
            base.PrepareApplication();

            PhoneService = new PhoneApplicationService();
            PhoneService.Activated += OnActivate;
            PhoneService.Deactivated += OnDeactivate;
            PhoneService.Launching += OnLaunch;
            PhoneService.Closing += OnClose;

            Application.ApplicationLifetimeObjects.Add(PhoneService);

            if (PhoneApplicationInitialized)
                return;

            RootFrame = CreatePhoneApplicationFrame();
            RootFrame.Navigated += OnNavigated;

            PhoneApplicationInitialized = true;
        }

        void OnNavigated(object sender, NavigationEventArgs e) {
            if (Application.RootVisual != RootFrame)
                Application.RootVisual = RootFrame;
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
        protected virtual void OnLaunch(object sender, LaunchingEventArgs e) {
            isResurrecting = false;
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        protected virtual void OnClose(object sender, ClosingEventArgs e) { }

        /// <summary>
        /// Occurs when the application is being tombstoned.
        /// </summary>
        protected virtual void OnDeactivate(object sender, DeactivatedEventArgs e) {
            Tombstone();
        }

        /// <summary>
        /// Occurs when a previously tombstoned application instance is resurrected.
        /// </summary>
        protected virtual void OnActivate(object sender, ActivatedEventArgs e)
        {
            if (isResurrecting) {
                NavigatedEventHandler onNavigated = null;
                onNavigated = (s2, e2) => {
                    Resurrect();
                    RootFrame.Navigated -= onNavigated;
                };
                RootFrame.Navigated += onNavigated;
            }

            isResurrecting = false;
        }

        /// <summary>
        /// Called when tombstoning is required.
        /// </summary>
        protected virtual void Tombstone() { }

        /// <summary>
        /// Resurrects the instances after activation.
        /// </summary>
        protected virtual void Resurrect() { }
    }
}