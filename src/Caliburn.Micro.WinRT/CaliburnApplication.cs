using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CaliburnApplication : Application
    {
        private bool isInitialized;

        public Frame RootFrame
        {
            get;
            private set;
        }

        /// <summary>
        /// Called by the bootstrapper's constructor at design time to start the framework.
        /// </summary>
        protected virtual void StartDesignTime()
        {
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
            AssemblySource.Instance.AddRange(SelectAssemblies());

            PrepareApplication();
            Configure();

            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;
        }

        protected virtual void Initialise()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

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
                }
            }
            else
            {
                StartRuntime();
            }
        }

        /// <summary>
        /// Provides an opportunity to hook into the application object.
        /// </summary>
        protected virtual void PrepareApplication()
        {
            Resuming += OnResuming;
            Suspending += OnSuspending;
            UnhandledException += OnUnhandledException;

            RootFrame = CreateApplicationFrame();
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

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            EnsurePage(args);
        }

        protected virtual void OnResuming(object sender, object e)
        {

        }

        protected virtual void OnSuspending(object sender, SuspendingEventArgs e)
        {
        }

        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        protected virtual Frame CreateApplicationFrame()
        {
            return new Frame();
        }

        protected virtual void EnsurePage(IActivatedEventArgs args)
        {
            Initialise();

            switch (args.Kind)
            {
                default:

                    var defaultView = GetDefaultView();

                    RootFrame.Navigate(defaultView);

                    break;
            }

            // Seems stupid but observed weird behaviour when resetting the Content
            if (Window.Current.Content != RootFrame)
                Window.Current.Content = RootFrame;

            Window.Current.Activate();
        }

        protected abstract Type GetDefaultView();
    }
}
