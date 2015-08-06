using System;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.HelloUWP.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly WinRTContainer _container;
        private INavigationService _navigationService;

        public ShellViewModel(WinRTContainer container)
        {
            _container = container;
        }

        public void SetupNavigationService(Frame frame)
        {
            _navigationService = _container.RegisterNavigationService(frame);
        }

        public void ShowDevices()
        {
            _navigationService.For<DeviceViewModel>().Navigate();
        }
    }
}
