using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Features.Avalonia.ViewModels;

namespace Features.Avalonia
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();

            DisplayRootViewFor<ShellViewModel>();
        }


    }
}
