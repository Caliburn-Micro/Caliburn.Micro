using Caliburn.Micro;

namespace Setup.WinUI3.ViewModels
{
    public class HomeViewModel : Screen
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange("Title");
            }
        }

        public HomeViewModel()
        {
            _title = string.Empty;
            Title = "Welcome to Caliburn Micro in WinUI3";
        }
    }
}
