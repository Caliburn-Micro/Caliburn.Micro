using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Caliburn.Micro
{
    /// <summary>
    /// A collection of extension methods to help with differing reflection between the portable library and SL5
    /// </summary>
    internal static class NonPortableReflectionExtensions
    {
        public static bool IsAbstract(this Type t)
        {
            return t.IsAbstract;
        }

        public static bool IsInterface(this Type t)
        {
            return t.IsInterface;
        }

        public static bool IsGenericType(this Type t)
        {
            return t.IsGenericType;
        }
    }
}
