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

        public ShellViewModel(IEventAggregator eventAggregator, SimpleContainer container)
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

        public async void NavReady(NavigationFrame frame)
        {
            _navigationService = frame as INavigationService;

            _container.Instance(_navigationService);
            await _navigationService.NavigateToViewModelAsync(typeof(MenuViewModel));
        }
    }

}
