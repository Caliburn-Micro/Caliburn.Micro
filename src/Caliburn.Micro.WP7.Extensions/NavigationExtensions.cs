namespace Caliburn.Micro {
    public static class NavigationExtensions {
        public static UriBuilder<TViewModel> UriFor<TViewModel>(this INavigationService navigationService) {
            return new UriBuilder<TViewModel>(navigationService);
        }
    }
}