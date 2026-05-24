namespace Caliburn.Micro.Xamarin.Forms
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="INavigationService"/>
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Creates a Uri builder based on a view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="navigationService">The navigation service.</param>
        /// <returns>The builder.</returns>
        public static NavigateHelper<TViewModel> For<TViewModel>(this INavigationService navigationService)
        {
            return new NavigateHelper<TViewModel>().AttachTo(navigationService);
        }
    }
}
