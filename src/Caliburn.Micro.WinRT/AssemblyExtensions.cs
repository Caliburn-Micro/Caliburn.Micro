using System;
using System.Collections.Generic;
using System.Reflection;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods for <see cref="System.Reflection.Assembly"/>
    /// </summary>
    public static class AssemblyExensions
    {
        /// <summary>
        /// Gets a collection of the public types defined in this assembly that are visible outside the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A collection of the public types defined in this assembly that are visible outside the assembly.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<Type> GetExportedTypes(this Assembly assembly) {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.ExportedTypes;
        }
    }
}
