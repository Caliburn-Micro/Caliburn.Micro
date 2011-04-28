namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class StorageMap<T> : IStorageHandler {
        readonly List<Instruction<T>> instructions = new List<Instruction<T>>();
        Func<T, object> getId = instance => null;

        public void Id(Func<T, object> getter) {
            getId = getter;
        }

        public InstructionBuilder<T> Property(Expression<Func<T, object>> property) {
            var info = (PropertyInfo)property.GetMemberInfo();
            Func<T, object> getter = instance => info.GetValue(instance, null);
            Action<T, object> setter = (instance, value) => info.SetValue(instance, value, null);
            var instruction = AddInstruction(info.Name, getter, setter);
            return new InstructionBuilder<T>(instruction);
        }

        public Instruction<T> AddInstruction(string key = null, Func<T, object> getter = null, Action<T, object> setter = null, IStorage storage = null) {
            var instruction = new Instruction<T>
            {
                Key = key,
                Get = getter,
                Set = setter,
                Storage = storage
            };
            instructions.Add(instruction);
            return instruction;
        }

        public virtual void Save(T instance, StorageMode mode) {
            var id = getId(instance);
            var baseKey = typeof(T).FullName + (id == null ? "" : "_" + id);

            foreach(var instruction in instructions.Where(x => x.Storage.Supports(mode))) {
                var value = instruction.Get(instance);
                instruction.Storage.Put(baseKey + "_" + instruction.Key, value);
            }
        }

        public virtual void Restore(T instance) {
            var id = getId(instance);
            var baseKey = typeof(T).FullName + (id == null ? "" : "_" + id);

            foreach(var instruction in instructions) {
                var value = instruction.Storage.Get(baseKey + "_" + instruction.Key);
                instruction.Set(instance, value);
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