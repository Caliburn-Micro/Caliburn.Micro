namespace Caliburn.Micro.KeyBinding 
{
    /// <summary>
    /// The Input code is based on the sample code provided by BladeWise at http://caliburnmicro.codeplex.com/discussions/243905    
    /// </summary>
    public class ShellViewModel : Screen
    {
        private string enterMessage;
        public string EnterMessage
        {
            get { return enterMessage; }
            set
            {
                this.enterMessage = value;
                this.NotifyOfPropertyChange( () => EnterMessage );
            }
        }

        public ShellViewModel()
        {
            this.EnterMessage = "Write something in the Text Box and press enter.";
        }

        public void EnterPressed()
        {
            this.EnterMessage = "Enter has been pressed";
        }
    }
}