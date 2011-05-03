namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public StorageInstructionBuilder<T> EntireGraph<TService>() {
            return AddInstruction().Configure(x => {
                x.Key = "ObjectGraph";
                x.Get = instance => instance;
                x.Set = (instance, storage, getKey) => { };
                x.PropertyChanged += (s, e) => {
                    if(e.PropertyName == "StorageMechanism" && x.StorageMechanism != null)
                        x.StorageMechanism.RegisterWithContainer(typeof(TService), GetKey(default(T), x.Key), typeof(T));
                };
            });
        }

        public StorageInstructionBuilder<T> Property(Expression<Func<T, object>> property) {
            var info = (PropertyInfo)property.GetMemberInfo();

            return AddInstruction().Configure(x => {
                x.Key = info.Name;
                x.Get = instance => info.GetValue(instance, null);
                x.Set = (instance, storage, getKey) => {
                    object value;
                    if(storage.TryGet(getKey(), out value))
                        info.SetValue(instance, value, null);
                };
            });
        }

        public StorageInstructionBuilder<T> AddInstruction() {
            var instruction = new StorageInstruction<T> { Owner = this };
            instructions.Add(instruction);
            return new StorageInstructionBuilder<T>(instruction);
        }

        public virtual void Save(T instance, StorageMode mode) {
            foreach(var instruction in instructions.Where(x => x.StorageMechanism.Supports(mode))) {
                var key = GetKey(instance, instruction.Key);
                var value = instruction.Get(instance);

                instruction.StorageMechanism.Store(key, value);
            }
        }

        public virtual void Restore(T instance) {
            foreach(var instruction in instructions) {
                var current = instruction;
                current.Set(instance, current.StorageMechanism, () => GetKey(instance, current.Key));
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

        void IStorageHandler.Restore(object instance) {
            Restore((T)instance);
        }
    }
}