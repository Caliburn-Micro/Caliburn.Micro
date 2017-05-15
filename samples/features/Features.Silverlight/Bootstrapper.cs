using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels;

namespace Features.CrossPlatform
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IEventAggregator, EventAggregator>();
            container.Instance(container);

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

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show(
                $"{e.ExceptionObject.Message}\n\n{e.ExceptionObject.StackTrace}", 
                "An error as occurred", 
                MessageBoxButton.OK);
        }
    }
}
