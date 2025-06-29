﻿using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Caliburn.Micro
{
    /// <summary>
    /// Generic extension methods used by the framework.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The assembly's name.</returns>
        public static string GetAssemblyName(this Assembly assembly)
        {
            return assembly.FullName.Remove(assembly.FullName.IndexOf(','));
        }

        /// <summary>
        /// Gets the value for a key. If the key does not exist, returns default(TValue).
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to call this method on.</param>
        /// <param name="key">The key to look up.</param>
        /// <returns>The key value. default(TValue) if this key is not in the dictionary.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue result) ? result : default;
        }

        /// <summary>
        /// Determines whether the specified <see cref="StringBuilder"/> is null or has a length of zero.
        /// </summary>
        /// <param name="source">The <see cref="StringBuilder"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="StringBuilder"/> is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this StringBuilder source)
        {
            return source == null || source.Length == 0;
        }
    }
}

