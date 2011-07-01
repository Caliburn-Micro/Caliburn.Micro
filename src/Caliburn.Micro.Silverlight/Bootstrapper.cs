namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Instantiate this class in order to configure the framework.
    /// </summary>
    public class Bootstrapper
    {
        readonly bool useApplication;

        /// <summary>
        /// The application.
        /// </summary>
        public Application Application { get; protected set; }


        /// <summary>
        /// Creates an instance of the bootstrapper.
        /// </summary>
        public Bootstrapper(bool useApplication = true) {
            this.useApplication = useApplication;

            if (Execute.InDesignMode)
                StartDesignTime();
            else StartRuntime();
        }

        /// <summary>
        /// Called by the bootstrapper's constructor at design time to start the framework.
        /// </summary>
        protected virtual void StartDesignTime() {}

        /// <summary>
        /// Called by the bootstrapper's constructor at runtime to start the framework.
        /// </summary>
        protected virtual void StartRuntime() {
            Execute.InitializeWithDispatcher();
            EventAggregator.DefaultPublicationThreadMarshaller = Execute.OnUIThread;
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
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected virtual void Configure() { }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected virtual void PrepareApplication()
        {
            Application.Startup += OnStartup;
#if SILVERLIGHT
            Application.UnhandledException += OnUnhandledException;
#else
            Application.DispatcherUnhandledException += OnUnhandledException;
#endif
            Application.Exit += OnExit;
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
#if SILVERLIGHT
            return new[] { Application.Current.GetType().Assembly };
#else
            return new[] { Assembly.GetEntryAssembly() };
#endif
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected virtual object GetInstance(Type service, string key)
        {
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
        protected virtual void BuildUp(object instance) {}

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected virtual void OnStartup(object sender, StartupEventArgs e) {}

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
        protected virtual void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {}
#else
        /// <summary>
        /// Override this to add custom behavior for unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) { }
#endif
            
#if SILVERLIGHT && !WP7
        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="viewModelType">The view model type.</param>
        protected static void DisplayRootViewFor(Application application, Type viewModelType) {
            var viewModel = IoC.GetInstance(viewModelType, null);
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if(activator != null)
                activator.Activate();

            Mouse.Initialize(view);
            application.RootVisual = view;
        }
#elif NET
        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        protected static void DisplayRootViewFor(Type viewModelType) {
            IWindowManager windowManager;

            try
            {
                windowManager = IoC.Get<IWindowManager>();
            }
            catch
            {
                windowManager = new WindowManager();
            }

            windowManager.ShowWindow(IoC.GetInstance(viewModelType, null));
        }
#endif
    }

#if !WP7
    /// <summary>
    /// A strongly-typed version of <see cref="Bootstrapper"/> that specifies the type of root model to create for the application.
    /// </summary>
    /// <typeparam name="TRootModel">The type of root model for the application.</typeparam>
    public class Bootstrapper<TRootModel> : Bootstrapper {
        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
#if SILVERLIGHT
            DisplayRootViewFor(Application, typeof(TRootModel));
#else
            DisplayRootViewFor(typeof(TRootModel));
#endif
        }
    }
#endif
}