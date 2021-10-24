using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Setup.WPF.Core.ViewModels
{
    public class ShellViewModel : Conductor<object>
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
            Title = "Welcome to Caliburn Micro in .net 5 WPF";
        }

    }
}
