namespace Caliburn.Micro {
    using System;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// A custom IoC container which integrates with WinRT and properly registers all Caliburn.Micro services.
    /// </summary>
    public class WinRTContainer : SimpleContainer {
        /// <summary>
        /// Registers the Caliburn.Micro WinRT services with the container.
        /// </summary>
        public void RegisterWinRTServices() {
            RegisterInstance(typeof (SimpleContainer), null, this);
            RegisterInstance(typeof (WinRTContainer), null, this);

            if (!HasHandler(typeof (IEventAggregator), null)) {
                RegisterSingleton(typeof (IEventAggregator), null, typeof (EventAggregator));
            }
        }

        /// <summary>
        /// Registers the Caliburn.Micro navigation service with the container.
        /// </summary>
        /// <param name="rootFrame">The application root frame.</param>
        /// <param name="treatViewAsLoaded">if set to <c>true</c> [treat view as loaded].</param>
        /// <param name="cacheViewModels">if set to <c>true</c> then navigation service cache view models for resuse.</param>
        public INavigationService RegisterNavigationService(Frame rootFrame, bool treatViewAsLoaded = false, bool cacheViewModels = false) {
            if (HasHandler(typeof (INavigationService), null))
                return this.GetInstance<INavigationService>(null);

            if (rootFrame == null)
                throw new ArgumentNullException("rootFrame");
#if WinRT81
            var frameAdapter = cacheViewModels ? (INavigationService)
                new CachingFrameAdapter(rootFrame, treatViewAsLoaded) : 
                new FrameAdapter(rootFrame, treatViewAsLoaded);
#else
            var frameAdapter = new FrameAdapter(rootFrame, treatViewAsLoaded);
#endif

            RegisterInstance(typeof (INavigationService), null, frameAdapter);

            return frameAdapter;
        }

        /// <summary>
        /// Registers the Caliburn.Micro sharing service with the container.
        /// </summary>
        public ISharingService RegisterSharingService() {
            if (HasHandler(typeof (ISharingService), null))
                return this.GetInstance<ISharingService>(null);

            var sharingService = new SharingService();

            RegisterInstance(typeof (ISharingService), null, sharingService);

            return sharingService;
        }

#if !WP81 && !WINDOWS_UWP
        /// <summary>
        /// Registers the Caliburn.Micro settings service with the container.
        /// </summary>

        public ISettingsService RegisterSettingsService() {
            if (HasHandler(typeof (ISettingsService), null))
                return this.GetInstance<ISettingsService>(null);

            if (!HasHandler(typeof (ISettingsWindowManager), null))
#if WinRT81
                RegisterInstance(typeof (ISettingsWindowManager), null, new SettingsWindowManager());
#else
                RegisterInstance(typeof(ISettingsWindowManager), null, new CallistoSettingsWindowManager());
#endif

            var settingsService =
                new SettingsService((ISettingsWindowManager) GetInstance(typeof (ISettingsWindowManager), null));
            RegisterInstance(typeof (ISettingsService), null, settingsService);

            return settingsService;
        }
#endif
    }
}
