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
        protected NavigationView RootFrame { get; private set; }

        /// <summary>
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime()
        {
            AssemblySourceCache.Install();
            var ass = SelectAssemblies();
            AssemblySource.AddRange(ass);
            Log.Debug("Start Runtime Prepare application");
            //  Log.Debug($"Assemblies {AssemblySource.AssemblyCount}");
            //PrepareApplication();
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
            container = new WinRTContainer();

            container.RegisterWinRTServices();
            
            container.PerRequest<ShellViewModel>();
            container.PerRequest<HomeViewModel>();
            container.PerRequest<HomeView>();
            container.PerRequest<ShellView>();
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { GetType().GetTypeInfo().Assembly };
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

            Log.Info("baseExtractTypes");
            var baseExtractTypes = AssemblySourceCache.ExtractTypes;

            AssemblySourceCache.ExtractTypes = assembly =>
            {
                var baseTypes = baseExtractTypes(assembly);
                var elementTypes = assembly.GetExportedTypes()
                    .Where(t => typeof(UIElement).IsAssignableFrom(t));

                return baseTypes.Union(elementTypes);
            };

            AssemblySource.Instance.Refresh();


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
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            DisplayRootView<ShellView>();
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
        }

        private MainWindow m_window;

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

           // RootFrame(DisplayRootView<ShellView>());

          //  Window.Current.Activate();
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
        protected virtual void PrepareViewFirst(NavigationView rootFrame)
        {
        }

        /// <summary>
        /// Creates the root frame used by the application.
        /// </summary>
        /// <returns>The frame.</returns>
        protected virtual NavigationView CreateApplicationFrame()
        {
            m_window = new MainWindow();
            m_window.Activate();
            return m_window.RootFrame;
        }
    }
}
