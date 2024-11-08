using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ActionsViewModel : Screen
    {
        private string _output;
        public ActionsViewModel()
        {
            Output="Caliburn Micro";
        }

        public void Clear() => Output = string.Empty;

        public void SimpleSayHello() => Output = "Hello from Caliburn.Micro";

        public void SayHello(string username) => Output = $"Hello {username}";

        public bool CanSayHello(string username) => !String.IsNullOrEmpty(username);

        public Task SayGoodbyeAsync(string username)
        {
            Output = $"Goodbye {username}";

            return TaskHelper.FromResult(true);
        }

        public bool CanSayGoodbye(string username) => !String.IsNullOrEmpty(username);

        public string Output
        {
            get { return _output; }
            set { Set(ref _output, value); }
        }
    }
}
