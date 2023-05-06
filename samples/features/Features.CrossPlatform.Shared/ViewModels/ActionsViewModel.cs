using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class ActionsViewModel : Screen
    {
        private string _output;

        public void Clear() => Output = string.Empty;

        public void SimpleSayHello() => Output = "Hello from Caliburn.Micro";

        public void SayHello(string name) => Output = $"Hello {name}";

        public bool CanSayHello(string name) => !string.IsNullOrEmpty(name);

        public Task SayGoodbyeAsync(string name)
        {
            Output = $"Goodbye {name}";

            return TaskHelper.FromResult(true);
        }
        
        public bool CanSayGoodbye(string name) => !string.IsNullOrEmpty(name);

        public string Output
        {
            get { return _output; }
            set { Set(ref _output, value); }
        }
    }
}
