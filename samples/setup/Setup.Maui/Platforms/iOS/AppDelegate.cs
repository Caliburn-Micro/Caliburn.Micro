using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Setup.Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : Caliburn.Micro.Maui.CaliburnApplication
    {
        public AppDelegate()
        {
            
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
