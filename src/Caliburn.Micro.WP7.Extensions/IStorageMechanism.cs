namespace Caliburn.Micro {
    using System;

    public interface IStorageMechanism {
        bool Supports(StorageMode mode);

        void BeginStore();
        void Store(string key, object data);
        void EndStore();

        bool TryGet(string key, out object value);
        void Delete(string key);

        void RegisterWithContainer(Type service, string key, Type implementation);
    }
}