using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.WinAppSdk.Test.ViewModels
{
    public class HomeViewModel : Screen
    {

        private static readonly ILog Log = LogManager.GetLog(typeof(HomeViewModel));

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
            Log.Debug("Home View Model constructor");
            Title = "Welcome to Caliburn Micro in WinAppSdk";
        }
    }
}
