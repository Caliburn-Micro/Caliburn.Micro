using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Hello.Xamarin.Core.ViewModels;
using Hello.Xamarin.Windows.Views;

namespace Hello.Xamarin.Windows
{
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            ViewModelLocator.AddNamespaceMapping("Hello.Xamarin.Windows.Views", "Hello.Xamarin.Core.ViewModels");

            container = new WinRTContainer();
            container.RegisterWinRTServices();

            container
                .PerRequest<LoginViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
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

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            DisplayRootView<LoginView>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().GetTypeInfo().Assembly,
                typeof (LoginViewModel).GetTypeInfo().Assembly
            };
        }
    }
}