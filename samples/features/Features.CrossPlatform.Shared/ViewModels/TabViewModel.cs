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

            return Task.CompletedTask;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Messages.Add("Activated");

            return Task.CompletedTask;
        }
        
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Messages.Add($"Deactivated, close: {close}");

            return Task.CompletedTask;
        }

        public BindableCollection<string> Messages { get; }
    }
}
