using System;
using Caliburn.Micro;
#if XAMARINFORMS
using INavigationService = Caliburn.Micro.Xamarin.Forms.INavigationService;
#endif

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
                new FeatureViewModel("Action Conventions", "Wiring view events to view model methods.", typeof(ActionsViewModel)),
                new FeatureViewModel("Coroutines", "Using IEnumerable<IResult>", typeof(CoroutineViewModel)),
                new FeatureViewModel("Execute", "Using Execute to execute code on the UI thread.", typeof(ExecuteViewModel)),
                new FeatureViewModel("Event Aggregator", "Send events between uncoupled view models.", typeof(EventAggregationViewModel)),
#if !XAMARINFORMS
                new FeatureViewModel("Design Time", "Conventions in the xaml designer and design mode detection.", typeof(DesignTimeViewModel)),
#endif
                new FeatureViewModel("Conductors", "Composing view models together with lifecycle events", typeof(ConductorViewModel)),
                new FeatureViewModel("Bubbling Actions", "How to bubble actions from a child view model to a parent", typeof(BubblingViewModel)),
                new FeatureViewModel("Navigation", "Using a navigation service to switch between view models.", typeof(NavigationSourceViewModel)),
                // Context Menus
                // Navigation
                // Window Manager
            };
        }

        public BindableCollection<FeatureViewModel> Features { get; }

        public void ShowFeature(FeatureViewModel feature)
        {
            navigationService.NavigateToViewModel(feature.ViewModel);
        }
    }
}
