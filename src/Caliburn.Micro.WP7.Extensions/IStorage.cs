namespace Caliburn.Micro {
    public interface IStorage {
        bool Supports(StorageMode mode);
        void Put(string key, object value);
        object Get(string key);
    }
}