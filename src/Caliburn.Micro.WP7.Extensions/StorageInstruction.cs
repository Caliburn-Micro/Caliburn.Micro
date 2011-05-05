namespace Caliburn.Micro {
    using System;

    public class StorageInstruction<T> : PropertyChangedBase {
        IStorageHandler owner;
        IStorageMechanism storageMechanism;
        string key;
        Action<T, Func<string>, StorageMode> save;
        Action<T, Func<string>> restore;

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

        public Action<T, Func<string>, StorageMode> Save {
            get { return save; }
            set {
                save = value;
                NotifyOfPropertyChange("Save");
            }
        }

        public Action<T, Func<string>> Restore {
            get { return restore; }
            set {
                restore = value;
                NotifyOfPropertyChange("Restore");
            }
        }
    }
}