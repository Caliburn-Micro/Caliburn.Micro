using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class NavigationTargetViewModel : Screen
    {
        private string _text;
        private bool _isEnabled;
        public NavigationTargetViewModel()
        {
            _text = string.Empty;
        }
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public bool IsNavigationEnabled
        {
            get { return _isEnabled; }
            set { Set(ref _isEnabled, value); }
        }
    }
}
