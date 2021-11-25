using Caliburn.Micro.WinAppSdk.Test.ViewModels;
using Caliburn.Micro.WinAppSdk.Test.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Caliburn.Micro.WinAppSdk.Test
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : CaliburnApplication
    {
        private WinRTContainer container;

        public App()
        {
            Trace.WriteLine("App start");
            LogManager.GetLog = type => new DebugLogger(type);
            Initialize();
            InitializeComponent();
        }

        protected override void Configure()
        { 
            Trace.WriteLine("Configure");
            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container.PerRequest<HomeViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            Trace.WriteLine("PrepareViewFirst");
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Trace.WriteLine("OnLaunched");
            DisplayRootView<HomeView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
