using System;
using System.Windows.Forms;

namespace Caliburn.Micro.WinFormsInterop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var bootstrapper = new InteropBootstrapper(elementHost1);
            bootstrapper.Start();

            base.OnLoad(e);
        }
    }
}
