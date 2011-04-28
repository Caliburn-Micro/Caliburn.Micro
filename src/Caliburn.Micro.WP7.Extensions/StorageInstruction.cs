namespace Caliburn.Micro {
    using System;

    public class StorageInstruction<T> {
        public IStorageHandler Owner;
        public IStorageMechanism StorageMechanism;
        public string Key;
        public Func<T, object> Get;
        public Action<T, object> Set;
    }
}