namespace Caliburn.Micro.HelloWP7 {
    using System.Windows;
    using Microsoft.Phone.Tasks;

    public class TabViewModel : Screen, IHandle<TaskCompleted<PhoneNumberResult>> {
        string text;
        readonly IEventAggregator events;

        public TabViewModel(IEventAggregator events) {
            this.events = events;
            this.events.Subscribe(this);
        }

        public string Text {
            get { return text; }
            set {
                text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public void Choose() {
            //specifying an id is only necessary if multiple instances could handle the same task
            //in this case, we have 5 instances of TabViewModel, so the id needs to be passed back in order to determine the original sender
            events.RequestTask<PhoneNumberChooserTask>(id:DisplayName);
        }

        public void Handle(TaskCompleted<PhoneNumberResult> message) {
            if(message.Id.Equals(DisplayName))
                MessageBox.Show("The result was " + message.Result.TaskResult, DisplayName, MessageBoxButton.OK);
        }
    }
}