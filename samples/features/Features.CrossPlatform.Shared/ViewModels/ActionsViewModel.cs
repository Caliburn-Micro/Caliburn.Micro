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

        public void SayHello(string name) => Output = $"Hello {name}";

        public bool CanSayHello(string name) => !String.IsNullOrEmpty(name);

        public Task SayGoodbyeAsync(string name)
        {
            Output = $"Goodbye {name}";

            return TaskHelper.FromResult(true);
        }
        
        public bool CanSayGoodbye(string name) => !String.IsNullOrEmpty(name);

        public string Output
        {
            get { return output; }
            set { this.Set(ref output, value); }
        }
    }
}
