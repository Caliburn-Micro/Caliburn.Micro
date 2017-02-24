using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Caliburn.Micro;

namespace Setup.Forms.Droid
{
    [Activity(Label = "Setup.Forms", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(IoC.Get<App>());
        }
    }
}

