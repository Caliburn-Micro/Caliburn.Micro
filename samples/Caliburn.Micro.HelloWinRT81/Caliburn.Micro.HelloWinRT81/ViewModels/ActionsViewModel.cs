using System;
using System.Threading.Tasks;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ActionsViewModel : ViewModelBase
    {
        string input;
        string input2;
        string output;

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
                this.Set(ref output, value);
            }
        }

        public void SimpleSayHello()
        {
            Output = "Hello from Caliburn.Micro";
        }

        public async Task AsyncSayHelloAsync()
        {
            await Task.Delay(0);

            Output = "Hello from Caliburn.Micro (async)";
        }

        public void SayHello(string name)
        {
            Output = String.Format("Hello {0} from Caliburn.Micro", name);
        }

        public bool CanSayHello(string name)
        {
            return !String.IsNullOrEmpty(name);
        }

        public async Task SayHello2Async(string name)
        {
            await Task.Delay(0);

            Output = String.Format("Hello {0} from Caliburn.Micro (async)", name);
        }

        // Notice that the guard method is sync and is missing the Async suffix.
        public bool CanSayHello2(string name)
        {
            return !String.IsNullOrEmpty(name);
        }

        public void AppBarHello()
        {
            Output = "Hello from the App Bar.";
        }
    }
}
