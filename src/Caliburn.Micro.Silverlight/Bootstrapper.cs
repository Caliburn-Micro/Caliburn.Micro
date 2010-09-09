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
        private static bool? isInDesignMode;

        /// <summary>
        /// The application.
        /// </summary>
        public Application Application { get; private set; }

        /// <summary>
        /// Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                if (isInDesignMode == null)
                {
                    var app = Application.Current.ToString();

                    if (app == "System.Windows.Application" || app == "Microsoft.Expression.Blend.BlendApplication")
                        isInDesignMode = true;
                    else isInDesignMode = false;
                }

                return isInDesignMode.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// Creates an instance of the bootstrapper.
        /// </summary>
        public Bootstrapper()
        {
            if(IsInDesignMode)
                return;

            Execute.InitializeWithDispatcher();
            AssemblySource.Instance.AddRange(SelectAssemblies());

            Application = Application.Current;
            PrepareApplication();

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
        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
#if !WP7
            DisplayRootView();
#endif
        }

        /// <summary>
        /// Override this to add custom behavior on exit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnExit(object sender, EventArgs e) { }

#if !WP7
        /// <summary>
        /// Override to display your UI at startup.
        /// </summary>
        protected virtual void DisplayRootView() {}
#endif

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
    }

#if !WP7
    /// <summary>
    /// A strongly-typed version of <see cref="Bootstrapper"/> that specifies the type of root model to create for the application.
    /// </summary>
    /// <typeparam name="TRootModel">The type of root model for the application.</typeparam>
    public class Bootstrapper<TRootModel> : Bootstrapper
    {
        /// <summary>
        /// Override to display your UI at startup.
        /// </summary>
        protected override void DisplayRootView()
        {
            var viewModel = IoC.Get<TRootModel>();
#if SILVERLIGHT
            var view = ViewLocator.LocateForModel(viewModel, null, null);
            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if (activator != null)
                activator.Activate();

            Application.RootVisual = view;
#else
            IWindowManager windowManager;

            try
            {
                windowManager = IoC.Get<IWindowManager>();
            }
            catch
            {
                windowManager = new WindowManager();
            }

            windowManager.Show(viewModel);
#endif
        }
    }
#endif
}