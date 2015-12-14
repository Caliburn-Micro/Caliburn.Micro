using System;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro.HelloUWP.Messages;
using Caliburn.Micro.HelloUWP;

namespace Caliburn.Micro.HelloUWP.ViewModels
{
    public class ShellViewModel : Screen, IHandle<ResumeStateMessage>, IHandle<SuspendStateMessage>
    {
        private readonly WinRTContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private INavigationService _navigationService;
        private bool _resume;

        private bool _isPaneOpen = true;
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

        public BindableCollection<SampleViewModel> Samples
        {
            get;
            private set;
        }


        private SampleViewModel _selectedSample;
        public SampleViewModel SelectedSample
        {
            get
            {
                return _selectedSample;
            }
            set
            {
                _selectedSample = value;
                NotifyOfPropertyChange(() => SelectedSample);
                _navigationService.NavigateToViewModel(_selectedSample.ViewModelType);
                IsPaneOpen = false;

            }
        }
        public ShellViewModel(WinRTContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            Samples = new BindableCollection<SampleViewModel>()
            {
                new SampleViewModel { Title = "Device", Subtitle = "Setting up a WinRT application.", ViewModelType = typeof(DeviceViewModel) },
                new SampleViewModel { Title = "Binding ItemTemplates", Subtitle = "Binding view model properties to your view.", ViewModelType = typeof(BindingItemTemplatesViewModel) },
                new SampleViewModel { Title = "Binding Conventions", Subtitle = "Binding view model properties to your view.", ViewModelType = typeof(BindingConventionsViewModel) }
            };

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



        public void Handle(SuspendStateMessage message)
        {
            _navigationService.SuspendState();
        }

        public void OpenClosePane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        public void Handle(ResumeStateMessage message)
        {
            _resume = true;
        }
    }
}
