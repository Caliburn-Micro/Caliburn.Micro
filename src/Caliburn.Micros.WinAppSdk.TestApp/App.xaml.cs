using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Caliburn.Micro;
using Caliburn.Micros.WinAppSdk.TestApp.ViewModels;
using Caliburn.Micros.WinAppSdk.TestApp.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Caliburn.Micros.WinAppSdk.TestApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : CaliburnApplication
    {

        private bool isInitialized;
        private static readonly ILog Log = LogManager.GetLog(typeof(CaliburnApplication));
        private WinRTContainer container;
        protected Frame RootFrame { get; private set; }


        /// <summary>
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime()
        {
            AssemblySourceCache.Install();
            var ass = SelectAssemblies();
            AssemblySource.Instance.AddRange(ass);
            Log.Debug("Start Runtime Prepare application");
            //PrepareApplication();
            Log.Debug($"Assemblies in AssemblySource: {AssemblySource.Instance.Count}");
            Log.Debug("Start Runtime Prepare configure");
            Configure();
            Log.Debug("Start Runtime IoC GetInstance");

            IoC.GetInstance = GetInstance;
            Log.Debug("Start Runtime IoC GetAllInstances");
            IoC.GetAllInstances = GetAllInstances;
            Log.Debug("Start Runtime BuildUp");
            IoC.BuildUp = BuildUp;
            Log.Debug("Buildup done");
        }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            LogManager.GetLog = type => new DebugLog(type);
            Initialize();
            this.InitializeComponent();
        }
        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected virtual void Configure()
        {
            Log.Debug("Configure");
            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container.PerRequest<HomeViewModel>();
            container.PerRequest<HomeView>();
            container.BuildUp(this);

            Log.Debug($"Assemblies {AssemblySource.AssemblyCount}");

        }
        protected override Frame CreateApplicationFrame()
        {
            Log.Debug("CreateApplicationFrame");
            Frame rootFrame = ((MainWindow)m_window).RootFrame;
            return rootFrame;
        }
        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
            var s = GetType().GetTypeInfo().Assembly;
            List<Assembly> lstAssembly = new List<Assembly>();
            lstAssembly.Add(s);
            return lstAssembly;
        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
            DisplayRootView<HomeView>();
        }
        
        
         private Window m_window;

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <param name="viewType">The view type to navigate to.</param>
        /// <param name="paramter">The object parameter to pass to the target.</param>
        protected void DisplayRootView(Type viewType, object paramter = null)
        {
            Log.Debug("DisplayRootView");
            Initialize();

            PrepareViewFirst();
            //var navService = container.GetInstance<INavigationService>();
            Log.Debug($"Assemblies in AssemblySource: {AssemblySource.Instance.Count}");
            //navService.NavigateToViewModel<HomeViewModel>();
        }

        /// <summary>
        /// Allows you to trigger the creation of the RootFrame from Configure if necessary.
        /// </summary>
        protected virtual void PrepareViewFirst()
        {
            if (RootFrame != null)
                return;

            RootFrame = CreateApplicationFrame();
            PrepareViewFirst(RootFrame);
        }


        /// <summary>
        /// Override this to register a navigation service.
        /// </summary>
        /// <param name="rootFrame">The root frame of the application.</param>
        protected virtual void PrepareViewFirst(Frame rootFrame)
        {
            Log.Debug("PrepareViewFirst");
            container.RegisterNavigationService(rootFrame);
        }

    }
}
