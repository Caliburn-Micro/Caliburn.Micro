namespace Caliburn.Micro {
    using System;

    public class PhoneStateStorageMechanism : IStorageMechanism {
        readonly IPhoneContainer container;
        readonly IPhoneService phoneService;

        public PhoneStateStorageMechanism(IPhoneContainer container, IPhoneService phoneService) {
            this.container = container;
            this.phoneService = phoneService;
        }

        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Temporary;
        }

        public void BeginStoring() {}

        public void Store(string key, object data) {
            phoneService.State[key] = data;
        }

        public void EndStoring() {}

        public bool TryGet(string key, out object value) {
            return phoneService.State.TryGetValue(key, out value);
        }

        public void Delete(string key) {
            phoneService.State.Remove(key);
        }

        public void RegisterSingleton(Type service, string key, Type implementation) {
            container.RegisterWithPhoneService(service, key, implementation);
        }
    }
}