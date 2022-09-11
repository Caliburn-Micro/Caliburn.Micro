using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Caliburn.Micro.WinAppSdk.Test.ViewModels
{
    public class HomeViewModel : Screen
    {

        private static readonly ILog Log = LogManager.GetLog(typeof(HomeViewModel));

        private string _title;
        private string _clicked;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange("Title");
            }
        }
        
        public string Clicked
        {
            get => _clicked;
            set
            {
                _clicked = value;
                NotifyOfPropertyChange("Clicked");
            }
        }
        public HomeViewModel()
        {
            Log.Debug("Home View Model constructor");
            Title = "Welcome to Caliburn Micro in WinAppSdk";
            Clicked = "Button was clicked 0 times";
        }

        public void myButton()
        {
            Log.Debug("Button Clicked");
        }
    }
}

