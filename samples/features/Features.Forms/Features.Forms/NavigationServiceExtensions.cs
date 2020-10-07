using System;
using Caliburn.Micro;

#if XAMARINFORMS
using Caliburn.Micro.Xamarin.Forms;
#endif

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
