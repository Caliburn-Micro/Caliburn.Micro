namespace Caliburn.Micro.HelloEventAggregator {
    using System.ComponentModel.Composition;

    [Export(typeof(RightViewModel))]
    public class RightViewModel : IHandle<LeftEvent> {
        readonly IEventAggregator events;
        readonly IObservableCollection<int> history = new BindableCollection<int>();
        int count = 1;

        [ImportingConstructor]
        public RightViewModel(IEventAggregator events) {
            this.events = events;
            events.Subscribe(this);
        }

        public IObservableCollection<int> History {
            get { return history; }
        }

        public void Handle(LeftEvent message) {
            history.Add(message.Number);
        }

        public void Publish() {
            events.Publish(new RightEvent {
                Number = count++
            });
        }
    }
}