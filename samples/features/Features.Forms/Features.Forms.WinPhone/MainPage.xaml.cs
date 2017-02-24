using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Features.CrossPlatform
{
	public partial class MainPage
	{
		public MainPage ()
		{
			InitializeComponent ();

			SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

			Xamarin.Forms.Forms.Init ();
            LoadApplication(IoC.Get<FormsApp>());
        }
	}
}
