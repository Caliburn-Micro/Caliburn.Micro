using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels;


namespace Features.Avalonia
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;

        public Bootstrapper()
        {
            LogManager.GetLog = type => new DebugLog(type);
            _container = new SimpleContainer();
            _container.Instance(_container); 
            
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
                .PerRequest<MenuViewModel>()
                .PerRequest<BindingsViewModel>()
                .PerRequest<ActionsViewModel>()
                .PerRequest<CoroutineViewModel>()
                .PerRequest<ExecuteViewModel>()
                .PerRequest<EventAggregationViewModel>()
                .PerRequest<DesignTimeViewModel>()
                .PerRequest<ConductorViewModel>()
                .PerRequest<BubblingViewModel>()
                .PerRequest<NavigationSourceViewModel>()
                .PerRequest<NavigationTargetViewModel>();
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
