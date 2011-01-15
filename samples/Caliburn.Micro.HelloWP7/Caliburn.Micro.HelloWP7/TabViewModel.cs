namespace Caliburn.Micro.HelloWP7 {
    using System;
    using System.Windows;
    using Microsoft.Phone.Tasks;

    [SurviveTombstone]
    public class TabViewModel : Screen, ILaunchChooser<PhoneNumberResult> {
        string text;

        [SurviveTombstone]
        public string Text {
            get { return text; }
            set {
                text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public event EventHandler<TaskLaunchEventArgs> TaskLaunchRequested = delegate { };

        public void Handle(PhoneNumberResult message) {
            MessageBox.Show("The result was " + message.TaskResult, DisplayName, MessageBoxButton.OK);
        }

        public void Choose() {
            TaskLaunchRequested(this, TaskLaunchEventArgs.For<PhoneNumberChooserTask>());
        }
    }
}