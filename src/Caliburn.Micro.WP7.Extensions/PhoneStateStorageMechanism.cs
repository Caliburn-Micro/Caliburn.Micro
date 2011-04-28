namespace Caliburn.Micro {
    using Microsoft.Phone.Shell;

    public class PhoneStateStorageMechanism : IStorageMechanism {
        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Tombstone;
        }

        public void BeginStore() {}

        public void Store(string key, object data) {
            PhoneApplicationService.Current.State[key] = data;
        }

        public void EndStore() {}

        public object Get(string key) {
            return PhoneApplicationService.Current.State[key];
        }

        public void Delete(string key) {
            PhoneApplicationService.Current.State.Remove(key);
        }
    }
}