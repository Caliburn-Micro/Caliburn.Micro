using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels;

namespace Features.CrossPlatform
{
    public class Bootstrapper : PhoneBootstrapperBase
    {
        private PhoneContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new PhoneContainer();

            if (!Execute.InDesignMode)
                container.RegisterPhoneServices(RootFrame);

            container
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

            MessageBox.Show("An error as occurred", e.ExceptionObject.Message, MessageBoxButton.OK);
        }
    }
}
