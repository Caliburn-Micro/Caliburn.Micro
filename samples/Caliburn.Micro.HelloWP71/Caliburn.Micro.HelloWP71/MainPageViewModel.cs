namespace Caliburn.Micro.HelloWP71 {
    public class MainPageViewModel {
        readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
        }

        public void GotoPageTwo() {
            navigationService.UriFor<PivotPageViewModel>()
                .WithParam(x => x.NumberOfTabs, 5)
                .Navigate();
        }
    }
}