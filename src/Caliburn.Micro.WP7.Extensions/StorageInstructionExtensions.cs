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
                var original = x.Set;

                x.Set = (instance, storage, getKey) => {
                    if(instance.IsActive)
                        original(instance, storage, getKey);
                    else {
                        EventHandler<ActivationEventArgs> onActivate = null;
                        onActivate = (s, e) => {
                            original(instance, storage, getKey);
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

                x.Set = (instance, storage, getKey) => {
                    EventHandler<ViewAttachedEventArgs> onViewAttached = null;
                    onViewAttached = (s, e) => {
                        var fe = (FrameworkElement)e.View;
                        View.ExecuteOnLoad(fe, (s2, e2) => { original(instance, storage, getKey); });
                        instance.ViewAttached -= onViewAttached;
                    };
                    instance.ViewAttached += onViewAttached;
                };
            });
        }

        public static StorageInstructionBuilder<T> ActiveItem<T>(this StorageHandler<T> handler) 
            where T : IParent, IHaveActiveItem, IActivate {
            var builder = handler.AddInstruction().Configure(x => {
                x.Key = "ActiveItemIndex";
                x.Get = conductor => {
                    var children = conductor.GetChildren().OfType<object>().ToList();
                    return children.IndexOf(conductor.ActiveItem);
                };
                x.Set = (instance, storage, getKey) => {
                    object value;
                    if(storage.TryGet(getKey(), out value)) {
                        var children = instance.GetChildren().OfType<object>().ToList();
                        var index = Convert.ToInt32(value);
                        instance.ActiveItem = children[index];
                    }
                };
            });

            return builder.RestoreAfterActivation();
        }
    }
}