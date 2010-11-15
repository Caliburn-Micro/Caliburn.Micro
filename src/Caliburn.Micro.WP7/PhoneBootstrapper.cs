namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// A custom bootstrapper designed to setup phone applications.
    /// </summary>
    public class PhoneBootstrapper : Bootstrapper
    {
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
        protected override void PrepareApplication()
        {
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

        void OnNavigated(object sender, NavigationEventArgs e)
        {
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
        protected virtual void OnDeactivate(object sender, DeactivatedEventArgs e)
        {
            Tombstone(SelectInstancesToTombstone());
        }

        /// <summary>
        /// Occurs when a previously tombstoned application instance is resurrected.
        /// </summary>
        protected virtual void OnActivate(object sender, ActivatedEventArgs e)
        {
            if (isResurrecting) {
                NavigatedEventHandler onNavigated = null;
                onNavigated = (s2, e2) => {
                    Resurrect(SelectInstancesToResurrect());
                    RootFrame.Navigated -= onNavigated;
                };
                RootFrame.Navigated += onNavigated;
            }

            isResurrecting = false;
        }

        /// <summary>
        /// Selects all instance which require some sort of persistence before tombstoning.
        /// </summary>
        /// <returns>The list of instances to be persisted.</returns>
        protected virtual IEnumerable<object> SelectInstancesToTombstone()
        {
            var fe = RootFrame.Content as FrameworkElement;
            if (fe != null && fe.DataContext != null)
                return new [] { fe.DataContext };
            return new object[0];
        }

        /// <summary>
        /// Selects all instance which require some sort of re-hydration after tombstoning.
        /// </summary>
        /// <returns>The list of instances to be re-hydrated.</returns>
        protected virtual IEnumerable<object> SelectInstancesToResurrect()
        {
            var fe = RootFrame.Content as FrameworkElement;
            if (fe != null && fe.DataContext != null)
                return new [] { fe.DataContext };
            return new object[0];
        }

        /// <summary>
        /// Persists the specified instances before tombstoning.
        /// </summary>
        /// <param name="instances">The instances to persist.</param>
        protected virtual void Tombstone(IEnumerable<object> instances)
        {
            var phoneService = (IPhoneService)IoC.GetAllInstances(typeof(IPhoneService))
                .FirstOrDefault() ?? new PhoneApplicationServiceAdapter(PhoneService);

            foreach(var instance in instances)
            {
                var persister = instance.GetType().GetAttributes<ITombstone>(true)
                    .FirstOrDefault();

                if (persister == null)
                    continue;

                persister.Tombstone(phoneService, null, null, instance, null);
            }
        }

        /// <summary>
        /// Occurs after resurrection has completed.
        /// </summary>
        public event EventHandler ResurrectionComplete = delegate { }; 

        /// <summary>
        /// Resurrects the instances after activation.
        /// </summary>
        /// <param name="instances">The instances to resurrect.</param>
        protected virtual void Resurrect(IEnumerable<object> instances)
        {
            var phoneService = (IPhoneService)IoC.GetAllInstances(typeof(IPhoneService))
                .FirstOrDefault() ?? new PhoneApplicationServiceAdapter(PhoneService);

            foreach(var instance in instances)
            {
                var persister = instance.GetType().GetAttributes<ITombstone>(true)
                    .FirstOrDefault();

                if (persister == null)
                    continue;

                persister.Resurrect(phoneService, null, null, instance, null);
            }

            ResurrectionComplete(this, EventArgs.Empty);
        }
    }
}