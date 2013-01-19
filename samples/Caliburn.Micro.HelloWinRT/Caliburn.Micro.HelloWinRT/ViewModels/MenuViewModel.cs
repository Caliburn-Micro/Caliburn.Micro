using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class MenuViewModel : ViewModelBase, ISupportSharing
    {
        private readonly INavigationService _navigationService;

        public MenuViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            
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
        }

        public void SampleSelected(ItemClickEventArgs eventArgs)
        {
            var sample = (SampleViewModel)eventArgs.ClickedItem;

            _navigationService.NavigateToViewModel(sample.ViewModelType);
        }

        public BindableCollection<SampleViewModel> Samples
        {
            get;
            private set;
        }

        public void OnShareRequested(DataRequest dataRequest)
        {
            dataRequest.Data.Properties.Title = "Caliburn.Micro Share Sample";
            dataRequest.Data.Properties.Description = "from Marker Metro";
            dataRequest.Data.SetUri(new Uri("https://markermetro.com"));
        }
    }
}
