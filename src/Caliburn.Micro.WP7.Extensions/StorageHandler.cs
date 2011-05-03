namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class StorageHandler<T> : IStorageHandler {
        readonly List<StorageInstruction<T>> instructions = new List<StorageInstruction<T>>();
        Func<T, object> getId = instance => null;

        public void Id(Func<T, object> getter) {
            getId = getter;
        }

        public StorageCoordinator Coordinator { get; set; }

        public StorageInstructionBuilder<T> EntireGraph() {
            return EntireGraph<T>();
        }

        public StorageInstructionBuilder<T> EntireGraph<TService>() {
            return AddInstruction().Configure(x => {
                x.Key = "ObjectGraph";
                x.Get = instance => instance;
                x.Set = (instance, value) => { };
                x.PropertyChanged += (s, e) => {
                    if(e.PropertyName == "StorageMechanism" && x.StorageMechanism != null)
                        x.StorageMechanism.RegisterWithContainer(typeof(TService), GetKey(typeof(T).FullName, x.Key), typeof(T));
                };
            });
        }

        public StorageInstructionBuilder<T> Property(Expression<Func<T, object>> property) {
            var info = (PropertyInfo)property.GetMemberInfo();

            return AddInstruction().Configure(x => {
                x.Key = info.Name;
                x.Get = instance => info.GetValue(instance, null);
                x.Set = (instance, value) => info.SetValue(instance, value, null);
            });
        }

        public StorageInstructionBuilder<T> AddInstruction() {
            var instruction = new StorageInstruction<T> { Owner = this };
            instructions.Add(instruction);
            return new StorageInstructionBuilder<T>(instruction);
        }

        public virtual void Save(T instance, StorageMode mode) {
            var baseKey = GetBaseKey(instance);

            foreach(var instruction in instructions.Where(x => x.StorageMechanism.Supports(mode))) {
                var key = GetKey(baseKey, instruction.Key);
                var value = instruction.Get(instance);

                instruction.StorageMechanism.Store(key, value);
            }
        }

        public virtual void Restore(T instance) {
            var baseKey = GetBaseKey(instance);

            foreach(var instruction in instructions) {
                string key = GetKey(baseKey, instruction.Key);
                var value = instruction.StorageMechanism.Get(key);

                instruction.Set(instance, value);
                instruction.StorageMechanism.Delete(key);
            }
        }

        string GetKey(string baseKey, string detailKey) {
            return baseKey + "_" + detailKey;
        }

        string GetBaseKey(T instance) {
            var id = getId(instance);
            return typeof(T).FullName + (id == null ? "" : "_" + id);
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