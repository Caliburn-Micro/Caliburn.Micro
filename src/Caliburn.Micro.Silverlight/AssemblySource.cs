namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A source of assemblies that are inspectable by the framework.
    /// </summary>
    public static class AssemblySource {
        /// <summary>
        /// The singleton instance of the AssemblySource used by the framework.
        /// </summary>
        public static readonly IObservableCollection<Assembly> Instance = new BindableCollection<Assembly>();

        /// <summary>
        ///   Finds a type which matches a sequence or one of the elements in the sequence of names.
        /// </summary>
        /// <remarks>
        ///   By default, the first name which matches a <see cref="Type.FullName"/> from the <see cref="Assembly.GetExportedTypes"/> 
        ///   of assemblies in <see cref="AssemblySource.Instance"/> is selected.
        /// </remarks>
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
}
