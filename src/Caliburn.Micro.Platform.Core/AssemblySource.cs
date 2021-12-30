﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro
{
    /// <summary>
    /// A source of assemblies that are inspectable by the framework.
    /// </summary>
    public static class AssemblySource
    {
        /// <summary>
        /// The singleton instance of the AssemblySource used by the framework.
        /// </summary>
        public static readonly IObservableCollection<Assembly> Instance = new BindableCollection<Assembly>();

        public static int AssemblyCount
        {
            get
            {
                int amt = 0;
                if (Instance != null)
                {
                    amt = Instance.Count;
                }

                return amt;
            }
        }

        /// <summary>
        /// Adds a collection of assemblies to AssemblySource
        /// </summary>
        /// <param name="assemblies">The assemblies to add</param>
        public static void AddRange(IEnumerable<Assembly> assemblies)
        {
            foreach(var assembly in assemblies)
            {
                try
                {
                    if (!Instance.Contains(assembly))
                        Instance.Add(assembly);
                }
                catch (ArgumentException)
                {
                    
                    // ignore
                }
            }
        }

        /// <summary>
        /// Finds a type which matches one of the elements in the sequence of names.
        /// </summary>
        public static Func<IEnumerable<string>, Type> FindTypeByNames = names =>
        {
            if (names == null)
            {
                return null;
            }

            var type = names
                .Join(Instance.SelectMany(a => a.ExportedTypes), n => n, t => t.FullName, (n, t) => t)
                .FirstOrDefault();
            return type;
        };
    }

    /// <summary>
    /// A caching subsystem for <see cref="AssemblySource"/>.
    /// </summary>
    public static class AssemblySourceCache
    {
        private static bool isInstalled;
        private static readonly IDictionary<String, Type> TypeNameCache = new Dictionary<string, Type>();

        /// <summary>
        /// Extracts the types from the spezified assembly for storing in the cache.
        /// </summary>
        public static Func<Assembly, IEnumerable<Type>> ExtractTypes = assembly =>
            assembly.ExportedTypes
                .Where(t =>
                    typeof(INotifyPropertyChanged).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()));

        /// <summary>
        /// Installs the caching subsystem.
        /// </summary>
        public static System.Action Install = () =>
        {
            if (isInstalled)
            {
                return;
            }

            isInstalled = true;

            AssemblySource.Instance.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        e.NewItems.OfType<Assembly>()
                            .SelectMany(a => ExtractTypes(a))
                            .Apply(AddTypeAssembly);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Reset:
                        TypeNameCache.Clear();
                        AssemblySource.Instance
                            .SelectMany(a => ExtractTypes(a))
                            .Apply(AddTypeAssembly);
                        break;
                }
            };

            AssemblySource.Instance.Refresh();

            AssemblySource.FindTypeByNames = names =>
            {
                if (names == null)
                {
                    return null;
                }

                var type = names.Select(n => TypeNameCache.GetValueOrDefault(n)).FirstOrDefault(t => t != null);
                return type;
            };
        };

        private static void AddTypeAssembly(Type type)
        {
            if (!TypeNameCache.ContainsKey(type.FullName))
            {
                TypeNameCache.Add(type.FullName, type);
            }
        }
    }
}
