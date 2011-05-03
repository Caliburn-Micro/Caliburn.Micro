namespace Caliburn.Micro {
    using System;

    public interface IStorageMechanism {
        bool Supports(StorageMode mode);

        void BeginStore();
        void Store(string key, object data);
        void EndStore();

        object Get(string key);
        void Delete(string key);

        void RegisterWithContainer(Type service, string key, Type implementation);
    }
}