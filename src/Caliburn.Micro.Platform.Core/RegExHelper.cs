﻿using System;

namespace Caliburn.Micro {
    /// <summary>
    ///  Helper class for encoding strings to regular expression patterns.
    /// </summary>
    public static class RegExHelper {
        /// <summary>
        /// Regular expression pattern for valid name.
        /// </summary>
        public const string NameRegEx = @"[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}_][\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}_]*";

        /// <summary>
        /// Regular expression pattern for subnamespace (including dot).
        /// </summary>
        public const string SubNamespaceRegEx = NameRegEx + @"\.";

        /// <summary>
        /// Regular expression pattern for namespace or namespace fragment.
        /// </summary>
        public const string NamespaceRegEx = "(" + SubNamespaceRegEx + ")*";

        /// <summary>
        /// Creates a named capture group with the specified regular expression.
        /// </summary>
        /// <param name="groupName">Name of capture group to create.</param>
        /// <param name="regEx">Regular expression pattern to capture.</param>
        /// <returns>Regular expression capture group with the specified group name.</returns>
        public static string GetCaptureGroup(string groupName, string regEx)
            => string.Concat(@"(?<", groupName, ">", regEx, ")");

        /// <summary>
        /// Converts a namespace (including wildcards) to a regular expression string.
        /// </summary>
        /// <param name="srcNamespace">Source namespace to convert to regular expression.</param>
        /// <returns>Namespace converted to a regular expression.</returns>
        public static string NamespaceToRegEx(string srcNamespace) {
            // Need to escape the "." as it's a special character in regular expression syntax
            string nsencoded = srcNamespace.Replace(".", @"\.");

            // Replace "*" wildcard with regular expression syntax
            nsencoded = nsencoded.Replace(@"*\.", NamespaceRegEx);

            return nsencoded;
        }

        /// <summary>
        /// Creates a capture group for a valid name regular expression pattern.
        /// </summary>
        /// <param name="groupName">Name of capture group to create.</param>
        /// <returns>Regular expression capture group with the specified group name.</returns>
        public static string GetNameCaptureGroup(string groupName)
            => GetCaptureGroup(groupName, NameRegEx);

        /// <summary>
        /// Creates a capture group for a namespace regular expression pattern.
        /// </summary>
        /// <param name="groupName">Name of capture group to create.</param>
        /// <returns>Regular expression capture group with the specified group name.</returns>
        public static string GetNamespaceCaptureGroup(string groupName)
            => GetCaptureGroup(groupName, NamespaceRegEx);
    }
}
