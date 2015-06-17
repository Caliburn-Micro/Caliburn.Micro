using System;

namespace Caliburn.Micro {
    /// <summary>
    /// Extension methods related to navigation.
    /// </summary>
    public static class NavigationExtensions {
        /// <summary>
        /// Creates a Uri builder based on a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>The builder.</returns>
        [Obsolete("Use For instead of UriFor")]
        public static UriBuilder<TViewModel> UriFor<TViewModel>(this INavigationService navigationService) {
            return navigationService.For<TViewModel>();
        }

        /// <summary>
        /// Creates a Uri builder based on a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>The builder.</returns>
        public static UriBuilder<TViewModel> For<TViewModel>(this INavigationService navigationService) {
            return new UriBuilder<TViewModel>().AttachTo(navigationService);
        }
    }
}