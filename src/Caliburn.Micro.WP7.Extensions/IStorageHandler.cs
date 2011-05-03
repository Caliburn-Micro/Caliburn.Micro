namespace Caliburn.Micro {
    public interface IStorageHandler {
        StorageCoordinator Coordinator { get; set; }

        void Configure();

        bool Handles(object instance);
        void Save(object instance, StorageMode mode);
        void Restore(object instance);
    }
}