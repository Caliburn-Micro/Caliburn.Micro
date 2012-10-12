using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class NavigationTargetViewModel : ViewModelBase
    {
        private string name;
        private int age;
        private bool isMarried;

        public NavigationTargetViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsMarried
        {
            get
            {
                return isMarried;
            }
            set
            {
                isMarried = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
