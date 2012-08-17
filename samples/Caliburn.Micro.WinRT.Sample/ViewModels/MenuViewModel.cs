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

            Samples.Add(new SampleViewModel { Title = "Setup", Subtitle = "Setting up a WinRT application.", ViewModelType = typeof(SetupViewModel)});
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
