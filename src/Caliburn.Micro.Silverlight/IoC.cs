namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used by the framework to pull instances from an IoC container and to inject dependencies into certain existing classes.
    /// </summary>
    public static class IoC {
        /// <summary>
        /// Gets an instance by type and key.
        /// </summary>
        public static Func<Type, string, object> GetInstance;

        /// <summary>
        /// Gets all instances of a particular type.
        /// </summary>
        public static Func<Type, IEnumerable<object>> GetAllInstances;

        /// <summary>
        /// Passes an existing instance to the IoC container to enable dependencies to be injected.
        /// </summary>
        public static Action<object> BuildUp;

        /// <summary>
        /// Gets an instance by type.
        /// </summary>
        /// <typeparam name="T">The type to resolve from the container.</typeparam>
        /// <returns>The resolved instance.</returns>
        public static T Get<T>() {
            return (T)GetInstance(typeof(T), null);
        }

        /// <summary>
        /// Gets an instance from the container using type and key.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <param name="key">The key to look up.</param>
        /// <returns>The resolved instance.</returns>
        public static T Get<T>(string key) {
            return (T)GetInstance(typeof(T), key);
        }
    }
}