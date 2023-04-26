using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Avalonia.Controls;
using Caliburn.Micro;
using Features.Avalonia.ViewModels;

namespace Features.Avalonia.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IHandle<FeatureViewModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private INavigationService navigationService;

        public ShellViewModel(IEventAggregator eventAggregator      )
        {
            _eventAggregator = eventAggregator;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public async Task HandleAsync(FeatureViewModel message, CancellationToken cancellationToken)
        {
            var vm = IoC.GetInstance(message.ViewModel, null) as Screen;
            await ActivateItemAsync(vm);
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            await ShowMenuPage();
        }

        private async Task ShowMenuPage()
        {
            var menuVM = IoC.GetInstance(typeof(MenuViewModel), null) as MenuViewModel;
            await ActivateItemAsync(menuVM);
        }

        public async void GoHome()
        {
            await ShowMenuPage();
        }

        public void RegisterFrame(ContentControl frame)
        {
            int x = 1;
            //navigationService = new FrameAdapter(frame);

            //container.Instance(navigationService);

            //navigationService.NavigateToViewModel(typeof(MenuViewModel));
        }
    }

}
