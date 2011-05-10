namespace Caliburn.Micro {
    using System;

    public class PhoneContainer : SimpleContainer, IPhoneContainer {
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

        public void RegisterWithIsolatedStorage(Type service, string isoStorageKey, Type implementation) {
            throw new NotImplementedException();
        }

        public void RegisterPhoneServices(bool treatViewAsLoaded = false) {
            var events = new EventAggregator();
            var phoneService = new PhoneApplicationServiceAdapter(bootstrapper.PhoneService);
            var navigationService = new FrameAdapter(bootstrapper.RootFrame, treatViewAsLoaded);

            RegisterInstance(typeof(PhoneBootstrapper), null, bootstrapper);
            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(PhoneContainer), null, this);
            RegisterInstance(typeof(IPhoneContainer), null, this);
            RegisterInstance(typeof(INavigationService), null, navigationService);
            RegisterInstance(typeof(IPhoneService), null, phoneService);
            RegisterInstance(typeof(IEventAggregator), null, events);
            RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));

            RegisterSingleton(typeof(StorageCoordinator), null, typeof(StorageCoordinator));
            var coordinator = (StorageCoordinator)GetInstance(typeof(StorageCoordinator), null);
            coordinator.Start();

            var taskController = new TaskController(bootstrapper, events);
            RegisterInstance(typeof(TaskController), null, taskController);
            taskController.Start();
        }
    }
}