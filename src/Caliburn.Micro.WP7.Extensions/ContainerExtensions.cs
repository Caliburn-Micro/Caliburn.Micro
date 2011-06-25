namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension methods for the <see cref="SimpleContainer"/>.
    /// </summary>
    public static class ContainerExtensions {
        /// <summary>
        /// Registers a singleton.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer Singleton<TImplementation>(this SimpleContainer container) {
            container.RegisterSingleton(typeof(TImplementation), null, typeof(TImplementation));
            return container;
        }

        /// <summary>
        /// Registers a singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer Singleton<TService, TImplementation>(this SimpleContainer container)
            where TImplementation : TService {
            container.RegisterSingleton(typeof(TService), null, typeof(TImplementation));
            return container;
        }

        /// <summary>
        /// Registers an service to be created on each request.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer PerRequest<TService, TImplementation>(this SimpleContainer container)
            where TImplementation : TService {
            container.RegisterPerRequest(typeof(TService), null, typeof(TImplementation));
            return container;
        }

        /// <summary>
        /// Registers an service to be created on each request.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer PerRequest<TImplementation>(this SimpleContainer container) {
            container.RegisterPerRequest(typeof(TImplementation), null, typeof(TImplementation));
            return container;
        }

        /// <summary>
        /// Registers an instance with the container.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="instance">The instance.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer Instance<TService>(this SimpleContainer container, TService instance) {
            container.RegisterInstance(typeof(TService), null, instance);
            return container;
        }

        /// <summary>
        /// Registers a custom service handler with the container.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer Handler<TService>(this SimpleContainer container, Func<SimpleContainer, object> handler) {
            container.RegisterHandler(typeof(TService), null, handler);
            return container;
        }

        /// <summary>
        /// Registers all specified types in an assembly as singletong in the container.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="assembly">The assembly.</param>
        /// <param name="filter">The type filter.</param>
        /// <returns>The container.</returns>
        public static SimpleContainer AllTypesOf<TService>(this SimpleContainer container, Assembly assembly, Func<Type, bool> filter = null) {
            if(filter == null)
                filter = type => true;

            var types = from type in assembly.GetTypes()
                        where typeof(TService).IsAssignableFrom(type)
                              && !type.IsAbstract
                              && !type.IsInterface
                              && filter(type)
                        select type;

            foreach(var type in types) {
                container.RegisterSingleton(typeof(TService), null, type);
            }

            return container;
        }
    }
}