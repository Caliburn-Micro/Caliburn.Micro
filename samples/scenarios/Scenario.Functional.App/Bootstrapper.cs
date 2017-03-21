using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Scenario.Functional.App.Views;
using Scenario.Functional.Core.ViewModels;


namespace Scenario.Functional.App
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            ViewLocator.AddNamespaceMapping("Scenario.Functional.Core.ViewModels", "Scenario.Functional.App.Views");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            yield return typeof(ShellView).Assembly;
            yield return typeof(ShellViewModel).Assembly;
        }
    }
}
