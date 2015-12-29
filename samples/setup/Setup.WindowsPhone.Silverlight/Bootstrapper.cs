using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Setup.WindowsPhone.ViewModels;

namespace Setup.WindowsPhone
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

            container.PerRequest<HomeViewModel>();
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
