using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro {
    /// <summary>
    /// A caching subsystem for <see cref="AssemblySource"/>.
    /// </summary>
    public static class AssemblySourceCache {
        private static readonly IDictionary<string, Type> TypeNameCache = new Dictionary<string, Type>();

        private static bool isInstalled;

        /// <summary>
        /// Gets or sets func to extracts the types from the spezified assembly for storing in the cache.
        /// </summary>
        public static Func<Assembly, IEnumerable<Type>> ExtractTypes { get; set; } = assembly =>
            assembly.ExportedTypes
                .Where(t =>
                    typeof(INotifyPropertyChanged).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()));

        /// <summary>
        /// Gets or sets func to installs the caching subsystem.
        /// </summary>
        public static System.Action Install { get; set; }
            = () => {
                if (isInstalled) {
                    return;
                }

                isInstalled = true;

                AssemblySource.Instance.CollectionChanged += (s, e) => {
                    switch (e.Action) {
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

                AssemblySource.FindTypeByNames = names => {
                    if (names == null) {
                        return null;
                    }

                    Type type = names.Select(n => TypeNameCache.GetValueOrDefault(n)).FirstOrDefault(t => t != null);
                    return type;
                };
            };

        private static void AddTypeAssembly(Type type) {
            if (!TypeNameCache.ContainsKey(type.FullName)) {
                TypeNameCache.Add(type.FullName, type);
            }
        }
    }
}
