namespace Caliburn.Micro {
    public class PhoneContainer : SimpleContainer {
        readonly PhoneBootstrapper bootstrapper;

        public PhoneContainer(PhoneBootstrapper bootstrapper) {
            this.bootstrapper = bootstrapper;
        }

        public void RegisterWithPhoneService<TService, TImplementation>() where TImplementation : TService {
            RegisterHandler(typeof(TService), null, () => {
                var phoneService = (IPhoneService)GetInstance(typeof(IPhoneService), null);

                if(phoneService.State.ContainsKey(typeof(TService).FullName))
                    return phoneService.State[typeof(TService).FullName];

                var instance = BuildInstance(typeof(TImplementation));
                phoneService.State[typeof(TService).FullName] = instance;

                return instance;
            });
        }

        public void RegisterPhoneServices(bool treatViewAsLoaded = false) {
            RegisterInstance(typeof(INavigationService), null, new FrameAdapter(bootstrapper.RootFrame, treatViewAsLoaded));
            RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(bootstrapper.PhoneService));
            RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
        }
    }
}