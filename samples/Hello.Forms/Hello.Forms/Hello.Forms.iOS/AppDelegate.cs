using System;
using Caliburn.Micro;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Hello.Forms.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
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
