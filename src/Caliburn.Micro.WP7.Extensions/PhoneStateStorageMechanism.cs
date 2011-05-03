namespace Caliburn.Micro {
    using System;

    public class PhoneStateStorageMechanism : IStorageMechanism {
        readonly PhoneContainer container;
        readonly IPhoneService phoneService;

        public PhoneStateStorageMechanism(PhoneContainer container, IPhoneService phoneService) {
            this.container = container;
            this.phoneService = phoneService;
        }

        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Temporary;
        }

        public void BeginStore() {}

        public void Store(string key, object data) {
            phoneService.State[key] = data;
        }

        public void EndStore() {}

        public bool TryGet(string key, out object value) {
            return phoneService.State.TryGetValue(key, out value);
        }

        public void Delete(string key) {
            phoneService.State.Remove(key);
        }

        public void RegisterWithContainer(Type service, string key, Type implementation) {
            container.RegisterWithPhoneService(service, key, implementation);
        }
    }
}