namespace Caliburn.Micro {
    using System;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Windows.Controls;

    /// <summary>
    /// A custom IoC container which integrates with the phone and properly registers all Caliburn.Micro services.
    /// </summary>
    public class PhoneContainer : SimpleContainer, IPhoneContainer {
        protected readonly Frame RootFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneContainer"/> class.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        public PhoneContainer(Frame rootFrame) {
            RootFrame = rootFrame;
        }

        /// <summary>
        /// Registers the service as a singleton stored in the phone state.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="phoneStateKey">The phone state key.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterWithPhoneService(Type service, string phoneStateKey, Type implementation) {
            var pservice = (IPhoneService)GetInstance(typeof(IPhoneService), null);

            if(!pservice.State.ContainsKey(phoneStateKey ?? service.FullName)) {
                pservice.State[phoneStateKey ?? service.FullName] = BuildInstance(implementation);
            }

            RegisterHandler(service, phoneStateKey, container => {
                var phoneService = (IPhoneService)container.GetInstance(typeof(IPhoneService), null);

                if(phoneService.State.ContainsKey(phoneStateKey ?? service.FullName)) {
                    return phoneService.State[phoneStateKey ?? service.FullName];
                }

                return BuildInstance(implementation);
            });
        }

        /// <summary>
        /// Registers the service as a singleton stored in the app settings.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="appSettingsKey">The app settings key.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterWithAppSettings(Type service, string appSettingsKey, Type implementation) {
            if(!IsolatedStorageSettings.ApplicationSettings.Contains(appSettingsKey ?? service.FullName)) {
                IsolatedStorageSettings.ApplicationSettings[appSettingsKey ?? service.FullName] = BuildInstance(implementation);
            }

            RegisterHandler(service, appSettingsKey, container => {
                if(IsolatedStorageSettings.ApplicationSettings.Contains(appSettingsKey ?? service.FullName)) {
                    return IsolatedStorageSettings.ApplicationSettings[appSettingsKey ?? service.FullName];
                }

                return BuildInstance(implementation);
            });
        }

        /// <summary>
        /// Registers the Caliburn.Micro services with the container.
        /// </summary>
        /// <param name="treatViewAsLoaded">if set to <c>true</c> [treat view as loaded].</param>
        public virtual void RegisterPhoneServices(bool treatViewAsLoaded = false) {
            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(PhoneContainer), null, this);
            RegisterInstance(typeof(IPhoneContainer), null, this);

            if (!HasHandler(typeof(INavigationService), null)) {
                RegisterInstance(typeof(INavigationService), null, new FrameAdapter(RootFrame, treatViewAsLoaded));
            }

            if (!HasHandler(typeof(IPhoneService), null)) {
                RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(RootFrame));
            }

            if (!HasHandler(typeof(IEventAggregator), null)) {
                RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
            }

            if (!HasHandler(typeof(IWindowManager), null)) {
                RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            }

            if (!HasHandler(typeof(IVibrateController), null)) {
                RegisterSingleton(typeof(IVibrateController), null, typeof(SystemVibrateController));
            }

            if (!HasHandler(typeof(ISoundEffectPlayer), null)) {
                RegisterSingleton(typeof(ISoundEffectPlayer), null, typeof(XnaSoundEffectPlayer));
            }

            EnableStorageCoordinator();
            EnableTaskController();
        }

        protected void EnableStorageCoordinator() {
            var toSearch = AssemblySource.Instance.ToArray()
                .Union(new[] { typeof(IStorageMechanism).Assembly });

            foreach(var assembly in toSearch) {
                this.AllTypesOf<IStorageMechanism>(assembly);
                this.AllTypesOf<IStorageHandler>(assembly);
            }

            RegisterSingleton(typeof(StorageCoordinator), null, typeof(StorageCoordinator));
            var coordinator = (StorageCoordinator)GetInstance(typeof(StorageCoordinator), null);
            coordinator.Start();
        }

        protected void EnableTaskController() {
            RegisterSingleton(typeof(TaskController), null, typeof(TaskController));
            var taskController = (TaskController)GetInstance(typeof(TaskController), null);
            taskController.Start();
        }
    }
}