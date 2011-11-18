namespace Caliburn.Micro.HelloWP71 {
    using System.Windows;
    using Microsoft.Phone.Tasks;

    public class TabViewModel : Screen, IHandle<TaskCompleted<PhoneNumberResult>> {
        string text;
        readonly IEventAggregator events;

        public TabViewModel(IEventAggregator events) {
            this.events = events;
        }

        public string Text {
            get { return text; }
            set {
                text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public void Choose() {
            events.RequestTask<PhoneNumberChooserTask>();
        }

        public void Handle(TaskCompleted<PhoneNumberResult> message) {
            MessageBox.Show("The result was " + message.Result.TaskResult, DisplayName, MessageBoxButton.OK);
        }

        protected override void OnActivate() {
            events.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close) {
            events.Unsubscribe(this);
            base.OnDeactivate(close);
        }
    }
}