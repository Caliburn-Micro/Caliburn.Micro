using System;
using System.Collections.Generic;
using Autofac;
using Scenario.Autofac.ViewModels;
using Windows.ApplicationModel.Activation;

namespace Scenario.Autofac
{
    public sealed partial class App
    {
        private IContainer _container;

        public App()
        {
            InitializeComponent();
        }
        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ScenarioModule>();
            builder.RegisterType<ShellViewModel>();
            _container = builder.Build();
        }



        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return key == null ? _container.Resolve(service) : _container.ResolveNamed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);

            return _container.Resolve(type) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }
    }
}
