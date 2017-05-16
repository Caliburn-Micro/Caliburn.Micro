using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels;

namespace Features.CrossPlatform
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
            container = new WinRTContainer();
            container.RegisterWinRTServices();

            container
                .PerRequest<ShellViewModel>()
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

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            DisplayRootViewFor<ShellViewModel>();
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

        protected override async void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            var dialog = new MessageDialog(e.Message, "An error has occurred");

            await dialog.ShowAsync();
        }
    }
}
