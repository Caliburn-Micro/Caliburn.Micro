using Foundation;

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
