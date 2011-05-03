namespace Caliburn.Micro {
    using System;

    public class PhoneContainer : SimpleContainer {
        readonly PhoneBootstrapper bootstrapper;

        public PhoneContainer(PhoneBootstrapper bootstrapper) {
            this.bootstrapper = bootstrapper;
        }

        public void RegisterWithPhoneService(Type service, string phoneStateKey, Type implementation) {
            RegisterHandler(service, null, () => {
                var phoneService = (IPhoneService)GetInstance(typeof(IPhoneService), null);

                if(phoneService.State.ContainsKey(phoneStateKey ?? service.FullName))
                    return phoneService.State[phoneStateKey ?? service.FullName];

                return BuildInstance(implementation);
            });
        }

        public void RegisterPhoneServices(bool treatViewAsLoaded = false) {
            var events = new EventAggregator();

            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(PhoneContainer), null, this);
            RegisterInstance(typeof(INavigationService), null, new FrameAdapter(bootstrapper.RootFrame, treatViewAsLoaded));
            RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(bootstrapper.PhoneService));
            RegisterInstance(typeof(IEventAggregator), null, events);
            RegisterInstance(typeof(TaskController), null, new TaskController(bootstrapper, events));

            RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            RegisterSingleton(typeof(StorageCoordinator), null, typeof(StorageCoordinator));

            var storageCoordinator = (StorageCoordinator)GetInstance(typeof(StorageCoordinator), null);
            Activated += storageCoordinator.Restore;
        }
    }
}