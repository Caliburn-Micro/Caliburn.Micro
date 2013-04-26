using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// Encapsulates the app and its available services.
    /// </summary>
    public abstract class CaliburnApplication : Application
    {
        private bool isInitialized;

        /// <summary>
        /// The root frame of the application.
        /// </summary>
        protected Frame RootFrame
        {
            get;
            private set;
        }

        /// <summary>
        /// Called by the bootstrapper's constructor at design time to start the framework.
        /// </summary>
        protected virtual void StartDesignTime()
        {
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
        protected virtual void StartRuntime()
        {
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

            PrepareApplication();
            Configure();

            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;
        }

        /// <summary>
        /// Start the framework.
        /// </summary>
        protected virtual void Initialise()
        {
            if (isInitialized) {
                return;
            }

            isInitialized = true;

            if (Execute.InDesignMode) {
                try {
                    StartDesignTime();
                } catch {
                    //if something fails at design-time, there's really nothing we can do...
                    isInitialized = false;
                }
            }
            else {
                StartRuntime();
            }
        }

        /// <summary>
        /// Invoked when the application creates a window.
        /// </summary>
        /// <param name="args">Event data for the event.</param>
        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            base.OnWindowCreated(args);

            // Because dispatchers are tied to windows Execute will fail in 
            // scenarios when the app has multiple windows open (though contract 
            // activation, this keeps Excute up to date with the currently activated window

            args.Window.Activated += (s, e) => Execute.InitializeWithDispatcher();
        }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected virtual void PrepareApplication()
        {
            Resuming += OnResuming;
            Suspending += OnSuspending;
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
            return new[] { GetType().GetTypeInfo().Assembly };
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
        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
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
            Initialise();

            if (RootFrame == null)
            {
                RootFrame = CreateApplicationFrame();
                PrepareViewFirst(RootFrame);
            }

            RootFrame.Navigate(viewType, paramter);

            // Seems stupid but observed weird behaviour when resetting the Content
            if (Window.Current.Content != RootFrame)
                Window.Current.Content = RootFrame;

            Window.Current.Activate();
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <typeparam name="T">The view type to navigate to.</typeparam>
        /// <param name="parameter">The object parameter to pass to the target.</param>
        protected void DisplayRootView<T>(object parameter = null)
        {
            DisplayRootView(typeof(T), parameter);
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        protected void DisplayRootViewFor(Type viewModelType)
        {
            Initialise();

            var viewModel = IoC.GetInstance(viewModelType, null);
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if (activator != null)
                activator.Activate();

            Window.Current.Content = view;
            Window.Current.Activate();
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        protected void DisplayRootViewFor<T>()
        {
            DisplayRootViewFor(typeof(T));
        }
    }
}
