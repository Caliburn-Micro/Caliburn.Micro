namespace Caliburn.Micro {
    public interface IStorageHandler {
        bool Handles(object instance);
        void Save(object instance, StorageMode mode);
        void Restore(object instance);
    }
}