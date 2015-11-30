namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Extension methods for <see cref="INavigationService"/>
    /// </summary>
    public static class NavigationExtensions {
        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <param name="infoOverride">Info about the animated transition.</param>
        /// <typeparam name="T">The <see cref="System.Type" /> to navigate to.</typeparam>
        /// <returns>Whether or not navigation succeeded.</returns>
#if WinRT81 || WP81
        public static bool Navigate<T>(this INavigationService navigationService, object parameter = null,
            Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo infoOverride = null) {

            if (infoOverride == null) {
                return navigationService.Navigate(typeof(T), parameter);
            }

            return navigationService.Navigate(typeof(T), parameter, infoOverride);
        }
#else
        public static bool Navigate<T>(this INavigationService navigationService, object parameter = null) {
            return navigationService.Navigate(typeof (T), parameter);
        }
#endif

        /// <summary>
        /// Navigate to the specified model type.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="viewModelType">The model type to navigate to.</param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <param name="infoOverride">Info about the animated transition.</param>
        /// <returns>Whether or not navigation succeeded.</returns>
#if WinRT81 || WP81
        public static bool NavigateToViewModel(this INavigationService navigationService, Type viewModelType,
            object parameter = null, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo infoOverride = null) {
            var viewType = ViewLocator.LocateTypeForModelType(viewModelType, null, null);
            if (viewType == null) {
                throw new InvalidOperationException(
                    string.Format("No view was found for {0}. See the log for searched views.", viewModelType.FullName));
            }

            if (infoOverride == null) {
                return navigationService.Navigate(viewType, parameter);
            }

            return navigationService.Navigate(viewType, parameter, infoOverride);
        }
#else
        public static bool NavigateToViewModel(this INavigationService navigationService, Type viewModelType,
            object parameter = null) {
            var viewType = ViewLocator.LocateTypeForModelType(viewModelType, null, null);
            if (viewType == null) {
                throw new InvalidOperationException(
                    string.Format("No view was found for {0}. See the log for searched views.", viewModelType.FullName));
            }

            return navigationService.Navigate(viewType, parameter);
        }
#endif


        /// <summary>
        /// Navigate to the specified model type.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <param name="infoOverride">Info about the animated transition.</param>
        /// <typeparam name="T">The model type to navigate to.</typeparam>
        /// <returns>Whether or not navigation succeeded.</returns>
#if WinRT81 || WP81
        public static bool NavigateToViewModel<T>(this INavigationService navigationService, object parameter = null,
            Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo infoOverride = null) {

            return navigationService.NavigateToViewModel(typeof (T), parameter, infoOverride);
        }
#else
        public static bool NavigateToViewModel<T>(this INavigationService navigationService, object parameter = null) {
            return navigationService.NavigateToViewModel(typeof (T), parameter);
        }
#endif

        /// <summary>
        /// Creates a Uri builder based on a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>The builder.</returns>
        public static UriBuilder<TViewModel> UriFor<TViewModel>(this INavigationService navigationService) {
            return new UriBuilder<TViewModel>().AttachTo(navigationService);
        }
    }
}
