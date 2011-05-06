namespace Caliburn.Micro {
    using System;

    public class IsolatedStorageMechanism : IStorageMechanism {
        readonly IPhoneContainer container;

        public IsolatedStorageMechanism(IPhoneContainer container) {
            this.container = container;
        }

        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Permanent;
        }

        public void BeginStoring() { }

        public void Store(string key, object data) {
            throw new NotImplementedException();
        }

        public void EndStoring() { }

        public bool TryGet(string key, out object value) {
            throw new NotImplementedException();
        }

        public void Delete(string key) {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type service, string key, Type implementation) {
            container.RegisterWithIsolatedStorage(service, key, implementation);
        }
    }
}