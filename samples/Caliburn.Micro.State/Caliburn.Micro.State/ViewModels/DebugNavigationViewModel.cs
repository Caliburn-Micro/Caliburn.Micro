using System;

namespace Caliburn.Micro.State.ViewModels
{
    public class DebugNavigationViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public DebugNavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanGoBack
        {
            get { return _navigationService.CanGoBack; }
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public bool CanGoForward
        {
            get { return _navigationService.CanGoForward; }
        }

        public void GoForward()
        {
            _navigationService.GoForward();
        }
    }
}
