using System;
using System.Windows;
using Caliburn.Micro;
using Setup.WPF.ViewModels;

namespace Setup.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
