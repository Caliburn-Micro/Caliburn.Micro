using System;
using System.Globalization;

namespace Caliburn.Micro {
    /// <summary>
    /// Extension methods for <see cref="INavigationService"/>.
    /// </summary>
    public static class NavigationExtensions {
        /// <summary>
        ///   Navigates to the specified content.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <typeparam name="T">The <see cref="System.Type" /> to navigate to.</typeparam>
        /// <returns>Whether or not navigation succeeded.</returns>
        public static bool Navigate<T>(this INavigationService navigationService, object parameter = null) => navigationService.Navigate(typeof(T), parameter);

        /// <summary>
        /// Navigate to the specified model type.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="viewModelType">The model type to navigate to.</param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <returns>Whether or not navigation succeeded.</returns>
        public static bool NavigateToViewModel(this INavigationService navigationService, Type viewModelType, object parameter = null) {
            Type viewType = ViewLocator.LocateTypeForModelType(viewModelType, null, null);
            return viewType == null
                ? throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "No view was found for {0}. See the log for searched views.", viewModelType.FullName))
                : navigationService.Navigate(viewType, parameter);
        }

        /// <summary>
        /// Navigate to the specified model type.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        /// <typeparam name="T">The model type to navigate to.</typeparam>
        /// <returns>Whether or not navigation succeeded.</returns>
        public static bool NavigateToViewModel<T>(this INavigationService navigationService, object parameter = null)
            => navigationService.NavigateToViewModel(typeof(T), parameter);

        /// <summary>
        /// Creates a Uri builder based on a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>The builder.</returns>
        [Obsolete("Use For instead of UriFor")]
        public static NavigateHelper<TViewModel> UriFor<TViewModel>(this INavigationService navigationService)
            => navigationService.For<TViewModel>();

        /// <summary>
        /// Creates a Uri builder based on a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>The builder.</returns>
        public static NavigateHelper<TViewModel> For<TViewModel>(this INavigationService navigationService)
            => new NavigateHelper<TViewModel>().AttachTo(navigationService);
    }
}
