using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

namespace Caliburn.Micro
{
    /// <summary>
    /// Base class for bootstrapping the application.
    /// </summary>
    public class BootstrapperBase
    {
        static readonly ILog Log = LogManager.GetLog(typeof(BootstrapperBase));
        bool isInitialized;

        /// <summary>
        /// The application.
        /// </summary>
        protected Application Application { get; set; }

        /// <summary>
        /// Initialize the framework.
        /// </summary>
        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            PlatformProvider.Current = new XamlPlatformProvider();

            var baseExtractTypes = AssemblySourceCache.ExtractTypes;

            AssemblySourceCache.ExtractTypes = assembly =>
            {
                var baseTypes = baseExtractTypes(assembly);
                var elementTypes = assembly.GetExportedTypes()
                    .Where(t => typeof(Control).IsAssignableFrom(t));

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
            Coroutine.Completed += (s, e) =>
            {
                if (e.Error != null)
                {
                    CoroutineException(s, e);
                }
            };
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
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime()
        {
            AssemblySourceCache.Install();
            AssemblySource.AddRange(SelectAssemblies());

            Application = Application.Current;
            PrepareApplication();

            Configure();
            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;
        }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected virtual void PrepareApplication()
        {
            if (Application.ApplicationLifetime is IControlledApplicationLifetime controlledApplicationLifetime)
            {
                controlledApplicationLifetime.Startup += OnStartup;

                controlledApplicationLifetime.Exit += OnExit;
            }
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected virtual void Configure()
        {
        }

        /// <summary>
        /// Override this to catch unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Error(e.Exception);
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { GetType().Assembly };
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected virtual object GetInstance(Type service, string key)
        {
            if (service == typeof(IWindowManager))
                service = typeof(WindowManager);

            return Activator.CreateInstance(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected virtual IEnumerable<object> GetAllInstances(Type service)
        {
            return new[] { Activator.CreateInstance(service) };
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected virtual void BuildUp(object instance)
        {
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected virtual void OnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
        }

        /// <summary>
        /// Override this to add custom behavior on exit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="settings">The optional window settings.</param>
        protected async Task DisplayRootViewForAsync(Type viewModelType, IDictionary<string, object> settings = null)
        {
            Log.Info("Displaying RootView: {0}", viewModelType.FullName);
            var windowManager = IoC.Get<IWindowManager>();
            var window = await windowManager.CreateWindowAsync(IoC.GetInstance(viewModelType, null), null, settings);
            if (Application.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow == null)
                desktop.MainWindow = window;
            window.Show();
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        /// <param name="settings">The optional window settings.</param>
        protected async Task DisplayRootViewFor<TViewModel>(IDictionary<string, object> settings = null)
        {
            await DisplayRootViewForAsync(typeof(TViewModel), settings);
        }

        /// <summary>
        /// Handles coroutine exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void CoroutineException(object sender, ResultCompletionEventArgs e)
        {
            Log.Error(e.Error ?? new Exception("Coroutine Error"));
        }
    }
}
