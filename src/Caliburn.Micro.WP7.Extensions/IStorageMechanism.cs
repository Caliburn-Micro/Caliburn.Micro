namespace Caliburn.Micro {
    using System;

    public interface IStorageMechanism {
        bool Supports(StorageMode mode);

        void BeginStoring();
        void Store(string key, object data);
        void EndStoring();

        bool TryGet(string key, out object value);
        void Delete(string key);

        void RegisterSingleton(Type service, string key, Type implementation);
    }
}