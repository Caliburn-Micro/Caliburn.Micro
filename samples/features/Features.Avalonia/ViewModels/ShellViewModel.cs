using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ShellViewModel : Screen, IHandle<FeatureViewModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private SimpleContainer _container;
        private INavigationService _navigationService;

        // supressing warning for _navigationService not having a value in the constructor
        // need NavigationFrame to be passed in from the view
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ShellViewModel(IEventAggregator eventAggregator, SimpleContainer container)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
            _container = container;
        }

        public async Task HandleAsync(FeatureViewModel message, CancellationToken cancellationToken)
        {
            await _navigationService.NavigateToViewModelAsync(message.ViewModel);
        }

        protected override async Task OnInitializedAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializedAsync(cancellationToken);

            GoHome();
        }

        public void GoHome()
        {
            var menuVM = _container.GetInstance<MenuViewModel>();
            if (_navigationService != null)
            {
                _navigationService.NavigateToViewModelAsync(typeof(MenuViewModel));
            }
        }

        public void GoBack()
        {
            var menuVM = _container.GetInstance<MenuViewModel>();
            if (_navigationService != null)
            {
                _navigationService.GoBackAsync();
            }
        }

        public async void NavReady(NavigationFrame frame)
        {
            _navigationService = frame as INavigationService;

            _container.Instance(_navigationService);
            await _navigationService.NavigateToViewModelAsync(typeof(MenuViewModel));
        }
    }

}
