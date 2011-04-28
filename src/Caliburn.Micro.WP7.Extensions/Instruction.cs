namespace Caliburn.Micro {
    using System;

    public class Instruction<T> {
        public string Key;
        public Func<T, object> Get;
        public Action<T, object> Set;
        public IStorage Storage;
    }
}