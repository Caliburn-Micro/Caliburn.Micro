using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using Hello.Forms.ViewModels;

namespace Hello.Forms.WinPhone
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

            container.Singleton<Forms.App>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof (LoginViewModel).Assembly
            };
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
    }
}
