using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Features.CrossPlatform.Messages;

namespace Features.CrossPlatform.ViewModels.Events
{
    public class EventDestinationViewModel : Screen, IHandle<SimpleMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public EventDestinationViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Messages = new BindableCollection<SimpleMessage>();
        }

        public void Subscribe()
        {
            eventAggregator.SubscribeOnPublishedThread(this);
        }

        public void Unsubscribe()
        {
            eventAggregator.Unsubscribe(this);
        }

        public Task HandleAsync(SimpleMessage message, CancellationToken cancellationToken)
        {
            Messages.Add(message);

            return Task.FromResult(true);
        }

        public BindableCollection<SimpleMessage> Messages { get; }
    }
}
