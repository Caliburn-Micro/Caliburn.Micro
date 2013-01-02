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
        /// <param name="treatViewAsLoaded">if set to <c>true</c> [treat view as loaded].</param>
        public void RegisterWinRTServices(bool treatViewAsLoaded = false)
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

            RegisterInstance(typeof(INavigationService), null, new FrameAdapter(rootFrame, treatViewAsLoaded));
        }
    }
}
