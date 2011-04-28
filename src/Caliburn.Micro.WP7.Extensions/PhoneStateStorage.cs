namespace Caliburn.Micro {
    public class PhoneStateStorage : IStorage {
        readonly IPhoneService phoneService;

        public PhoneStateStorage(IPhoneService phoneService) {
            this.phoneService = phoneService;
        }

        public bool Supports(StorageMode mode) {
            return mode == StorageMode.Tombstone;
        }

        public void Put(string key, object value) {
            phoneService.State[key] = value;
        }

        public object Get(string key) {
            return phoneService.State[key];
        }
    }
}