using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.WinAppSdk.Test.ViewModels
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
            Title = "Welcome to Caliburn Micro in WinAppSdk";
        }
    }
}
