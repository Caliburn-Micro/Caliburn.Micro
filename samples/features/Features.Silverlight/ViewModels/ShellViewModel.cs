using System;
using System.Windows.Controls;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer container;
        private INavigationService navigationService;

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            navigationService = new FrameAdapter(frame);

            container.Instance(navigationService);

            navigationService.Navigated += (s, e) => NotifyOfPropertyChange(nameof(CanGoBack));

            navigationService.NavigateToViewModel<MenuViewModel>();
        }

        public bool CanGoBack => navigationService?.CanGoBack ?? false;

        public void GoBack() => navigationService.GoBack();
    }
}
