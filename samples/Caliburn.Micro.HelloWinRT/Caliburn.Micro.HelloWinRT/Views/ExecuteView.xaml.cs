using System;

namespace Caliburn.Micro.WinRT.Sample.Views
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
