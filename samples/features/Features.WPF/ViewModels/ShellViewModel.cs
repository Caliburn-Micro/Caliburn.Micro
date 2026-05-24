using System;
using System.Windows.Controls;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer _container;
        private INavigationService _navigationService;

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            _navigationService = new FrameAdapter(frame);

            _container.Instance(_navigationService);

            _navigationService.NavigateToViewModel(typeof(MenuViewModel));
        }
    }
}
