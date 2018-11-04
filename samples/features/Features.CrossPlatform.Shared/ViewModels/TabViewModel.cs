using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class TabViewModel : Screen
    {
        private readonly Random random = new Random();

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

        public override async Task<bool> CanCloseAsync(CancellationToken cancellationToken)
        {
            var delay = random.Next(5) + 1;
            var canClose = random.Next(2) == 0;

            Messages.Add($"Delaying {delay} seconds and allowing close: {canClose}");

            await Task.Delay(TimeSpan.FromSeconds(delay));

            return canClose;
        }

        public BindableCollection<string> Messages { get; }
    }
}
