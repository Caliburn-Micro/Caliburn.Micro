using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro.HelloPortable.Portable.ViewModels;
using Caliburn.Micro.HelloPortable.Views;

namespace Caliburn.Micro.HelloPortable
{
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {

            return new[]
                {
                    GetType().GetTypeInfo().Assembly,
                    typeof (MainViewModel).GetTypeInfo().Assembly
                };
        }

        protected override void Configure()
        {
            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);

            ViewModelLocator.AddNamespaceMapping("Caliburn.Micro.HelloPortable.Views", "Caliburn.Micro.HelloPortable.Portable.ViewModels");
           
            
            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container.PerRequest<MainViewModel>();

            PrepareViewFirst();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Initialize();

            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            if (RootFrame.Content == null)
                DisplayRootView<MainView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}