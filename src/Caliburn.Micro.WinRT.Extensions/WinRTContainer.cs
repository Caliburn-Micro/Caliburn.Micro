using System;
using System.Collections.Generic;
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
        /// <param name="rootFrame">The application root frame.</param>
        public void RegisterSharingService(Frame rootFrame)
        {
            if (HasHandler(typeof (ISharingService), null))
                return;

            if (rootFrame == null)
                throw new ArgumentNullException("rootFrame");

            RegisterInstance(typeof(ISharingService), null, new SharingeService(rootFrame));
        }

        /// <summary>
        /// Registers the settings service with the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="settingsWindowManager">The settings window manager.</param>
        public void RegisterSettingsService(ISettingsWindowManager settingsWindowManager)
        {
            if (HasHandler(typeof(ISettingsService), null))
                return;

            if (settingsWindowManager == null)
                throw new ArgumentNullException("settingsWindowManager");

            RegisterInstance(typeof(ISettingsService), null, new SettingsService(settingsWindowManager));
        }

        /// <summary>
        /// Registers a Settings Command with the container.
        /// </summary>
        /// <typeparam name="TViewModel">The commands view model.</typeparam>
        /// <param name="commandId">The command id.</param>
        /// <param name="label">The command label.</param>
        /// <param name="viewSettings">The optional flyout view settings.</param>
        public void SettingsCommand<TViewModel>(object commandId, string label, IDictionary<string, object> viewSettings = null)
        {
            RegisterInstance(typeof(CaliburnSettingsCommand), null, new CaliburnSettingsCommand(commandId, label, typeof(TViewModel), viewSettings));
        }
    }
}
