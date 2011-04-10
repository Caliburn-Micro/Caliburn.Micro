namespace Caliburn.Micro {
    using System;
    using Microsoft.Phone.Tasks;

    public class PhoneContainer : SimpleContainer {
        readonly PhoneBootstrapper bootstrapper;

        public PhoneContainer(PhoneBootstrapper bootstrapper) {
            this.bootstrapper = bootstrapper;
            Activator = new InstanceActivator(bootstrapper, type => GetInstance(type, null));
        }

        public InstanceActivator Activator { get; private set; }

        protected override object ActivateInstance(Type type, object[] args) {
            return Activator.ActivateInstance(base.ActivateInstance(type, args));
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

        public void InstallChooser<TChooser, TResult>()
            where TChooser : ChooserBase<TResult>, new()
            where TResult : TaskEventArgs {
            Activator.InstallChooser<TChooser, TResult>();
        }

        public void InstallLauncher<TLauncher>()
            where TLauncher : new() {
            Activator.InstallLauncher<TLauncher>();
        }
    }
}