using System;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro.HelloUWP.Messages;

namespace Caliburn.Micro.HelloUWP.ViewModels
{
    public class ShellViewModel : Screen, IHandle<ResumeStateMessage>, IHandle<SuspendStateMessage>
    {
        private readonly WinRTContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private INavigationService _navigationService;
        private bool _resume;

        private bool _isPaneOpen=true;
        public bool IsPaneOpen
        {
            get
            {
                return _isPaneOpen;
            }
            set
            {
                _isPaneOpen = value;
                NotifyOfPropertyChange(() => IsPaneOpen);
            }
        }
        public ShellViewModel(WinRTContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void SetupNavigationService(Frame frame)
        {
            _navigationService = _container.RegisterNavigationService(frame);

            if (_resume)
                _navigationService.ResumeState();
        }

        public void ShowDevices()
        {
            _navigationService.For<DeviceViewModel>().Navigate();
            IsPaneOpen = false;
        }
        public void ShowBindings()
        {
            _navigationService.For<BindingsViewModel>().Navigate();
            IsPaneOpen = false;
        }
        public void Handle(SuspendStateMessage message)
        {
            _navigationService.SuspendState();
        }

        public void OpenClosePane()
        {
            IsPaneOpen=!IsPaneOpen;
        }
        public void Handle(ResumeStateMessage message)
        {
            _resume = true;
        }
    }
}
