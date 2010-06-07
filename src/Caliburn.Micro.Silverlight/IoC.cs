namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IoC
    {
        static Func<Type, string, object> getInstanceImplementation;
        static Func<Type, IEnumerable<object>> getAllInstancesImplementation;

        public static void Initialize(Func<Type, string, object> getInstance, Func<Type, IEnumerable<object >> getAllInstances)
        {
            getInstanceImplementation = getInstance;
            getAllInstancesImplementation = getAllInstances;
        }

        public static T GetInstance<T>()
        {
            return (T)GetInstance(typeof(T));
        }

        public static object GetInstance(Type serviceType)
        {
            return GetInstance(serviceType, null);
        }

        public static object GetInstance(Type serviceType, string key)
        {
            return getInstanceImplementation(serviceType, key);
        }

        public static IEnumerable<T> GetAllInstances<T>()
        {
            return GetAllInstances(typeof(T)).OfType<T>();
        }

        public static IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return getAllInstancesImplementation(serviceType);
        }
    }
}