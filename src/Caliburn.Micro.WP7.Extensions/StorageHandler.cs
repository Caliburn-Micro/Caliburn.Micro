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
            var id = getId(instance);
            var baseKey = typeof(T).FullName + (id == null ? "" : "_" + id);

            foreach(var instruction in instructions.Where(x => x.StorageMechanism.Supports(mode))) {
                var key = baseKey + "_" + instruction.Key;
                var value = instruction.Get(instance);

                instruction.StorageMechanism.Store(key, value);
            }
        }

        public virtual void Restore(T instance) {
            var id = getId(instance);
            var baseKey = typeof(T).FullName + (id == null ? "" : "_" + id);

            foreach(var instruction in instructions) {
                var key = baseKey + "_" + instruction.Key;
                var value = instruction.StorageMechanism.Get(key);

                instruction.Set(instance, value);
                instruction.StorageMechanism.Delete(key);
            }
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