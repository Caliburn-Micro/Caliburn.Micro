using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel;

namespace Caliburn.Micro
{
    /// <summary>
    /// Encapsulates the app and its available services.
    /// </summary>
    public abstract class CaliburnApplication : Application
    {
        private bool isInitialized;
        private static readonly ILog Log = LogManager.GetLog(typeof(CaliburnApplication));

        /// <summary>
        /// The root frame of the application.
        /// </summary>
        protected Frame RootFrame { get; private set; }

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
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime()
        {
            AssemblySourceCache.Install();
            AssemblySource.AddRange(SelectAssemblies());
            Log.Debug("Start Runtime Prepare application");
            PrepareApplication();
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


        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Log.Debug("OnLaunched");
            base.OnLaunched(args);
            PlatformProvider.Current = new XamlPlatformProvider();
        }


        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected virtual void PrepareApplication()
        {
            //Resuming += OnResuming;
            //Suspending += OnSuspending;
            UnhandledException += OnUnhandledException;
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected virtual void Configure()
        {
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {GetType().GetTypeInfo().Assembly};
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
            return new[] {System.Activator.CreateInstance(service)};
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected virtual void BuildUp(object instance)
        {
        }

        /// <summary>
        /// Override this to add custom behavior when the application transitions from Suspended state to Running state.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnResuming(object sender, object e)
        {
        }

        /// <summary>
        /// Override this to add custom behavior when the application transitions to Suspended state from some other state.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnSuspending(object sender, SuspendingEventArgs e)
        {
        }

        /// <summary>
        /// Override this to add custom behavior for unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Log.Error(e.Exception);
        }

        /// <summary>
        /// Creates the root frame used by the application.
        /// </summary>
        /// <returns>The frame.</returns>
        protected virtual Frame CreateApplicationFrame()
        {
            return new Frame();
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
        }

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
            Log.Debug($"Made it past PrepareViewFirst RootFrame is null {RootFrame == null}");
            Log.Debug($"ViewType is null {viewType == null} {viewType}");
            Log.Debug($"RootFrame is null {RootFrame == null}");

            //RootFrame.Navigate(viewType, paramter);
            var myView = IoC.GetInstance(viewType, "") as UserControl;
            var viewModel = ViewModelLocator.LocateForView(myView);
            Log.Debug($"myView is null {myView == null}");
            //myView.DataContext = viewModel;
            ViewModelBinder.Bind(viewModel, myView, null);
            RootFrame.Content = myView;
            Log.Debug($"RootFrame.Content is null {RootFrame.Content == null}");
            // Seems stupid but observed weird behaviour when resetting the Content
            //if (Window.Current.Content == null)
            //    Window.Current.Content = RootFrame;
            Log.Debug("Past Window.Current.Content");
            //Window.Current.Activate();
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

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected async Task DisplayRootViewForAsync(Type viewModelType, CancellationToken cancellationToken)
        {
            Log.Debug("Display root view for async initialize");
            Initialize();

            Log.Debug($"Ioc get instance {viewModelType.ToString()}");
            var viewModel = IoC.GetInstance(viewModelType, null);
            Log.Debug("ViewLocator Locate for model");
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            Log.Debug("Display root view for async bind");
            ViewModelBinder.Bind(viewModel, view, null);

            if (viewModel is IActivate activator)
                await activator.ActivateAsync(cancellationToken);
            Log.Debug("Display root view for async current");

            Window.Current.Content = view;
            Log.Debug("Display root view for async activate");

            Window.Current.Activate();
            Log.Debug("Display root view for async done");

        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected Task DisplayRootViewForAsync(Type viewModelType) => DisplayRootViewForAsync(viewModelType, CancellationToken.None);

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected Task DisplayRootViewForAsync<T>(CancellationToken cancellationToken)
        {
            Log.Debug("DisplayRootViewForAsync start");
            return DisplayRootViewForAsync(typeof(T), cancellationToken);
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected Task DisplayRootViewForAsync<T>() => DisplayRootViewForAsync<T>(CancellationToken.None);
    }
}
