using System;
using System.Collections.Generic;
using System.Reflection;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Caliburn.Micro.Maui
{
    /// <summary>
    /// Encapsulates the app and its available services.
    /// </summary>
    public abstract class CaliburnApplication : Microsoft.Maui.MauiApplication
    {
        private bool isInitialized;

        /// <summary>
        /// Creates an instance of <see cref="CaliburnApplication"/>.
        /// </summary>
        /// <param name="javaReference">A <see cref="T:System.IntPtr"/> which contains the <c>java.lang.Class</c> JNI value corresponding to this type.</param>
        /// <param name="transfer">How to handle ownership</param>
        public CaliburnApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            
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
        /// </summary>B
        protected virtual void StartRuntime()
        {
            AssemblySourceCache.Install();
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
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            PlatformProvider.Current = new AndroidPlatformProvider(this);

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
    }
}
