namespace Caliburn.Micro {
    using Microsoft.Phone.Shell;

    public class PhoneStateStorageMechanism : IStorageMechanism {
        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Tombstone;
        }

        public void Begin() { }

        public void Put(string key, object data) {
            PhoneApplicationService.Current.State[key] = data;
        }

        public void End() { }

        public object Get(string key) {
            return PhoneApplicationService.Current.State[key];
        }

        public void Delete(string key) {
            PhoneApplicationService.Current.State.Remove(key);
        }
    }
}