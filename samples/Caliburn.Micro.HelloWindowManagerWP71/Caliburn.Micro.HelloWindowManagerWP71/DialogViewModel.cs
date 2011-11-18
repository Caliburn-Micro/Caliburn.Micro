namespace Caliburn.Micro.HelloWindowManagerWP71 {
    using System;

    public class DialogViewModel : Screen {
        string someTextInput;
        DateTime? someDateInput;

        public string Message { get; set; }

        public string SomeTextInput {
            get { return someTextInput; }
            set {
                someTextInput = value;
                NotifyOfPropertyChange(() => SomeTextInput);
            }
        }

        public DateTime? SomeDateInput {
            get { return someDateInput; }
            set {
                someDateInput = value;
                NotifyOfPropertyChange(() => SomeDateInput);
            }
        }

        public string Result { get; set; }

        public void Confirm() {
            Result = SomeTextInput + "\n" + SomeDateInput.ToString();
            TryClose();
        }

        public void Cancel() {
            Result = null;
            TryClose();
        }
    }
}