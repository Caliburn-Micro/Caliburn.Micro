using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class TabViewModel : Screen
    {
        public TabViewModel()
        {
            Messages = new BindableCollection<string>();
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Messages.Add("Initialized");

			return Task.FromResult(true);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Messages.Add("Activated");

			return Task.FromResult(true);
        }

        protected override void OnDeactivate(bool close)
        {
            Messages.Add($"Deactivated, close: {close}");
        }

        public BindableCollection<string> Messages { get; }
    }
}
