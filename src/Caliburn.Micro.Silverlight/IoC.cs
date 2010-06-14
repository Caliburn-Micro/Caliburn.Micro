namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public static class IoC
    {
        public static Func<Type, string, object> GetInstance;
        public static Func<Type, IEnumerable<object>> GetAllInstances;

        public static T Get<T>()
        {
            return (T)GetInstance(typeof(T), null);
        }
    }
}