namespace Caliburn.Micro
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///   A simple IoC container.
    /// </summary>
    public class SimpleContainer
    {
#if WinRT
        static readonly TypeInfo delegateType = typeof(Delegate).GetTypeInfo();
        static readonly TypeInfo enumerableType = typeof(IEnumerable).GetTypeInfo();
#else
        static readonly Type delegateType = typeof(Delegate);
        static readonly Type enumerableType = typeof(IEnumerable);
#endif

        readonly List<ContainerEntry> entries;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SimpleContainer" /> class.
        /// </summary>
        public SimpleContainer()
        {
            entries = new List<ContainerEntry>();
        }

        SimpleContainer(IEnumerable<ContainerEntry> entries)
        {
            this.entries = new List<ContainerEntry>(entries);
        }

        /// <summary>
        ///   Registers the instance.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        /// <param name = "implementation">The implementation.</param>
        public void RegisterInstance(Type service, string key, object implementation)
        {
            RegisterHandler(service, key, container => implementation);
        }

        /// <summary>
        ///   Registers the class so that a new instance is created on every request.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        /// <param name = "implementation">The implementation.</param>
        public void RegisterPerRequest(Type service, string key, Type implementation)
        {
            RegisterHandler(service, key, container => container.BuildInstance(implementation));
        }

        /// <summary>
        ///   Registers the class so that it is created once, on first request, and the same instance is returned to all requestors thereafter.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        /// <param name = "implementation">The implementation.</param>
        public void RegisterSingleton(Type service, string key, Type implementation)
        {
            object singleton = null;
            RegisterHandler(service, key, container => singleton ?? (singleton = container.BuildInstance(implementation)));
        }

        /// <summary>
        ///   Registers a custom handler for serving requests from the container.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        /// <param name = "handler">The handler.</param>
        public void RegisterHandler(Type service, string key, Func<SimpleContainer, object> handler)
        {
            GetOrCreateEntry(service, key).Add(handler);
        }

#if WinRT
        /// <summary>
        ///   Requests an instance.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        /// <returns>The instance, or null if a handler is not found.</returns>
        public object GetInstance(Type service, string key)
        {
            var entry = GetEntry(service, key);
            if (entry != null)
            {
                return entry.Single()(this);
            }

            var serviceInfo = service.GetTypeInfo();

            if (delegateType.IsAssignableFrom(serviceInfo))
            {
                var typeToCreate = serviceInfo.GenericTypeArguments[0];
                var factoryFactoryType = typeof(FactoryFactory<>).MakeGenericType(typeToCreate);
                var factoryFactoryHost = Activator.CreateInstance(factoryFactoryType);
                var factoryFactoryMethod = factoryFactoryType.GetTypeInfo().DeclaredMethods.First(x => x.Name == "Create");
                return factoryFactoryMethod.Invoke(factoryFactoryHost, new object[] { this });
            }

            if (enumerableType.IsAssignableFrom(serviceInfo))
            {
                var listType = service.GenericTypeArguments[0];
                var instances = GetAllInstances(listType).ToList();
                var array = Array.CreateInstance(listType, instances.Count);

                for (var i = 0; i < array.Length; i++)
                {
                    array.SetValue(instances[i], i);
                }

                return array;
            }

            if (IsConcrete(service))
            {
                RegisterPerRequest(service, key, service);

                return GetInstance(service, key);
            }

            return null;
        }

        private bool IsConcrete(Type service)
        {
            var serviceInfo = service.GetTypeInfo();

            return !serviceInfo.IsAbstract && !serviceInfo.IsInterface;
        }
#else
        /// <summary>
        ///   Requests an instance.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        /// <returns>The instance, or null if a handler is not found.</returns>
        public object GetInstance(Type service, string key) {
            var entry = GetEntry(service, key);
            if(entry != null) {
                return entry.Single()(this);
            }

            if(delegateType.IsAssignableFrom(service)) {
                var typeToCreate = service.GetGenericArguments()[0];
                var factoryFactoryType = typeof(FactoryFactory<>).MakeGenericType(typeToCreate);
                var factoryFactoryHost = Activator.CreateInstance(factoryFactoryType);
                var factoryFactoryMethod = factoryFactoryType.GetMethod("Create");
                return factoryFactoryMethod.Invoke(factoryFactoryHost, new object[] { this });
            }

            if(enumerableType.IsAssignableFrom(service)) {
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
#endif
        /// <summary>
        /// Determines if a handler for the service/key has previously been registered.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <returns>True if a handler is registere; false otherwise.</returns>
        public bool HasHandler(Type service, string key)
        {
            return GetEntry(service, key) != null;
        }

        /// <summary>
        ///   Requests all instances of a given type.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <returns>All the instances or an empty enumerable if none are found.</returns>
        public IEnumerable<object> GetAllInstances(Type service)
        {
            var entry = GetEntry(service, null);
            return entry != null ? entry.Select(x => x(this)) : new object[0];
        }

        /// <summary>
        ///   Pushes dependencies into an existing instance based on interface properties with setters.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        public void BuildUp(object instance)
        {
#if WinRT
            var injectables = from property in instance.GetType().GetTypeInfo().DeclaredProperties
                              where property.CanRead && property.CanWrite && property.PropertyType.GetTypeInfo().IsInterface
                              select property;
#else
            var injectables = from property in instance.GetType().GetProperties()
                              where property.CanRead && property.CanWrite && property.PropertyType.IsInterface
                              select property;
#endif

            foreach (var propertyInfo in injectables)
            {
                var injection = GetAllInstances(propertyInfo.PropertyType).ToArray();
                if (injection.Any())
                {
                    propertyInfo.SetValue(instance, injection.First(), null);
                }
            }
        }

        /// <summary>
        /// Creates a child container.
        /// </summary>
        /// <returns>A new container.</returns>
        public SimpleContainer CreateChildContainer()
        {
            return new SimpleContainer(entries);
        }

        ContainerEntry GetOrCreateEntry(Type service, string key)
        {
            var entry = GetEntry(service, key);
            if (entry == null)
            {
                entry = new ContainerEntry { Service = service, Key = key };
                entries.Add(entry);
            }

            return entry;
        }

        ContainerEntry GetEntry(Type service, string key)
        {
            if (service == null)
            {
                return entries.Where(x => x.Key == key).FirstOrDefault();
            }

            if (key == null)
            {
                return entries.Where(x => x.Service == service && x.Key == null).FirstOrDefault()
                       ?? entries.Where(x => x.Service == service).FirstOrDefault();
            }

            return entries.Where(x => x.Service == service && x.Key == key).FirstOrDefault();
        }

        /// <summary>
        ///   Actually does the work of creating the instance and satisfying it's constructor dependencies.
        /// </summary>
        /// <param name = "type">The type.</param>
        /// <returns></returns>
        protected object BuildInstance(Type type)
        {
            var args = DetermineConstructorArgs(type);
            return ActivateInstance(type, args);
        }

        /// <summary>
        ///   Creates an instance of the type with the specified constructor arguments.
        /// </summary>
        /// <param name = "type">The type.</param>
        /// <param name = "args">The constructor args.</param>
        /// <returns>The created instance.</returns>
        protected virtual object ActivateInstance(Type type, object[] args)
        {
            var instance = args.Length > 0 ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
            Activated(instance);
            return instance;
        }

        /// <summary>
        ///   Occurs when a new instance is created.
        /// </summary>
        public event Action<object> Activated = delegate { };

        object[] DetermineConstructorArgs(Type implementation)
        {
            var args = new List<object>();
            var constructor = SelectEligibleConstructor(implementation);

            if (constructor != null)
                args.AddRange(constructor.GetParameters().Select(info => GetInstance(info.ParameterType, null)));

            return args.ToArray();
        }

#if WinRT
        static ConstructorInfo SelectEligibleConstructor(Type type)
        {
            return (from c in type.GetTypeInfo().DeclaredConstructors
                    orderby c.GetParameters().Length descending
                    select c).FirstOrDefault();
        }
#else
        static ConstructorInfo SelectEligibleConstructor(Type type) {
            return (from c in type.GetConstructors()
                    orderby c.GetParameters().Length descending
                    select c).FirstOrDefault();
        }
#endif

        class ContainerEntry : List<Func<SimpleContainer, object>>
        {
            public string Key;
            public Type Service;
        }

        class FactoryFactory<T>
        {
            public Func<T> Create(SimpleContainer container)
            {
                return () => (T)container.GetInstance(typeof(T), null);
            }
        }
    }
}