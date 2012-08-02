using System;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    public class WinRTContainer : SimpleContainer
    {
        private readonly Frame rootFrame;

        public WinRTContainer(Frame rootFrame)
        {
            this.rootFrame = rootFrame;
        }

        public void RegisterWinRTServices(bool treatViewAsLoaded = false)
        {
            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(WinRTContainer), null, this);

            if (!HasHandler(typeof(INavigationService), null))
            {
                RegisterInstance(typeof(INavigationService), null, new FrameAdapter(rootFrame, treatViewAsLoaded));
            }

            if (!HasHandler(typeof(IEventAggregator), null))
            {
                RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            }
        }
    }
}
