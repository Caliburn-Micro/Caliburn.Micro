namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    public class Bootstrapper
    {
        public Bootstrapper()
        {
            AssemblySource.Known.AddRange(SelectAssemblies());
            ConfigureContainer();
            IoC.Initialize(GetInstance, GetAllInstances);
        }

        protected virtual void ConfigureContainer() { }

        protected virtual IEnumerable<Assembly> SelectAssemblies()
        {
#if SILVERLIGHT
            return new[] { Application.Current.GetType().Assembly };
#else
            return new[] { Assembly.GetEntryAssembly() };
#endif
        }

        protected virtual object GetInstance(Type service, string key)
        {
            return Activator.CreateInstance(service);
        }

        protected virtual IEnumerable<object> GetAllInstances(Type service)
        {
            return new[] { Activator.CreateInstance(service) };
        }
    }

    public class Bootstrapper<TRootModel> : Bootstrapper
    {
        public Bootstrapper()
        {
            Application.Current.Startup += OnStartup;
        }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
#if SILVERLIGHT
            Application.Current.RootVisual = View.GetWithViewModel<TRootModel>();
#else
            var window = (Window)View.GetWithViewModel<TRootModel>();
            window.Show();
#endif
        }
    }
}