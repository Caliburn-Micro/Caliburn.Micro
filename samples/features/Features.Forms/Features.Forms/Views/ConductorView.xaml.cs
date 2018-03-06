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

	    public async void OnAddTab(object sender, EventArgs e)
	    {
	        await ViewModel.AddTabAsync();
	    }

        public void OnCloseTab(object sender, EventArgs e)
        {
            if (ViewModel.CanCloseTab)
                ViewModel.CloseTab();
        }
    }
}
