using System;
using Caliburn.Micro;

namespace Setup.UWP.ViewModels
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
            Title = "Welcome to Caliburn Micro in UWP";
        }
    }
}
