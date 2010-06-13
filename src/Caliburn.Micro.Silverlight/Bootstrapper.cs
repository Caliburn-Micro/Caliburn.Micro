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
            if(!IsInDesignMode)
                Start();
        }

        internal virtual void Start() 
        {
            Execute.InitializeWithDispatcher();
            AssemblySource.Instance.AddRange(SelectAssemblies());
            Configure();

            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
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
        internal override void Start()
        {
            base.Start();
            Application.Current.Startup += OnStartup;
        }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
            var viewModel = IoC.GetInstance(typeof(TRootModel), null);
            var view = ViewLocator.LocateForModel(viewModel, null);
            ViewModelBinder.Bind(viewModel, view);

            var activator = viewModel as IActivate;
            if (activator != null)
                activator.Activate();

#if SILVERLIGHT
            Application.Current.RootVisual = view;
#else
            ((Window)view).Show();
#endif
        }
    }
}