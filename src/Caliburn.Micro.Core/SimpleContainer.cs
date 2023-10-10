using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro
{
    /// <summary>
    ///   A simple IoC container.
    /// </summary>
    public class SimpleContainer
    {
        private static readonly Type delegateType = typeof(Delegate);
        private static readonly Type enumerableType = typeof(IEnumerable);
        private static readonly TypeInfo enumerableTypeInfo = enumerableType.GetTypeInfo();
        private static readonly TypeInfo delegateTypeInfo = delegateType.GetTypeInfo();
        private readonly Type simpleContainerType = typeof(SimpleContainer);
        private readonly List<ContainerEntry> entries;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SimpleContainer" /> class.
        /// </summary>
        public SimpleContainer()
        {
            entries = new List<ContainerEntry>();
        }

        private SimpleContainer(IEnumerable<ContainerEntry> entries)
        {
            this.entries = new List<ContainerEntry>(entries);
        }

        /// <summary>
        /// Whether to enable recursive property injection for all resolutions.
        /// </summary>
        public bool EnablePropertyInjection { get; set; }

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

        /// <summary>
        ///   Unregisters any handlers for the service/key that have previously been registered.
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key.</param>
        public void UnregisterHandler(Type service, string key)
        {
            var entry = GetEntry(service, key);
            if (entry != null)
            {
                entries.Remove(entry);
            }
        }

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
                var instance = entry.Single()(this);

                if (EnablePropertyInjection && instance != null)
                {
                    BuildUp(instance);
                }

                return instance;
            }

            if (service == null)
            {
                return null;
            }
            TypeInfo serviceTypeInfo = service.GetTypeInfo();

            if (delegateTypeInfo.IsAssignableFrom(serviceTypeInfo))
            {
                var typeToCreate = serviceTypeInfo.GenericTypeArguments[0];
                var factoryFactoryType = typeof(FactoryFactory<>).MakeGenericType(typeToCreate);
                var factoryFactoryHost = Activator.CreateInstance(factoryFactoryType);
                var factoryFactoryMethod = factoryFactoryType.GetRuntimeMethod("Create", new Type[] { simpleContainerType });
                return factoryFactoryMethod.Invoke(factoryFactoryHost, new object[] { this });
            }

            if (enumerableTypeInfo.IsAssignableFrom(serviceTypeInfo) && serviceTypeInfo.IsGenericType)
            {
                var listType = serviceTypeInfo.GenericTypeArguments[0];
                var instances = GetAllInstances(listType).ToList();
                var array = Array.CreateInstance(listType, instances.Count);

                for (var i = 0; i < array.Length; i++)
                {
                    if (EnablePropertyInjection)
                    {
                        BuildUp(instances[i]);
                    }

                    array.SetValue(instances[i], i);
                }

                return array;
            }

            return null;
        }

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
        ///   Requests all instances of a given type and the given key (default null).
        /// </summary>
        /// <param name = "service">The service.</param>
        /// <param name = "key">The key shared by those instances</param>
        /// <returns>All the instances or an empty enumerable if none are found.</returns>
        public IEnumerable<object> GetAllInstances(Type service, string key = null)
        {
            var entries = GetEntry(service, key);

            if (entries == null)
            {
                return new object[0];
            }

            var instances = entries.Select(e => e(this));

            foreach (var instance in instances)
            {
                if (EnablePropertyInjection && instance != null)
                {
                    BuildUp(instance);
                }
            }

            return instances;
        }

        /// <summary>
        ///   Pushes dependencies into an existing instance based on interface properties with setters.
        /// </summary>
        /// <param name = "instance">The instance.</param>
        public void BuildUp(object instance)
        {
            var properties = instance
                .GetType()
                .GetRuntimeProperties()
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType.GetTypeInfo().IsInterface);

            foreach (var property in properties)
            {
                var value = GetInstance(property.PropertyType, null);

                if (value != null)
                {
                    property.SetValue(instance, value, null);
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

        private ContainerEntry GetOrCreateEntry(Type service, string key)
        {
            var entry = GetEntry(service, key);
            if (entry == null)
            {
                entry = new ContainerEntry { Service = service, Key = key };
                entries.Add(entry);
            }

            return entry;
        }

        private ContainerEntry GetEntry(Type service, string key)
        {
            if (service == null)
            {
                return entries.FirstOrDefault(x => x.Key == key);
            }

            if (key == null)
            {
                return entries.FirstOrDefault(x => x.Service == service && string.IsNullOrEmpty(x.Key))
                       ?? entries.FirstOrDefault(x => x.Service == service);
            }

            return entries.FirstOrDefault(x => x.Service == service && x.Key == key);
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
            var instance = args.Length > 0 ? System.Activator.CreateInstance(type, args) : System.Activator.CreateInstance(type);
            Activated(instance);
            return instance;
        }

        /// <summary>
        ///   Occurs when a new instance is created.
        /// </summary>
        public event Action<object> Activated = delegate { };

        private object[] DetermineConstructorArgs(Type implementation)
        {
            var args = new List<object>();
            var constructor = SelectEligibleConstructor(implementation);

            if (constructor != null)
            {
                args.AddRange(constructor.GetParameters().Select(info => GetInstance(info.ParameterType, null)));
            }

            return args.ToArray();
        }

        private ConstructorInfo SelectEligibleConstructor(Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors
                .Where(c => c.IsPublic)
                .Select(c => new
                {
                    Constructor = c,
                    HandledParamters = c.GetParameters().Count(p => HasHandler(p.ParameterType, null))
                })
                .OrderByDescending(c => c.HandledParamters)
                .Select(c => c.Constructor)
                .FirstOrDefault();
        }

        private class ContainerEntry : List<Func<SimpleContainer, object>>
        {
            public string Key;
            public Type Service;
        }

        private class FactoryFactory<T>
        {
            public Func<T> Create(SimpleContainer container)
            {
                return () => (T)container.GetInstance(typeof(T), null);
            }
        }
    }
}
