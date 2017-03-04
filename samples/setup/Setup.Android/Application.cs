using System;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using Setup.Android.ViewModels;

namespace Setup.Android
{
    [Application(Label = "@string/ApplicationName", Icon = "@drawable/Icon")]
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
            container = new SimpleContainer();

            container.PerRequest<HomeViewModel>();
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