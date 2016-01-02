using System;
using Caliburn.Micro;
using Features.CrossPlatform.Messages;

namespace Features.CrossPlatform.ViewModels.Events
{
    public class EventSourceViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        private string text;

        public EventSourceViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public string Text
        {
            get { return text; }
            set { this.Set(ref text, value); }
        }

        public void Publish()
        {
            eventAggregator.PublishOnUIThread(new SimpleMessage(Text));
        }
    }
}
