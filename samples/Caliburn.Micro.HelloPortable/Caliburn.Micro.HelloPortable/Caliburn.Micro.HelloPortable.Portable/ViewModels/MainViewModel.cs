using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Caliburn.Micro.HelloPortable.Portable.ViewModels
{
    public class MainViewModel : Screen
    {
        public string Name { get; set; }
        public MainViewModel()
        {
            Name = "Hello from portable";
        }
    }
}
