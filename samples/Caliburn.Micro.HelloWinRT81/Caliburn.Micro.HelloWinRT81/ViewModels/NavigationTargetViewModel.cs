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
                this.Set(ref name, value);
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
                this.Set(ref age, value);
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
                this.Set(ref isMarried, value);
            }
        }
    }
}
