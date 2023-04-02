using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Setup.Avalonia.ViewModels;

namespace Setup.Avalonia
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
          LogManager.GetLog = type => new DebugLog(type);
          container = new SimpleContainer();

            container = container.Instance(container);
            Initialize();
            (DisplayRootViewFor<ShellViewModel>()).ConfigureAwait(false);
        }

        protected override void Configure()
        {
            container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            container
               .PerRequest<ShellViewModel>()
               .PerRequest<MainViewModel>();
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
