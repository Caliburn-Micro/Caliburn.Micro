using Caliburn.Micro;

#if XAMARINFORMS
using Caliburn.Micro.Xamarin.Forms;
#endif


namespace Features.CrossPlatform.ViewModels
{
    public class NavigationSourceViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private string _text;
        private bool _isEnabled;

        public NavigationSourceViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { Set(ref _isEnabled, value); }
        }

        public void Navigate()
        {
#if !AVALONIA
            _navigationService.For<NavigationTargetViewModel>()
                .WithParam(v => v.Text, Text)
                .WithParam(v => v.IsEnabled, IsEnabled)
                .Navigate();
#endif
        }
    }
}
