using Caliburn.Micro.WinAppSdk.Test.ViewModels;
using Caliburn.Micro.WinAppSdk.Test.Views;
using Microsoft.UI.Dispatching;
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
using System.Reflection;
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
    public partial class App : Application
    {

        private bool isInitialized;
        private static readonly ILog Log = LogManager.GetLog(typeof(CaliburnApplication));
        private WinRTContainer container;

        /// <summary>
        /// The root frame of the application.
        /// </summary>
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
        /// Called by the bootstrapper's constructor at design time to start the framework.
        /// </summary>
        protected virtual void StartDesignTime()
        {
            AssemblySource.Instance.Clear();
            AssemblySource.AddRange(SelectAssemblies());

            Configure();
            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;
        }

        /// <summary>
        /// Start the framework.
        /// </summary>
        protected void Initialize()
        {
            Log.Info("Initialize");
            if (isInitialized)
            {
                Log.Debug("Already initialized");
                return;
            }

            isInitialized = true;

            Log.Info("PlatformProvider.Current");
            PlatformProvider.Current = new XamlPlatformProvider();


            if (Execute.InDesignMode)
            {
                try
                {
                    StartDesignTime();
                }
                catch
                {
                    //if something fails at design-time, there's really nothing we can do...
                    isInitialized = false;
                    throw;
                }
            }
            else
            {
                StartRuntime();
            }
            Log.Debug("Intitialize done");
        }


        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected virtual object GetInstance(Type service, string key)
        {
            return System.Activator.CreateInstance(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected virtual IEnumerable<object> GetAllInstances(Type service)
        {
            return new[] { System.Activator.CreateInstance(service) };
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected virtual void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            DisplayRootView<HomeView>();
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <typeparam name="T">The view type to navigate to.</typeparam>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        protected void DisplayRootView<T>(object parameter = null)
        {
            Log.Info("DisplayRootView start");
            DisplayRootView(typeof(T), parameter);
            Log.Info("DisplayRootView end");
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

            var homeView = container.GetInstance<HomeView>();
            var homeViewModel = container.GetInstance<HomeViewModel>();
            homeView.DataContext = homeViewModel;
            Log.Debug($"Assemblies in AssemblySource: {AssemblySource.Instance.Count}");
            this.RootFrame.Content = homeView;
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

        /// <summary>
        /// Creates the root frame used by the application.
        /// </summary>
        /// <returns>The frame.</returns>
        protected virtual Frame CreateApplicationFrame()
        {
            Log.Debug("CreateApplicationFrame");
            m_window = new Window();
            m_window.Activated += Window_Activated;
            Frame rootFrame = new Frame();
            rootFrame.Name = "RootFrame";
            m_window.Content = rootFrame;
            m_window.Activate();
            return rootFrame;
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            Log.Debug("Window loaded");
        }
    }
}
