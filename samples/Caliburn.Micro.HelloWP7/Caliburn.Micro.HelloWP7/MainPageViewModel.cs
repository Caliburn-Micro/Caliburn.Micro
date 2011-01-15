namespace Caliburn.Micro.HelloWP7 {
    using System;

    public class MainPageViewModel {
        readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
        }

        public void GotoPageTwo() {
            navigationService.Navigate(new Uri("/PageTwo.xaml?NumberOfTabs=5", UriKind.RelativeOrAbsolute));
        }
    }
}