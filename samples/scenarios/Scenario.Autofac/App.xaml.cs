using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Scenario.Autofac.ViewModels;
using Windows.ApplicationModel.Activation;

namespace Scenario.Autofac
{
    public sealed partial class App
    {
        private IContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ScenarioModule>();

            container = builder.Build();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var displayStartPage = Task.Run(async () => await DisplayRootViewForAsync<ShellViewModel>());
            displayStartPage.Wait();
        }

        protected override object GetInstance(Type service, string key)
        {
            return key == null ? container.Resolve(service) : container.ResolveNamed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);

            return container.Resolve(type) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            container.InjectProperties(instance);
        }
    }
}
