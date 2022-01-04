using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Setup.Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : Caliburn.Micro.Maui.CaliburnApplicationDelegate
    {
        public AppDelegate()
        {
            Initialize();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
