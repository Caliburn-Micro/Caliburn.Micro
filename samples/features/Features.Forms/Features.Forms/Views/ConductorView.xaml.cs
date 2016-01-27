using System;
using Features.CrossPlatform.ViewModels;

namespace Features.CrossPlatform.Views
{
	public partial class ConductorView
	{
		public ConductorView ()
		{
			InitializeComponent ();
		}

	    private ConductorViewModel ViewModel => (ConductorViewModel) BindingContext;

	    public void OnAddTab(object sender, EventArgs e)
	    {
	        ViewModel.AddTab();
	    }

        public void OnCloseTab(object sender, EventArgs e)
        {
            if (ViewModel.CanCloseTab)
                ViewModel.CloseTab();
        }
    }
}
