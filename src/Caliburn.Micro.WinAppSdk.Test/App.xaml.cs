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
        private WinRTContainer container;
        private bool isInitialized;

        public App()
        {
            Trace.WriteLine("App start");
            LogManager.GetLog = type => new DebugLog(type);
            Initialize();
            Trace.WriteLine("Initialize done");
            InitializeComponent();
            Trace.WriteLine("Initialize component done");
        }

        /// <summary>
        /// Start the framework.
        /// </summary>
        protected void Initialize()
        {
            Trace.WriteLine("Initialize");
            if (isInitialized)
            {
                Trace.WriteLine("Already initialized");
                return;
            }

            isInitialized = true;

            Trace.WriteLine("PlatformProvider.Current");
            PlatformProvider.Current = new XamlPlatformProvider();
            Trace.WriteLine("baseExtractTypes");
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
            Trace.WriteLine("Intitialize done");
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
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { GetType().GetTypeInfo().Assembly };
        }


        /// <summary>
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime()
        {
            AssemblySourceCache.Install();
            AssemblySource.AddRange(SelectAssemblies());
            Trace.WriteLine("Start Runtime Prepare application");
            //PrepareApplication();
            Trace.WriteLine("Start Runtime Prepare configure");
            Configure();
            Trace.WriteLine("Start Runtime IoC GetInstance");

            IoC.GetInstance = GetInstance;
            Trace.WriteLine("Start Runtime IoC GetAllInstances");
            IoC.GetAllInstances = GetAllInstances;
            Trace.WriteLine("Start Runtime BuildUp");
            IoC.BuildUp = BuildUp;
            Trace.WriteLine("Buildup done");
        }


        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            PlatformProvider.Current = new XamlPlatformProvider();
        }
        protected void Configure()
        {
            Trace.WriteLine("Configure");
            container = new WinRTContainer();
            Trace.WriteLine("Configure after new");

            container.RegisterWinRTServices();
            Trace.WriteLine("Configure after register");

            container.PerRequest<ShellViewModel>();
            container.PerRequest<HomeViewModel>();
            container.PerRequest<ShellView>();
            container.PerRequest<HomeView>();
            Trace.WriteLine("Configure after HomeViewModel");


        }

        //protected void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        //{
        //    base.OnUnhandledException(sender, e);
        //    Trace.WriteLine("Unhandled exception");
        //    Trace.WriteLine(e.Message);
        //    Trace.WriteLine(e.Exception.Source);
        //    Trace.WriteLine(e.Exception.StackTrace);
        //}
        protected void PrepareViewFirst(Frame rootFrame)
        {
            Trace.WriteLine("PrepareViewFirst");
            container.RegisterNavigationService(rootFrame);
            Trace.WriteLine("PrepareViewFirst registered navigation service");
        }

        //protected async override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        //{
        //    Trace.WriteLine("OnLaunched");
        //    await DisplayRootViewForAsync<ShellView>();
        //    Trace.WriteLine("On launched shell view displayed");
        //}

        protected  object GetInstance(Type service, string key)
        {
            Trace.WriteLine("GetInstance");
            return container.GetInstance(service, key);
        }

        protected  IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected void BuildUp(object instance)
        {
            Trace.WriteLine("BuildUp");

            container.BuildUp(instance);
            Trace.WriteLine("BuildUp done");

        }
    }
}
