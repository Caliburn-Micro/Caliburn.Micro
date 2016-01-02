using System;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class MenuViewModel : Screen
    {
        private readonly INavigationService navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Features = new BindableCollection<FeatureViewModel>
            {
                new FeatureViewModel("Binding Conventions", "Binding view model properties to your view.", typeof(BindingsViewModel)),
                new FeatureViewModel("Action Conventions", "Wiring view events to view model methods.", typeof(BindingsViewModel)),
                //new FeatureViewModel("Coroutines", "Mix IResult and Async / Await", typeof(CoroutineViewModel),
                //new FeatureViewModel("Execute", "Using Execute to execute code on the UI thread.", typeof(ExecuteViewModel),
                
            };
        }

        public BindableCollection<FeatureViewModel> Features { get; }

        public void ShowFeature(FeatureViewModel feature)
        {
            navigationService.NavigateToViewModel(feature.ViewModel);
        }
    }
}
