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
        private bool _isNavigationEnabled;

        public NavigationSourceViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public bool IsNavigationEnabled
        {
            get { return _isNavigationEnabled; }
            set { Set(ref _isNavigationEnabled, value); }
        }

        public void Navigate()
        {
            _navigationService.For<NavigationTargetViewModel>()
                .WithParam(v => v.Text, Text)
                .WithParam(v => v.IsNavigationEnabled, IsNavigationEnabled)
                .Navigate();
#endif
        }
    }
}
