namespace Caliburn.Micro {
    using System;

    public class IsolatedStorageMechanism : IStorageMechanism {
        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Permanent;
        }

        public void BeginStore() {
            throw new NotImplementedException();
        }

        public void Store(string key, object data) {
            throw new NotImplementedException();
        }

        public void EndStore() {
            throw new NotImplementedException();
        }

        public object Get(string key) {
            throw new NotImplementedException();
        }

        public void Delete(string key) {
            throw new NotImplementedException();
        }

        public void RegisterWithContainer(Type service, string key, Type implementation) {
            throw new NotImplementedException();
        }
    }
}