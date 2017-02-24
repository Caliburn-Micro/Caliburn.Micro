using System;
using Caliburn.Micro;
using Foundation;
using UIKit;

namespace Features.CrossPlatform
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        private readonly CaliburnAppDelegate appDelegate = new CaliburnAppDelegate();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();

            LoadApplication(IoC.Get<FormsApp>());

            return base.FinishedLaunching(app, options);
        }
    }
}
