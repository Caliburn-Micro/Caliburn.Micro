namespace Caliburn.Micro {
    public interface IStorageMechanism {
        bool Supports(StorageMode mode);

        void BeginStore();
        void Store(string key, object data);
        void EndStore();

        object Get(string key);
        void Delete(string key);
    }
}