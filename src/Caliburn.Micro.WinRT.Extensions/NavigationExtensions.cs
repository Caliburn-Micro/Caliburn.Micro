using System;

namespace Caliburn.Micro
{
    public static class NavigationExtensions
    {
        public static bool Navigate<T>(this INavigationService navigationService, object parameter = null)
        {
            return navigationService.Navigate(typeof(T), parameter);
        }

        public static bool NavigateToViewModel(this INavigationService navigationService, Type viewModelType, object parameter = null)
        {
            var viewType = ViewLocator.LocateTypeForModelType(viewModelType, null, null);

            if (viewType == null)
            {
                throw new Exception(string.Format("No view was found for {0}. See the log for searched views.", viewModelType.FullName));
            }

            return navigationService.Navigate(viewType, parameter);
        }

        public static bool NavigateToViewModel<T>(this INavigationService navigationService, object parameter = null)
        {
            return navigationService.NavigateToViewModel(typeof(T), parameter);
        }
    }
}