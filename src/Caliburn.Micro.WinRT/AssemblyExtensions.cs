using System;
using System.Collections.Generic;
using System.Reflection;

namespace Caliburn.Micro
{
    public static class AssemblyExensions
    {
        public static IEnumerable<Type> GetExportedTypes(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.ExportedTypes;
        }
    }
}
