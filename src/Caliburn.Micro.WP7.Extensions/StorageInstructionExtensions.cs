namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Extension methods for configuring storage instructions.
    /// </summary>
    public static class StorageInstructionExtensions {
        /// <summary>
        /// Stores the data in the transient phone State.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static StorageInstructionBuilder<T> InPhoneState<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => { x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<PhoneStateStorageMechanism>(); });
        }

        /// <summary>
        /// Stores the data in the permanent ApplicationSettings.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static StorageInstructionBuilder<T> InAppSettings<T>(this StorageInstructionBuilder<T> builder) {
            return builder.Configure(x => { x.StorageMechanism = x.Owner.Coordinator.GetStorageMechanism<AppSettingsStorageMechanism>(); });
        }

        /// <summary>
        /// Restores the data when IActivate.Activated is raised.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static StorageInstructionBuilder<T> RestoreAfterActivation<T>(this StorageInstructionBuilder<T> builder)
            where T : IActivate {
            return builder.Configure(x => {
                var original = x.Restore;

                x.Restore = (instance, getKey, mode) => {
                    if(instance.IsActive)
                        original(instance, getKey, mode);
                    else {
                        EventHandler<ActivationEventArgs> onActivate = null;
                        onActivate = (s, e) => {
                            original(instance, getKey, mode);
                            instance.Activated -= onActivate;
                        };
                        instance.Activated += onActivate;
                    }
                };
            });
        }

        /// <summary>
        /// Restores the data after view's Loaded event is raised.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static StorageInstructionBuilder<T> RestoreAfterViewLoad<T>(this StorageInstructionBuilder<T> builder)
            where T : IViewAware {
            return builder.Configure(x => {
                var original = x.Restore;

                x.Restore = (instance, getKey, mode) => {
                    EventHandler<ViewAttachedEventArgs> onViewAttached = null;
                    onViewAttached = (s, e) => {
                        var fe = (FrameworkElement)e.View;
                        View.ExecuteOnLoad(fe, (s2, e2) => { original(instance, getKey, mode); });
                        instance.ViewAttached -= onViewAttached;
                    };
                    instance.ViewAttached += onViewAttached;
                };
            });
        }

        /// <summary>
        /// Restores the data after view's LayoutUpdated event is raised.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static StorageInstructionBuilder<T> RestoreAfterViewReady<T>(this StorageInstructionBuilder<T> builder)
            where T : IViewAware {
            return builder.Configure(x => {
                var original = x.Restore;

                x.Restore = (instance, getKey, mode) => {
                    EventHandler<ViewAttachedEventArgs> onViewAttached = null;
                    onViewAttached = (s, e) => {
                        var fe = (FrameworkElement)e.View;
                        instance.ViewAttached -= onViewAttached;
                        EventHandler handler = null;
                        handler = (s2, e2) => {
                            original(instance, getKey, mode);
                            fe.LayoutUpdated -= handler;
                        };
                        fe.LayoutUpdated += handler;
                    };
                    instance.ViewAttached += onViewAttached;
                };
            });
        }

        /// <summary>
        /// Stores the index of the Conductor's ActiveItem.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns>The builder.</returns>
        public static StorageInstructionBuilder<T> ActiveItemIndex<T>(this StorageHandler<T> handler)
            where T : IParent, IHaveActiveItem, IActivate {
            return handler.AddInstruction().Configure(x => {
                x.Key = "ActiveItemIndex";
                x.Save = (instance, getKey, mode) => {
                    var children = instance.GetChildren().OfType<object>().ToList();
                    x.StorageMechanism.Store(getKey(), children.IndexOf(instance.ActiveItem));
                };
                x.Restore = (instance, getKey, mode) => {
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