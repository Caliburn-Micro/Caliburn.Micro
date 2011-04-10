namespace Caliburn.Micro.HelloWP7
{
    using System;
    using System.Linq;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Tasks;

    public class PhoneContainer : SimpleContainer
    {
        readonly PhoneBootstrapper bootstrapper;

        public PhoneContainer(PhoneBootstrapper bootstrapper) {
            this.bootstrapper = bootstrapper;
            Activator = new InstanceActivator(bootstrapper, type => GetInstance(type, null));
        }

        public InstanceActivator Activator { get; private set; }

        protected override object ActivateInstance(Type type, object[] args)
        {
            return Activator.ActivateInstance(base.ActivateInstance(type, args));
        }

        public void RegisterWithPhoneService<TService, TImplementation>() where TImplementation : TService
        {
            RegisterHandler(typeof(TService), null, () =>
            {
                var phoneService = (IPhoneService)GetInstance(typeof(IPhoneService), null);

                if (phoneService.State.ContainsKey(typeof(TService).FullName))
                    return phoneService.State[typeof(TService).FullName];

                var instance = BuildInstance(typeof(TImplementation));
                phoneService.State[typeof(TService).FullName] = instance;

                return instance;
            });
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TImplementation : TService {
                RegisterSingleton(typeof(TService), null, typeof(TImplementation));
        }

        public void RegisterPerRequest<TService, TImplementation>()
            where TImplementation : TService {
            RegisterPerRequest(typeof(TService), null, typeof(TImplementation));
        }

        public void RegisterPerRequestPageVM<TViewModel>() {
            RegisterPerRequestPageVM(typeof(TViewModel));
        }

        public void RegisterPerRequestPageVM(Type viewModelType) {
            RegisterPerRequest(viewModelType, viewModelType.Name, viewModelType);
        }

        public void RegisterInstance<TService>(TService instance) {
            RegisterInstance(typeof(TService), null, instance);
        }

        public void RegisterAllViewModelsForPages()
        {
            var pages = from assembly in AssemblySource.Instance
                        from type in assembly.GetTypes()
                        where typeof(PhoneApplicationPage).IsAssignableFrom(type)
                        select type;

            foreach (var page in pages)
            {
                var key = ViewModelLocator.LocateKeyForViewType(page);

                var viewModelType = (from assembly in AssemblySource.Instance
                                     from type in assembly.GetTypes()
                                     where type.Name == key
                                     select type).FirstOrDefault();

                if (viewModelType != null)
                    RegisterPerRequestPageVM(viewModelType);
            }
        }

        public void RegisterPhoneServices(bool treatViewAsLoaded = false) {
            RegisterInstance<INavigationService>(new FrameAdapter(bootstrapper.RootFrame, treatViewAsLoaded));
            RegisterInstance<IPhoneService>(new PhoneApplicationServiceAdapter(bootstrapper.PhoneService));
            RegisterSingleton(typeof(IWindowManager), null, typeof(WindowManager));
        }

        public void InstallChooser<TChooser,TResult>()
            where TChooser : ChooserBase<TResult>, new()
            where TResult : TaskEventArgs
        {
            Activator.InstallChooser<TChooser, TResult>();
        }

        public void InstallLauncher<TLauncher>() 
            where TLauncher : new() {
            Activator.InstallLauncher<TLauncher>();
        }
    }
}