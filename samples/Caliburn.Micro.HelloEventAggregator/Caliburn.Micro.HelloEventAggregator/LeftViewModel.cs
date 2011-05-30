namespace Caliburn.Micro.HelloEventAggregator {
    using System.ComponentModel.Composition;

    [Export(typeof(LeftViewModel))]
    public class LeftViewModel : IHandle<RightEvent> {
        readonly IEventAggregator events;
        readonly IObservableCollection<int> history = new BindableCollection<int>();
        int count = 1;

        [ImportingConstructor]
        public LeftViewModel(IEventAggregator events) {
            this.events = events;
            events.Subscribe(this);
        }

        public IObservableCollection<int> History {
            get { return history; }
        }

        public void Handle(RightEvent message) {
            history.Add(message.Number);
        }

        public void Publish() {
            events.Publish(new LeftEvent {
                Number = count++
            });
        }
    }
}