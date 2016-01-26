using System;
using Caliburn.Micro.Xamarin.Forms;

namespace Features.CrossPlatform
{
    public static class NavigationServiceExtensions
    {
        public static async void NavigateToViewModel(this INavigationService navigationService, Type viewModelType)
        {
            await navigationService.NavigateToViewModelAsync(viewModelType);
        }
    }
}
