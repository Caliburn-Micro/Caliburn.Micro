using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Caliburn.Micro.HelloUWP.Messages;
using Caliburn.Micro.HelloUWP.ViewModels;

namespace Caliburn.Micro.HelloUWP
{
    public sealed partial class App
    {
        private WinRTContainer _container;
        private IEventAggregator _eventAggregator;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();

            _container
                .PerRequest<ShellViewModel>()
                .PerRequest<DeviceViewModel>();

            _eventAggregator = _container.GetInstance<IEventAggregator>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Note we're using DisplayRootViewFor (which is view model first)
            // this means we're not creating a root frame and just directly
            // inserting ShellView as the Window.Content

            DisplayRootViewFor<ShellViewModel>();

            // It's kinda of weird having to use the event aggregator to pass 
            // a value to ShellViewModel, could be an argument for allowing
            // parameters or launch arguments

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                _eventAggregator.PublishOnUIThread(new ResumeStateMessage());
            }
        }

        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            _eventAggregator.PublishOnUIThread(new SuspendStateMessage(e.SuspendingOperation));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
