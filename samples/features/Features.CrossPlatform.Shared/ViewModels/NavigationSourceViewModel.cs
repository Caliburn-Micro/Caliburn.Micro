using System;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class NavigationSourceViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private string text;
        private bool isEnabled;

        public NavigationSourceViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { Set(ref isEnabled, value); }
        }

        public void Navigate()
        {
            navigationService.For<NavigationTargetViewModel>()
                .WithParam(v => v.Text, Text)
                .WithParam(v => v.IsEnabled, IsEnabled)
                .Navigate();
        }
    }
}
