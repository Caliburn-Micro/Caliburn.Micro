using System;
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
            eventAggregator.Subscribe(this);
        }

        public void Unsubscribe()
        {
            eventAggregator.Unsubscribe(this);
        }

        public void Handle(SimpleMessage message)
        {
            Messages.Add(message);
        }

        public BindableCollection<SimpleMessage> Messages { get; }
    }
}
