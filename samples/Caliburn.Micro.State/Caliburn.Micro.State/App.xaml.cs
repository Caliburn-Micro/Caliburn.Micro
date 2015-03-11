using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro.State.ViewModels;
using Caliburn.Micro.State.Views;
using Octokit;
using Octokit.Internal;


namespace Caliburn.Micro.State
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
            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);

            var gitHubClient = new GitHubClient(
                new ProductHeaderValue("Caliburn.Micro.State", "1.0"),
                new InMemoryCredentialStore(Credentials.Anonymous));

            _container = new WinRTContainer();

            _container.RegisterWinRTServices();

            _container.Instance<IGitHubClient>(gitHubClient);

            _container
                .PerRequest<RepositorySearchViewModel>()
                .PerRequest<RepositoryDetailsViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            // Use the new caching frame adapter rather than the normal one
            _container.RegisterNavigationService(rootFrame, cacheViewModels: true);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            DisplayRootView<RepositorySearchView>();
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
    }
}