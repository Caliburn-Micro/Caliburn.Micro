namespace Caliburn.Micro {
    public static class StorageInstructionExtensions {
        public static StorageInstructionBuilder<T> StoredInPhoneState<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => {
                x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<PhoneStateStorageMechanism>();
            });
        }

        public static StorageInstructionBuilder<T> StoredInIsolatedStorage<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => {
                x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<IsolatedStorageMechanism>();
            });
        }
    }
}