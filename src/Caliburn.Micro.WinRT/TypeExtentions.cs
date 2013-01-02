using System;
using System.Reflection;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods for <see cref="System.Type"/>
    /// </summary>
    public static class TypeExtentions
    {
        /// <summary>
        /// Returns a value that indicates whether the specified type can be assigned to the current type.
        /// </summary>
        /// <param name="target">The target type</param>
        /// <param name="type">The type to check.</param>
        /// <returns>true if the specified type can be assigned to this type; otherwise, false.</returns>
        public static bool IsAssignableFrom(this Type target, Type type) {
            return target.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
    }
}
