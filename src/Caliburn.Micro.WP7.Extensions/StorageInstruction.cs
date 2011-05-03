namespace Caliburn.Micro {
    using System;

    public class StorageInstruction<T> : PropertyChangedBase {
        IStorageHandler owner;
        IStorageMechanism storageMechanism;
        string key;
        Func<T, object> get;
        Action<T, IStorageMechanism, Func<string>> set;

        public IStorageHandler Owner {
            get { return owner; }
            set {
                owner = value;
                NotifyOfPropertyChange("Owner");
            }
        }

        public IStorageMechanism StorageMechanism {
            get { return storageMechanism; }
            set {
                storageMechanism = value;
                NotifyOfPropertyChange("StorageMechanism");
            }
        }

        public string Key {
            get { return key; }
            set {
                key = value;
                NotifyOfPropertyChange("Key");
            }
        }

        public Func<T, object> Get {
            get { return get; }
            set {
                get = value;
                NotifyOfPropertyChange("Get");
            }
        }

        public Action<T, IStorageMechanism, Func<string>> Set {
            get { return set; }
            set {
                set = value;
                NotifyOfPropertyChange("Set");
            }
        }
    }
}