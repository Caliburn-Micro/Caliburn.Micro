namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public abstract class StorageHandler<T> : IStorageHandler {
        readonly List<StorageInstruction<T>> instructions = new List<StorageInstruction<T>>();
        Func<T, object> getId = instance => null;

        public void Id(Func<T, object> getter) {
            getId = getter;
        }

        public StorageCoordinator Coordinator { get; set; }

        public StorageInstructionBuilder<T> EntireGraph() {
            return EntireGraph<T>();
        }

        public abstract void Configure();

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

                    var handler = Coordinator.GetStorageHandlerFor(instance);
                    handler.Restore(child, mode);
                };
            });
        }

        public StorageInstructionBuilder<T> AddInstruction() {
            var instruction = new StorageInstruction<T> { Owner = this };
            instructions.Add(instruction);
            return new StorageInstructionBuilder<T>(instruction);
        }

        public virtual void Save(T instance, StorageMode mode) {
            foreach(var instruction in instructions) {
                var key = instruction.Key;
                if(instruction.StorageMechanism.Supports(mode))
                    instruction.Save(instance, () => GetKey(instance, key), mode);
            }
        }

        public virtual void Restore(T instance, StorageMode mode) {
            foreach(var instruction in instructions) {
                var key = instruction.Key;
                if(instruction.StorageMechanism.Supports(mode))
                    instruction.Restore(instance, () => GetKey(instance, key), mode);
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