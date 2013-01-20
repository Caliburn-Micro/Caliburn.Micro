using System;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public MenuViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            
            Samples = new BindableCollection<SampleViewModel>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Samples.Add(new SampleViewModel { Title = "Setup", Subtitle = "Setting up a WinRT application.", ViewModelType = typeof(SetupViewModel) });
            Samples.Add(new SampleViewModel { Title = "Binding Conventions", Subtitle = "Binding view model properties to your view.", ViewModelType = typeof(BindingsViewModel) });
            Samples.Add(new SampleViewModel { Title = "Action Conventions", Subtitle = "Wiring view events to view model methods.", ViewModelType = typeof(ActionsViewModel) });
            Samples.Add(new SampleViewModel { Title = "Coroutines", Subtitle = "Mix IResult and Async / Await", ViewModelType = typeof(CoroutineViewModel) });
            Samples.Add(new SampleViewModel { Title = "Execute", Subtitle = "Using Execute to execute code on the UI thread.", ViewModelType = typeof(ExecuteViewModel) });
            Samples.Add(new SampleViewModel { Title = "Navigating", Subtitle = "Navigating between pages and passing parameters.", ViewModelType = typeof(NavigationViewModel) });
            Samples.Add(new SampleViewModel { Title = "Search", Subtitle = "How to integrate the Share charm in your app.", ViewModelType = typeof(SearchViewModel) });
            Samples.Add(new SampleViewModel { Title = "Settings", Subtitle = "How to use your view models in the settings charm.", ViewModelType = typeof(SettingsViewModel) });
            Samples.Add(new SampleViewModel { Title = "Share Source", Subtitle = "How to use the share charm from your view model.", ViewModelType = typeof(ShareSourceViewModel) });
        }

        public void SampleSelected(ItemClickEventArgs eventArgs)
        {
            var sample = (SampleViewModel)eventArgs.ClickedItem;

            navigationService.NavigateToViewModel(sample.ViewModelType);
        }

        public BindableCollection<SampleViewModel> Samples
        {
            get;
            private set;
        }
    }
}
