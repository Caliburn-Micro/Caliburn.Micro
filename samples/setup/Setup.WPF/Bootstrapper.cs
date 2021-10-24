using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Setup.WPF.ViewModels;

namespace Setup.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();

            // container.PerRequest<ShellViewModel>();
            GetType().Assembly.GetTypes().Where(type => type.IsClass)
     .Where(type => type.Name.EndsWith("ViewModel"))
     .ToList()
     .ForEach(viewModelType => container.RegisterPerRequest
         (viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
           await DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
