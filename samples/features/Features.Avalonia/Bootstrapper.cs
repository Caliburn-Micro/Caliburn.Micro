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
            LogManager.GetLog = type => new DebugLog(type);
            container = new SimpleContainer();
            container.Instance(container); 
            
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
