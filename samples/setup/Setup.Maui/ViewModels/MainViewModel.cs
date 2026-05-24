using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Setup.Maui.ViewModels
{
    public class MainViewModel : Screen
    {
        int _count = 0;
        public void OnCounterClicked()
        {
            Count++;
        }

        public int Count
        {
            get => _count;
            set => Set(ref _count, value);
        }
    }
}
