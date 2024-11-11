using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ActionsViewModel : Screen
    {
        private string output;

        public void Clear() => Output = String.Empty;

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
            get { return output; }
            set { Set(ref output, value); }
        }
    }
}
