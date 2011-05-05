namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Windows;

    public static class StorageInstructionExtensions {
        public static StorageInstructionBuilder<T> InPhoneState<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => { x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<PhoneStateStorageMechanism>(); });
        }

        public static StorageInstructionBuilder<T> InIsolatedStorage<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => { x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<IsolatedStorageMechanism>(); });
        }

        public static StorageInstructionBuilder<T> RestoreAfterActivation<T>(this StorageInstructionBuilder<T> builder)
            where T : IActivate {
            return builder.Configure(x => {
                var original = x.Restore;

                x.Restore = (instance, getKey) => {
                    if(instance.IsActive)
                        original(instance, getKey);
                    else {
                        EventHandler<ActivationEventArgs> onActivate = null;
                        onActivate = (s, e) => {
                            original(instance, getKey);
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
                var original = x.Restore;

                x.Restore = (instance, getKey) => {
                    EventHandler<ViewAttachedEventArgs> onViewAttached = null;
                    onViewAttached = (s, e) => {
                        var fe = (FrameworkElement)e.View;
                        View.ExecuteOnLoad(fe, (s2, e2) => { original(instance, getKey); });
                        instance.ViewAttached -= onViewAttached;
                    };
                    instance.ViewAttached += onViewAttached;
                };
            });
        }

        public static StorageInstructionBuilder<T> ActiveItem<T>(this StorageHandler<T> handler)
            where T : IParent, IHaveActiveItem, IActivate {
            return handler.AddInstruction().Configure(x => {
                x.Key = "ActiveItemIndex";
                x.Save = (instance, getKey) => {
                    var children = instance.GetChildren().OfType<object>().ToList();
                    x.StorageMechanism.Store(getKey(), children.IndexOf(instance.ActiveItem));
                };
                x.Restore = (instance, getKey) => {
                    object value;
                    var key = getKey();
                    if(x.StorageMechanism.TryGet(key, out value)) {
                        var children = instance.GetChildren().OfType<object>().ToList();
                        var index = Convert.ToInt32(value);
                        instance.ActiveItem = children[index];
                        x.StorageMechanism.Delete(key);
                    }
                };
            });
        }
    }
}