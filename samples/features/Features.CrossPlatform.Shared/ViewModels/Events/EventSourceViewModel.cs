using System.Threading;
using Caliburn.Micro;
using Features.CrossPlatform.Messages;

namespace Features.CrossPlatform.ViewModels.Events
{
    public class EventSourceViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        private string _text;

        public EventSourceViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            _text = string.Empty;
        }

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public async void Publish()
        {
            await eventAggregator.PublishOnUIThreadAsync(new SimpleMessage(Text), CancellationToken.None);
        }
    }
}
