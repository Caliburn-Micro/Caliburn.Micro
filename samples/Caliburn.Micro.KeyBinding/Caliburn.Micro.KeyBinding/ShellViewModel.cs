namespace Caliburn.Micro.KeyBinding {
    /// <summary>
    ///   The Input code is based on the sample code provided by BladeWise at http://caliburnmicro.codeplex.com/discussions/243905
    /// </summary>
    public class ShellViewModel : Screen {
        string enterMessage;

        public string EnterMessage {
            get { return enterMessage; }
            set {
                enterMessage = value;
                NotifyOfPropertyChange(() => EnterMessage);
            }
        }

        public ShellViewModel() {
            EnterMessage = "Write something in the Text Box and press enter.";
        }

        public void EnterPressed() {
            EnterMessage = "Enter has been pressed";
        }

        public void CtrlEnterPressed()
        {
           EnterMessage = "Ctrl+Enter has been pressed";
        }
    }
}