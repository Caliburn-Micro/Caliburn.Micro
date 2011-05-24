namespace Caliburn.Micro {
    using System;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Windows.Controls;

    public class PhoneContainer : SimpleContainer, IPhoneContainer {
        readonly Frame rootFrame;

        public PhoneContainer(Frame rootFrame) {
            this.rootFrame = rootFrame;
        }

        public void RegisterWithPhoneService(Type service, string phoneStateKey, Type implementation) {
            RegisterHandler(service, null, () => {
                var phoneService = (IPhoneService)GetInstance(typeof(IPhoneService), null);

                if(phoneService.State.ContainsKey(phoneStateKey ?? service.FullName)) {
                    return phoneService.State[phoneStateKey ?? service.FullName];
                }

                return BuildInstance(implementation);
            });
        }

        public void RegisterWithAppSettings(Type service, string appSettingsKey, Type implementation) {
            RegisterHandler(service, null, () => {
                if(IsolatedStorageSettings.ApplicationSettings.Contains(appSettingsKey ?? service.FullName)) {
                    return IsolatedStorageSettings.ApplicationSettings[appSettingsKey ?? service.FullName];
                }

                return BuildInstance(implementation);
            });
        }

        public void RegisterPhoneServices(bool treatViewAsLoaded = false) {
            var toSearch = AssemblySource.Instance.ToArray().Union(new[] { typeof(IStorageMechanism).Assembly });

            foreach (var assembly in toSearch) {
                this.AllTypesOf<IStorageMechanism>(assembly);
                this.AllTypesOf<IStorageHandler>(assembly);
            }

            var phoneService = new PhoneApplicationServiceAdapter(rootFrame);
            var navigationService = new FrameAdapter(rootFrame, treatViewAsLoaded);

            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(PhoneContainer), null, this);
            RegisterInstance(typeof(IPhoneContainer), null, this);
            RegisterInstance(typeof(INavigationService), null, navigationService);
            RegisterInstance(typeof(IPhoneService), null, phoneService);
            RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));

            RegisterSingleton(typeof(StorageCoordinator), null, typeof(StorageCoordinator));
            var coordinator = (StorageCoordinator)GetInstance(typeof(StorageCoordinator), null);
            coordinator.Start();

            RegisterSingleton(typeof(TaskController), null, typeof(TaskController));
            var taskController = (TaskController)GetInstance(typeof(TaskController), null);
            taskController.Start();
        }
    }
}