using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using Foundation;
using Hello.Xamarin.Core.ViewModels;
using UIKit;

namespace Hello.Xamarin.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer container;

        public override UIWindow Window
        {
            get;
            set;
        }

        protected override void Configure()
        {
            ViewModelLocator.AddNamespaceMapping("Hello.Xamarin.iOS", "Hello.Xamarin.Core.ViewModels");

            container = new SimpleContainer();

            container
                .PerRequest<LoginViewModel>();
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

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof(LoginViewModel).Assembly
            };
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Initialize();

            return true;
        }
    }
}