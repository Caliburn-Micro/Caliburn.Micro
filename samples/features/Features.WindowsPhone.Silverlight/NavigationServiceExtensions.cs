using System;
using Caliburn.Micro;

namespace Features.CrossPlatform
{
    public static class NavigationServiceExtensions
    {
        public static void NavigateToViewModel(this INavigationService navigationService, Type viewModel)
        {
            var viewType = ViewLocator.LocateTypeForModelType(viewModel, null, null);
            
            var packUri = ViewLocator.DeterminePackUriFromType(viewModel, viewType);

            var uri = new Uri(packUri, UriKind.Relative);

            navigationService.Navigate(uri);
        }
    }
}
