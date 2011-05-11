namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    public class PhoneStateStorageMechanism : IStorageMechanism {
        readonly IPhoneContainer container;
        readonly IPhoneService phoneService;
        List<string> keys;

        public PhoneStateStorageMechanism(IPhoneContainer container, IPhoneService phoneService) {
            this.container = container;
            this.phoneService = phoneService;
        }

        public bool Supports(StorageMode mode) {
            return (mode & StorageMode.Temporary) == StorageMode.Temporary;
        }

        public void BeginStoring() {
            keys = new List<string>();
        }

        public void Store(string key, object data) {
            if(!phoneService.State.ContainsKey(key))
                keys.Add(key);

            phoneService.State[key] = data;
        }

        public void EndStoring() { }

        public bool TryGet(string key, out object value) {
            return phoneService.State.TryGetValue(key, out value);
        }

        public void Delete(string key) {
            phoneService.State.Remove(key);
        }

        public void ClearLastSession() {
            if(keys != null) {
                keys.Apply(x => phoneService.State.Remove(x));
                keys = null;
            }
        }

        public void RegisterSingleton(Type service, string key, Type implementation) {
            container.RegisterWithPhoneService(service, key, implementation);
        }
    }
}