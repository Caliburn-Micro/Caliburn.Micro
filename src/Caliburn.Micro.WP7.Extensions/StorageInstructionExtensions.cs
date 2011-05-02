namespace Caliburn.Micro {
    using System;
    using System.Windows;

    public static class StorageInstructionExtensions {
        public static StorageInstructionBuilder<T> StoredInPhoneState<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => { x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<PhoneStateStorageMechanism>(); });
        }

        public static StorageInstructionBuilder<T> StoredInIsolatedStorage<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => { x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<IsolatedStorageMechanism>(); });
        }

        public static StorageInstructionBuilder<T> RestoreAfterActivation<T>(this StorageInstructionBuilder<T> builder)
            where T : IActivate {
            return builder.Configure(x => {
                var original = x.Set;

                x.Set = (instance, value) => {
                    if(instance.IsActive)
                        original(instance, value);
                    else {
                        EventHandler<ActivationEventArgs> onActivate = null;
                        onActivate = (s, e) => {
                            original(instance, value);
                            instance.Activated -= onActivate;
                        };
                        instance.Activated += onActivate;
                    }
                };
            });
        }

        public static StorageInstructionBuilder<T> RestoreAfterViewLoad<T>(this StorageInstructionBuilder<T> builder)
            where T : IViewAware {
            return builder.Configure(x => {
                var original = x.Set;

                x.Set = (instance, value) => {
                    EventHandler<ViewAttachedEventArgs> onViewAttached = null;
                    onViewAttached = (s, e) => {
                        var fe = (FrameworkElement)e.View;
                        View.ExecuteOnLoad(fe, (s2, e2) => { original(instance, value); });
                        instance.ViewAttached -= onViewAttached;
                    };
                    instance.ViewAttached += onViewAttached;
                };
            });
        }
    }
}