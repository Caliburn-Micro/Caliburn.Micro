namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class Bootstrapper
    {
#if !SILVERLIGHT
        public static readonly bool IsInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
#else
        public static readonly bool IsInDesignMode = DesignerProperties.GetIsInDesignMode(new UserControl());
#endif

        public Bootstrapper()
        {
            Execute.InitializeWithDispatcher();
            AssemblySource.Known.AddRange(SelectAssemblies());
            Configure();
            IoC.Initialize(GetInstance, GetAllInstances);
        }

        protected virtual void Configure() { }

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