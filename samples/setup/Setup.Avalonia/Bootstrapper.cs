using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Setup.Avalonia.ViewModels;

namespace Setup.Avalonia
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;

        public Bootstrapper()
        {
          LogManager.GetLog = type => new DebugLog(type);
          _container = new SimpleContainer();

            _container = _container.Instance(_container);
            Initialize();
            (DisplayRootViewFor<ShellViewModel>()).ConfigureAwait(false);
        }

        protected override void Configure()
        {
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            _container
               .PerRequest<ShellViewModel>()
               .PerRequest<MainViewModel>();
        }


        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
