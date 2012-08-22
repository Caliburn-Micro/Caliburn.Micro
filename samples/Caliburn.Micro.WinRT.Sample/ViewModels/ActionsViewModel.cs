using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ActionsViewModel : ViewModelBase
    {
        private string input;
        private string output;

        public ActionsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                NotifyOfPropertyChange();
            }
        }

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;
                NotifyOfPropertyChange();
            }
        }

        public void SimpleSayHello()
        {
            Output = "Hello from Caliburn.Micro";
        }

        public void SayHello(string name)
        {
            Output = String.Format("Hello {0} from Caliburn.Micro", Input);
        }

        public void AppBarHello()
        {
            Output = "Hello from the App Bar.";
        }
    }
}
