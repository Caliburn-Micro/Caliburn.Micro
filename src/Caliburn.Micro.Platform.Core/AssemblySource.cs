using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro {
    /// <summary>
    /// A source of assemblies that are inspectable by the framework.
    /// </summary>
    public static class AssemblySource {
        /// <summary>
        /// The singleton instance of the AssemblySource used by the framework.
        /// </summary>
        public static readonly IObservableCollection<Assembly> Instance
            = new BindableCollection<Assembly>();

        /// <summary>
        /// Gets or sets func to finds a type which matches one of the elements in the sequence of names.
        /// </summary>
        public static Func<IEnumerable<string>, Type> FindTypeByNames { get; set; }
            = names => {
                if (names == null) {
                    return null;
                }

                Type type = names
                    .Join(Instance.SelectMany(a => a.ExportedTypes), n => n, t => t.FullName, (n, t) => t)
                    .FirstOrDefault();
                return type;
            };

        /// <summary>
        /// Adds a collection of assemblies to AssemblySource.
        /// </summary>
        /// <param name="assemblies">The assemblies to add.</param>
        public static void AddRange(IEnumerable<Assembly> assemblies) {
            foreach (Assembly assembly in assemblies) {
                try {
                    if (!Instance.Contains(assembly)) {
                        Instance.Add(assembly);
                    }
                } catch (ArgumentException) {
                    // ignore
                }
            }
        }
    }
}
