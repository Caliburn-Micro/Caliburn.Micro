using System;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// A custom IoC container which integrates with WinRT and properly registers all Caliburn.Micro services.
    /// </summary>
    public class WinRTContainer : SimpleContainer
    {
        /// <summary>
        /// Registers the Caliburn.Micro WinRT services with the container.
        /// </summary>
        public void RegisterWinRTServices()
        {
            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(WinRTContainer), null, this);

            if (!HasHandler(typeof(IEventAggregator), null))
            {
                RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            }
        }

        /// <summary>
        /// Registers the Caliburn.Micro navigation service with the container.
        /// </summary>
        /// <param name="rootFrame">The application root frame.</param>
        /// <param name="treatViewAsLoaded">if set to <c>true</c> [treat view as loaded].</param>
        public void RegisterNavigationService(Frame rootFrame, bool treatViewAsLoaded = false)
        {
            if (HasHandler(typeof(INavigationService), null))
                return;

            if (rootFrame == null)
                throw new ArgumentNullException("rootFrame");

            RegisterInstance(typeof(INavigationService), null, new FrameAdapter(rootFrame, treatViewAsLoaded));
        }

        /// <summary>
        /// Registers the Caliburn.Micro sharing service with the container.
        /// </summary>
        public void RegisterSharingService()
        {
            if (HasHandler(typeof (ISharingService), null))
                return;

            RegisterInstance(typeof(ISharingService), null, new SharingService());
        }

        /// <summary>
        /// Registers the Caliburn.Micro settings service with the container.
        /// </summary>
        public ISettingsService RegisterSettingsService()
        {
            if (HasHandler(typeof(ISettingsService), null))
                return (ISettingsService)GetInstance(typeof(ISettingsService), null);

            if (!HasHandler(typeof(ISettingsWindowManager), null))
                RegisterInstance(typeof(ISettingsWindowManager), null, new CallistoSettingsWindowManager());

            var settingsService = new SettingsService((ISettingsWindowManager)GetInstance(typeof(ISettingsWindowManager), null));
            RegisterInstance(typeof(ISettingsService), null, settingsService);

            return settingsService;
        }
    }
}
