namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Inherit from this class in order to customize the configuration of the framework.
    /// </summary>
    public abstract class BootstrapperBase {
        readonly bool useApplication;
        bool isInitialized;

        /// <summary>
        /// The application.
        /// </summary>
        protected Application Application { get; private set; }

        /// <summary>
        /// Creates an instance of the bootstrapper.
        /// </summary>
        /// <param name="useApplication">Set this to false when hosting Caliburn.Micro inside and Office or WinForms application. The default is true.</param>
        protected BootstrapperBase(bool useApplication = true) {
            this.useApplication = useApplication;
        }

        /// <summary>
        /// Start the framework.
        /// </summary>
        public void Start() {
            if(isInitialized) {
                return;
            }

            isInitialized = true;

            if(Execute.InDesignMode) {
                try {
                    StartDesignTime();
                }catch {
                    //if something fails at design-time, there's really nothing we can do...
                    isInitialized = false;
                    throw;
                }
            }
            else {
                StartRuntime();
            }
        }

        /// <summary>
        /// Called by the bootstrapper's constructor at design time to start the framework.
        /// </summary>
        protected virtual void StartDesignTime() {
            AssemblySource.Instance.Clear();
            AssemblySource.Instance.AddRange(SelectAssemblies());

            Configure();
            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;
        }

        /// <summary>
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime() {
            Execute.InitializeWithDispatcher();
            EventAggregator.DefaultPublicationThreadMarshaller = Execute.OnUIThread;

            EventAggregator.HandlerResultProcessing = (target, result) => {
#if !SILVERLIGHT || SL5 || WP8
                var task = result as System.Threading.Tasks.Task;
                if (task != null) {
                    result = new IResult[] {task.AsResult()};
                }
#endif

                var coroutine = result as IEnumerable<IResult>;
                if (coroutine != null) {
                    var viewAware = target as IViewAware;
                    var view = viewAware != null ? viewAware.GetView() : null;
                    var context = new ActionExecutionContext { Target = target, View = (DependencyObject)view };

                    Coroutine.BeginExecute(coroutine.GetEnumerator(), context);
                }
            };

            AssemblySource.Instance.AddRange(SelectAssemblies());

            if (useApplication) {
                Application = Application.Current;
                PrepareApplication();
            }

            Configure();
            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;
        }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected virtual void PrepareApplication() {
            Application.Startup += OnStartup;
#if SILVERLIGHT
            Application.UnhandledException += OnUnhandledException;
#else
            Application.DispatcherUnhandledException += OnUnhandledException;
#endif
            Application.Exit += OnExit;
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected virtual void Configure() { }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies() {
            if (Execute.InDesignMode) {
                var appDomain = AppDomain.CurrentDomain;
                var assemblies = appDomain.GetType()
                                     .GetMethod("GetAssemblies")
                                     .Invoke(appDomain, null) as Assembly[] ?? new Assembly[] { };

                var applicationAssembly = assemblies.LastOrDefault(ContainsApplicationClass);
                return applicationAssembly == null ? new Assembly[] { } : new[] { applicationAssembly };
            }

#if SILVERLIGHT
            var entryAssembly = Application.Current.GetType().Assembly;
#else
            var entryAssembly = Assembly.GetEntryAssembly();
#endif
            return entryAssembly == null ? new Assembly[] { } : new[] { entryAssembly };
        }

        private static bool ContainsApplicationClass(Assembly assembly) {
            var containsApp = false;

            try {
#if SILVERLIGHT && !WINDOWS_PHONE
                containsApp = !assembly.IsDynamic && assembly.GetExportedTypes().Any(t => t.IsSubclassOf(typeof(Application)));
#else
                containsApp = assembly.EntryPoint != null && assembly.GetExportedTypes().Any(t => t.IsSubclassOf(typeof(Application)));
#endif
            }
            catch {
            }

            return containsApp;
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected virtual object GetInstance(Type service, string key) {
#if NET
            if (service == typeof(IWindowManager))
                service = typeof(WindowManager);
#endif

            return Activator.CreateInstance(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected virtual IEnumerable<object> GetAllInstances(Type service) {
            return new[] { Activator.CreateInstance(service) };
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected virtual void BuildUp(object instance) { }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected virtual void OnStartup(object sender, StartupEventArgs e) { }

        /// <summary>
        /// Override this to add custom behavior on exit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnExit(object sender, EventArgs e) { }

#if SILVERLIGHT
        /// <summary>
        /// Override this to add custom behavior for unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) { }
#else
        /// <summary>
        /// Override this to add custom behavior for unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) { }
#endif
            
#if SILVERLIGHT && !WINDOWS_PHONE
        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        protected void DisplayRootViewFor(Type viewModelType) {
            var viewModel = IoC.GetInstance(viewModelType, null);
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if(activator != null)
                activator.Activate();

            Mouse.Initialize(view);
            Application.RootVisual = view;
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        protected void DisplayRootViewFor<TViewModel>() {
            DisplayRootViewFor(typeof(TViewModel));
        }
#elif NET
        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="settings">The optional window settings.</param>
        protected void DisplayRootViewFor(Type viewModelType, IDictionary<string, object> settings = null) {
            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowWindow(IoC.GetInstance(viewModelType, null), null, settings);
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        /// <param name="settings">The optional window settings.</param>
        protected void DisplayRootViewFor<TViewModel>(IDictionary<string, object> settings = null) {
            DisplayRootViewFor(typeof(TViewModel), settings);
        }
#endif
    }

#if !WINDOWS_PHONE
    /// <summary>
    /// A strongly-typed version of <see cref="BootstrapperBase"/> that specifies the type of root model to create for the application.
    /// </summary>
    /// <typeparam name="TRootModel">The type of root model for the application.</typeparam>
    public class Bootstrapper<TRootModel> : BootstrapperBase {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper&lt;TRootModel&gt;"/> class.
        /// </summary>
        public Bootstrapper() : base(true) {
            Start();
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e) {
            DisplayRootViewFor<TRootModel>();
        }
    }
#endif
}
