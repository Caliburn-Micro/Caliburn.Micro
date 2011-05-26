namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Handles the storage of a pariticular class.
    /// </summary>
    /// <typeparam name="T">The type that this class handles.</typeparam>
    public abstract class StorageHandler<T> : IStorageHandler {
        readonly List<StorageInstruction<T>> instructions = new List<StorageInstruction<T>>();
        Func<T, object> getId = instance => null;

        /// <summary>
        /// Provides a mechanism for obtaining an instance's unique id.
        /// </summary>
        /// <param name="getter">The getter.</param>
        public void Id(Func<T, object> getter) {
            getId = getter;
        }

        /// <summary>
        /// Gets or sets the coordinator.
        /// </summary>
        /// <value>
        /// The coordinator.
        /// </value>
        public StorageCoordinator Coordinator { get; set; }

        /// <summary>
        /// Overrided by inheritors to configure the handler for use.
        /// </summary>
        public abstract void Configure();

        /// <summary>
        /// Instructs the handler to store the entire object graph, rather than individual properties.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="storageKey">The optional storage key.</param>
        /// <returns>The builder.</returns>
        public StorageInstructionBuilder<T> EntireGraph<TService>(string storageKey = "ObjectGraph") {
            return AddInstruction().Configure(x => {
                x.Key = storageKey;
                x.Save = (instance, getKey, mode) => x.StorageMechanism.Store(getKey(), instance);
                x.Restore = (instance, getKey, mode) => { };
                x.PropertyChanged += (s, e) => {
                    if(e.PropertyName == "StorageMechanism" && x.StorageMechanism != null) {
                        x.StorageMechanism.RegisterSingleton(typeof(TService), GetKey(default(T), x.Key), typeof(T));
                    }
                };
            });
        }

        /// <summary>
        /// Instructs the handler to store a property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The builder.</returns>
        public StorageInstructionBuilder<T> Property(Expression<Func<T, object>> property) {
            var info = (PropertyInfo)property.GetMemberInfo();

            return AddInstruction().Configure(x => {
                x.Key = info.Name;
                x.Save = (instance, getKey, mode) => x.StorageMechanism.Store(getKey(), info.GetValue(instance, null));
                x.Restore = (instance, getKey, mode) => {
                    object value;
                    var key = getKey();
                    if(x.StorageMechanism.TryGet(key, out value)) {
                        info.SetValue(instance, value, null);
                        x.StorageMechanism.Delete(key);
                    }
                };
            });
        }

        /// <summary>
        /// Instructs the handler to store a child object's properties.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The builder.</returns>
        /// <remarks>This assumes that the parent instance provides the child instance, but that the child instance's properties are handled by a unique handler.</remarks>
        public StorageInstructionBuilder<T> Child(Expression<Func<T, object>> property) {
            var info = (PropertyInfo)property.GetMemberInfo();

            return AddInstruction().Configure(x => {
                x.Key = info.Name;
                x.Save = (instance, getKey, mode) => {
                    var child = info.GetValue(instance, null);
                    if(child == null)
                        return;

                    var handler = Coordinator.GetStorageHandlerFor(child);
                    handler.Save(child, mode);
                };
                x.Restore = (instance, getKey, mode) => {
                    var child = info.GetValue(instance, null);
                    if(child == null)
                        return;

                    var handler = Coordinator.GetStorageHandlerFor(child);
                    handler.Restore(child, mode);
                };
            });
        }

        /// <summary>
        /// Adds a new storage instruction.
        /// </summary>
        /// <returns>The builder.</returns>
        public StorageInstructionBuilder<T> AddInstruction() {
            var instruction = new StorageInstruction<T> { Owner = this };
            instructions.Add(instruction);
            return new StorageInstructionBuilder<T>(instruction);
        }

        /// <summary>
        /// Uses this handler to save a particular instance using instructions that support the provided mode.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="mode">The storage mode.</param>
        public virtual void Save(T instance, StorageMode mode) {
            foreach(var instruction in instructions) {
                var key = instruction.Key;
                if (instruction.StorageMechanism.Supports(mode)) {
                    instruction.Save(instance, () => GetKey(instance, key), mode);
                }
            }
        }

        /// <summary>
        /// Uses this handler to restore a particular instance using instructions that support the provided mode.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="mode">The mode.</param>
        public virtual void Restore(T instance, StorageMode mode) {
            foreach(var instruction in instructions) {
                var key = instruction.Key;
                if (instruction.StorageMechanism.Supports(mode)) {
                    instruction.Restore(instance, () => GetKey(instance, key), mode);
                }
            }
        }

        string GetKey(T instance, string detailKey) {
            var id = getId(instance);
            return typeof(T).FullName + (id == null ? "" : "_" + id) + "_" + detailKey;
        }

        bool IStorageHandler.Handles(object instance) {
            return instance is T;
        }

        void IStorageHandler.Save(object instance, StorageMode mode) {
            Save((T)instance, mode);
        }

        void IStorageHandler.Restore(object instance, StorageMode mode) {
            Restore((T)instance, mode);
        }
    }
}