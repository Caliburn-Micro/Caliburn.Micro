using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{

    /// <summary>
    /// Base view model for all our main screens, the method GoBack will be bound via convention
    /// to the back button and only display when it can go back due to the template of the back 
    /// button (Collapsed when Disabled)
    /// </summary>
    public abstract class ViewModelBase : Screen
    {
        private readonly INavigationService navigationService;

        protected ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void GoBack()
        {
            navigationService.GoBack();
        }

        public bool CanGoBack
        {
            get
            {
                return navigationService.CanGoBack;
            }
        }
    }
}
