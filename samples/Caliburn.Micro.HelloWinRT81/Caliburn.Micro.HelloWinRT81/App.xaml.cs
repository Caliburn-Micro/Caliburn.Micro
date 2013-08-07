using System;
using System.Collections.Generic;
using Caliburn.Micro.WinRT.Sample.ViewModels;
using Caliburn.Micro.WinRT.Sample.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.WinRT.Sample
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

            container.RegisterSharingService();

            container.RegisterSettingsService()
                .RegisterCommand<SampleSettingsViewModel>("Custom");

            container
                .PerRequest<ActionsViewModel>()
                .PerRequest<BindingsViewModel>()
                .PerRequest<CoroutineViewModel>()
                .PerRequest<ExecuteViewModel>()
                .PerRequest<MenuViewModel>()
                .PerRequest<NavigationTargetViewModel>()
                .PerRequest<NavigationViewModel>()
                .PerRequest<SampleSettingsViewModel>()
                .PerRequest<SearchViewModel>()
                .PerRequest<SettingsViewModel>()
                .PerRequest<SetupViewModel>()
                .PerRequest<ShareSourceViewModel>()
                .PerRequest<ShareTargetViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MenuView>();
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            DisplayRootView<SearchView>(args.QueryText);
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            // Normally wouldn't need to do this but need the container to be initialised
            Initialise();

            // replace the share operation in the container
            container.UnregisterHandler(typeof(ShareOperation), null);
            container.Instance(args.ShareOperation);

            DisplayRootViewFor<ShareTargetViewModel>();
        }
    }
}
