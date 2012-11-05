using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class BindingsViewModel : ViewModelBase
    {
        private DeveloperViewModel selectedDeveloper;

        public BindingsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Developers = new BindableCollection<DeveloperViewModel>
            {
                new DeveloperViewModel("Ben Gracewood"),
                new DeveloperViewModel("Ian Randall"),
                new DeveloperViewModel("Keith Patton"),
                new DeveloperViewModel("Nigel Sampson")
            };
        }

        public BindableCollection<DeveloperViewModel> Developers
        {
            get;
            private set;
        }

        public DeveloperViewModel SelectedDeveloper
        {
            get
            {
                return selectedDeveloper;
            }
            set
            {
                selectedDeveloper = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
