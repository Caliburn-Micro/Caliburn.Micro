using System;
using Caliburn.Micro;

namespace Scenario.KeyBinding.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string enterMessage;

        public ShellViewModel()
        {
            EnterMessage = "Write something in the Text Box and press enter.";
        }

        public string EnterMessage
        {
            get { return enterMessage; }
            set
            {
                enterMessage = value;
                NotifyOfPropertyChange(() => EnterMessage);
            }
        }
        
        public void EnterPressed()
        {
            EnterMessage = "Enter has been pressed";
        }

        public void CtrlEnterPressed()
        {
            EnterMessage = "Ctrl+Enter has been pressed";
        }
    }
}
