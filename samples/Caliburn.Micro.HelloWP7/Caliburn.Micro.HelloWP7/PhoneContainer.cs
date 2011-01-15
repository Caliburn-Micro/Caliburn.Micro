namespace Caliburn.Micro.HelloWP7
{
    using System;

    public class PhoneContainer : SimpleContainer
    {
        public PhoneContainer(PhoneBootstrapper bootstrapper)
        {
            Activator = new InstanceActivator(bootstrapper, type => GetInstance(type, null));
        }

        public InstanceActivator Activator { get; private set; }

        protected override object ActivateInstance(Type type, object[] args)
        {
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
    }
}