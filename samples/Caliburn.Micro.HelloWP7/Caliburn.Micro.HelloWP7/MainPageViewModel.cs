namespace Caliburn.Micro.HelloWP7 {
    public class MainPageViewModel {
        readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
        }

        public void GotoPageTwo() {
            navigationService.UriFor<PageTwoViewModel>()
                .WithParam(x => x.NumberOfTabs, 5)
                .Navigate();
        }
    }
}