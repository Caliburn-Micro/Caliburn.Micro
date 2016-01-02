using System;

namespace Features.CrossPlatform.Views
{
    public sealed partial class ExecuteView
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
