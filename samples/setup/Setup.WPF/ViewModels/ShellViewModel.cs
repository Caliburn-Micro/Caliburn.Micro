using System;
using System.Windows;
using Caliburn.Micro;

namespace Setup.WPF.ViewModels
{
    public class ShellViewModel : Screen
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

        public ShellViewModel()
        {
            Title = "Welcome to Caliburn Micro in WPF";
        }

    }
}
