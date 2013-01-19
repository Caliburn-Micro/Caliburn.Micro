using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro.WinRT.Sample.Settings;
using Caliburn.Micro.WinRT.Sample.ViewModels;
using Caliburn.Micro.WinRT.Sample.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.WinRT.Sample
{
    public sealed partial class App
    {
        private WinRTContainer _container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            // register all view models
            var serviceInfo = typeof(PropertyChangedBase).GetTypeInfo();
            var types = from info in typeof(App).GetTypeInfo().Assembly.DefinedTypes
                        where serviceInfo.IsAssignableFrom(info)
                              && !info.IsAbstract
                              && !info.IsInterface
                        select info.AsType();
            types.Apply(t => _container.RegisterPerRequest(t, null, t));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
            _container.RegisterSharingService(rootFrame);

            _container.RegisterSettingsService(new SimplePopupSettingsWindowManager());
            _container.SettingsCommand<SampleSettingsViewModel>(1, "Custom");
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
            _container.UnregisterHandler(typeof(ShareOperation), null);
            _container.Instance(args.ShareOperation);

            DisplayRootViewFor<ShareTargetViewModel>();
        }
    }
}
