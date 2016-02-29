namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
#if !WinRT
    using System.Windows;
#else
    using Windows.UI.Xaml;
#endif

    /// <summary>
    /// A source of assemblies that are inspectable by the framework.
    /// </summary>
    public static class AssemblySource {
        /// <summary>
        /// The singleton instance of the AssemblySource used by the framework.
        /// </summary>
        public static readonly IObservableCollection<Assembly> Instance = new BindableCollection<Assembly>();

        /// <summary>
        /// Finds a type which matches one of the elements in the sequence of names.
        /// </summary>
        public static Func<IEnumerable<string>, Type> FindTypeByNames = names => {
            if (names == null) {
                return null;
            }

            var type = names
                .Join(Instance.SelectMany(a => a.GetExportedTypes()), n => n, t => t.FullName, (n, t) => t)
                .FirstOrDefault();
            return type;
        };
    }

    /// <summary>
    /// A caching subsystem for <see cref="AssemblySource"/>.
    /// </summary>
    public static class AssemblySourceCache {
        static bool isInstalled;
        static readonly IDictionary<String, Type> TypeNameCache = new Dictionary<string, Type>();

        /// <summary>
        /// Extracts the types from the spezified assembly for storing in the cache.
        /// </summary>
        public static Func<Assembly, IEnumerable<Type>> ExtractTypes = assembly =>
            assembly.GetExportedTypes()
                .Where(t =>
#if !CORE
                    typeof(UIElement).IsAssignableFrom(t) ||
#endif
                    typeof(INotifyPropertyChanged).IsAssignableFrom(t));

        /// <summary>
        /// Installs the caching subsystem.
        /// </summary>
        public static void Install() {
            if (isInstalled) return;
            isInstalled = true;

            AssemblySource.Instance.CollectionChanged += (s, e) => {
                switch (e.Action) {
                    case NotifyCollectionChangedAction.Add:
                        e.NewItems.OfType<Assembly>()
                            .SelectMany(a => ExtractTypes(a))
                            .Apply(t => TypeNameCache.Add(t.FullName, t));
                        break;
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Reset:
                        TypeNameCache.Clear();
                        AssemblySource.Instance
                            .SelectMany(a => ExtractTypes(a))
                            .Apply(t => TypeNameCache.Add(t.FullName, t));
                        break;
                }
            };

            AssemblySource.Instance.Refresh();

            AssemblySource.FindTypeByNames = names => {
                if (names == null) {
                    return null;
                }

                var type = names.Select(n => TypeNameCache.GetValueOrDefault(n)).FirstOrDefault(t => t != null);
                return type;
            };
        }
    }
}
