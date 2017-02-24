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

            container.RegisterPhoneServices(RootFrame);
            container.Singleton<FormsApp>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show(e.ExceptionObject.Message, "An error as occurred", MessageBoxButton.OK);
        }
    }
}
