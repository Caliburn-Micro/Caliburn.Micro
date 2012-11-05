using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public NavigationViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this.navigationService = navigationService;
        }

        public void Navigate()
        {
            navigationService.UriFor<NavigationTargetViewModel>()
                .WithParam(m => m.Name, "Nigel Sampson")
                .WithParam(m => m.Age, 31)
                .WithParam(m => m.IsMarried, true)
                .Navigate();
        }
    }
}
