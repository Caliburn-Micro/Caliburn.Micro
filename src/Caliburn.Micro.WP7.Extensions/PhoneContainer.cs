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
            RegisterInstance(typeof(SimpleContainer), null, this);
            RegisterInstance(typeof(PhoneContainer), null, this);
            RegisterInstance(typeof(INavigationService), null, new FrameAdapter(bootstrapper.RootFrame, treatViewAsLoaded));
            RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(bootstrapper.PhoneService));
            RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
            RegisterSingleton(typeof(IEventAggregator), null, typeof(EventAggregator));
        }
    }
}