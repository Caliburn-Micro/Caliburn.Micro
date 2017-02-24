using System;
using Caliburn.Micro;
using Foundation;
using UIKit;

namespace Setup.Forms.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private readonly CaliburnAppDelegate appDelegate = new CaliburnAppDelegate();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();

            LoadApplication(IoC.Get<App>());

            return base.FinishedLaunching(app, options);
        }
    }
}
