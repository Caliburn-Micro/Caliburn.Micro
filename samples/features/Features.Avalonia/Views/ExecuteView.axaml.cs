using Avalonia.Controls;

namespace Features.CrossPlatform.Views
{
    public partial class ExecuteView : UserControl
    {

        public ExecuteView()
        {
            InitializeComponent();
        }

        public void UpdateView()
        {
            Output.Text = "View Updated";
        }
    }
}
