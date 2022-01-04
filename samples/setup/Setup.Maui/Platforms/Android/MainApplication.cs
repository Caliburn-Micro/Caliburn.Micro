using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Setup.Maui
{
    [Application]
    public class MainApplication : Caliburn.Micro.Maui.CaliburnApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            Initialize();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override void Configure()
        {
            base.Configure();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new List<Assembly>() { typeof(App).Assembly };
        }
    }
}
