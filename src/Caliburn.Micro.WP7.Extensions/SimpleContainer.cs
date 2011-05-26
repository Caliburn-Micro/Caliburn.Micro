namespace Caliburn.Micro {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A simple IoC container.
    /// </summary>
    public class SimpleContainer {
        readonly List<ContainerEntry> entries = new List<ContainerEntry>();

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterInstance(Type service, string key, object implementation) {
            RegisterHandler(service, key, () => implementation);
        }

        /// <summary>
        /// Registers the class so that a new instance is created on every request.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterPerRequest(Type service, string key, Type implementation) {
            RegisterHandler(service, key, () => BuildInstance(implementation));
        }

        /// <summary>
        /// Registers the class so that it is created once, on first request, and the same instance is returned to all requestors thereafter.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterSingleton(Type service, string key, Type implementation) {
            object singleton = null;
            RegisterHandler(service, key, () => singleton ?? (singleton = BuildInstance(implementation)));
        }

        /// <summary>
        /// Registers a custom handler for serving requests from the container.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="handler">The handler.</param>
        public void RegisterHandler(Type service, string key, Func<object> handler) {
            GetOrCreateEntry(service, key).Add(handler);
        }

        /// <summary>
        /// Requests an instance.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance, or null if a handler is not found.</returns>
        public object GetInstance(Type service, string key) {
            var entry = GetEntry(service, key);
            if (entry != null) {
                return entry.Single()();
            }

            if(typeof(Delegate).IsAssignableFrom(service)) {
                var typeToCreate = service.GetGenericArguments()[0];
                var factoryFactoryType = typeof(FactoryFactory<>).MakeGenericType(typeToCreate);
                var factoryFactoryHost = Activator.CreateInstance(factoryFactoryType);
                var factoryFactoryMethod = factoryFactoryType.GetMethod("Create");
                return factoryFactoryMethod.Invoke(factoryFactoryHost, new object[] { this });
            }

            if(typeof(IEnumerable).IsAssignableFrom(service)) {
                var listType = service.GetGenericArguments()[0];
                var instances = GetAllInstances(listType).ToList();
                var array = Array.CreateInstance(listType, instances.Count);

                for(var i = 0; i < array.Length; i++) {
                    array.SetValue(instances[i], i);
                }

                return array;
            }

            return null;
        }

        /// <summary>
        /// Requests all instances of a given type.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>All the instances or an empty enumerable if none are found.</returns>
        public IEnumerable<object> GetAllInstances(Type service) {
            var entry = GetEntry(service, null);
            return entry != null ? entry.Select(x => x()) : new object[0];
        }

        /// <summary>
        /// Pushes dependencies into an existing instance based on interface properties with setters.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void BuildUp(object instance) {
            var injectables = from property in instance.GetType().GetProperties()
                              where property.CanRead && property.CanWrite && property.PropertyType.IsInterface
                              select property;

            injectables.Apply(x => {
                var injection = GetAllInstances(x.PropertyType);
                if (injection.Any()) {
                    x.SetValue(instance, injection.First(), null);
                }
            });
        }

        ContainerEntry GetOrCreateEntry(Type service, string key) {
            var entry = GetEntry(service, key);
            if(entry == null) {
                entry = new ContainerEntry { Service = service, Key = key };
                entries.Add(entry);
            }

            return entry;
        }

        ContainerEntry GetEntry(Type service, string key) {
            return service == null
                       ? entries.Where(x => x.Key == key).FirstOrDefault()
                       : entries.Where(x => x.Service == service && x.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// Actually does the work of creating the instance and satisfying it's constructor dependencies.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected object BuildInstance(Type type) {
            var args = DetermineConstructorArgs(type);
            return ActivateInstance(type, args);
        }

        /// <summary>
        /// Creates an instance of the type with the specified constructor arguments.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The constructor args.</param>
        /// <returns>The created instance.</returns>
        protected virtual object ActivateInstance(Type type, object[] args) {
            var instance = args.Length > 0 ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
            Activated(instance);
            return instance;
        }

        /// <summary>
        /// Occurs when a new instance is created.
        /// </summary>
        public event Action<object> Activated = delegate { };

        object[] DetermineConstructorArgs(Type implementation) {
            var args = new List<object>();
            var constructor = SelectEligibleConstructor(implementation);

            if(constructor != null)
                args.AddRange(constructor.GetParameters().Select(info => GetInstance(info.ParameterType, null)));

            return args.ToArray();
        }

        static ConstructorInfo SelectEligibleConstructor(Type type) {
            return (from c in type.GetConstructors()
                    orderby c.GetParameters().Length descending
                    select c).FirstOrDefault();
        }

        class ContainerEntry : List<Func<object>> {
            public string Key;
            public Type Service;
        }

        class FactoryFactory<T> {
            public Func<T> Create(SimpleContainer container) {
                return () => (T)container.GetInstance(typeof(T), null);
            }
        }
    }
}