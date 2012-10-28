using System;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    public class WinRTContainer : SimpleContainer
    {   
        public void RegisterWinRTServices(bool treatViewAsLoaded = false)
        {
            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(WinRTContainer), null, this);

            if (!HasHandler(typeof(IEventAggregator), null))
            {
                RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            }
        }

        public void RegisterNavigationService(Frame rootFrame, bool treatViewAsLoaded = false)
        {
            if (HasHandler(typeof(INavigationService), null))
                return;

            RegisterInstance(typeof(INavigationService), null, new FrameAdapter(rootFrame, treatViewAsLoaded));
        }
    }
}
