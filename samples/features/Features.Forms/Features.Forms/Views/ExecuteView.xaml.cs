using System;

namespace Features.CrossPlatform.Views
{
	public partial class ExecuteView
	{
		public ExecuteView ()
		{
			InitializeComponent ();
		}

	    public void UpdateView()
	    {
	        Output.Text = "View Updated";
	    }
	}
}
