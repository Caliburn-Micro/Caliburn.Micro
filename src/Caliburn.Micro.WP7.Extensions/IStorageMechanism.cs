namespace Caliburn.Micro {
    public interface IStorageMechanism {
        bool Supports(StorageMode mode);

        void Begin();
        void Put(string key, object data);
        void End();

        object Get(string key);
        void Delete(string key);
    }
}