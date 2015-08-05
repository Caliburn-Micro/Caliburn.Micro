using System;
using System.Collections.Generic;
using System.Reflection;
using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using Hello.Xamarin.Core.ViewModels;

namespace Hello.Xamarin.Android
{
    [Application(Label = "@string/ApplicationName", Icon="@drawable/Icon")]
    public class Application : CaliburnApplication
    {
        private SimpleContainer container;

        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            Initialize();
        }

        protected override void Configure()
        {
            ViewModelLocator.AddNamespaceMapping("Hello.Xamarin.Android.Activities", "Hello.Xamarin.Core.ViewModels");

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
    }
}