using System;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly WinRTContainer container;
        private INavigationService navigationService;

        public ShellViewModel(WinRTContainer container)
        {
            this.container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            container.RegisterNavigationService(frame);

            navigationService = container.GetInstance<INavigationService>();

            navigationService.Navigated += (s, e) => NotifyOfPropertyChange(nameof(CanGoBack));

            navigationService.NavigateToViewModel<MenuViewModel>();
        }

        public bool CanGoBack => navigationService?.CanGoBack ?? false;

        public void GoBack() => navigationService.GoBack();
    }
}
