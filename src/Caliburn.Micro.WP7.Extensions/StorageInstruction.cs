namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// An instruction for saving/loading data.
    /// </summary>
    /// <typeparam name="T">The model type.</typeparam>
    public class StorageInstruction<T> : PropertyChangedBase {
        IStorageHandler owner;
        IStorageMechanism storageMechanism;
        string key;
        Action<T, Func<string>, StorageMode> save;
        Action<T, Func<string>, StorageMode> restore;

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public IStorageHandler Owner {
            get { return owner; }
            set {
                owner = value;
                NotifyOfPropertyChange("Owner");
            }
        }

        /// <summary>
        /// Gets or sets the storage mechanism.
        /// </summary>
        /// <value>
        /// The storage mechanism.
        /// </value>
        public IStorageMechanism StorageMechanism {
            get { return storageMechanism; }
            set {
                storageMechanism = value;
                NotifyOfPropertyChange("StorageMechanism");
            }
        }

        /// <summary>
        /// Gets or sets the persistence key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key {
            get { return key; }
            set {
                key = value;
                NotifyOfPropertyChange("Key");
            }
        }

        /// <summary>
        /// Gets or sets the save action.
        /// </summary>
        /// <value>
        /// The save action.
        /// </value>
        public Action<T, Func<string>, StorageMode> Save {
            get { return save; }
            set {
                save = value;
                NotifyOfPropertyChange("Save");
            }
        }

        /// <summary>
        /// Gets or sets the restore action.
        /// </summary>
        /// <value>
        /// The restore action.
        /// </value>
        public Action<T, Func<string>, StorageMode> Restore {
            get { return restore; }
            set {
                restore = value;
                NotifyOfPropertyChange("Restore");
            }
        }
    }
}