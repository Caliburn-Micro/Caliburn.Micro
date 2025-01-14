namespace Caliburn.Micro
{
    /// <summary>
    ///  Helper class for encoding strings to regular expression patterns
    /// </summary>
    public static class RegExHelper
    {
        /// <summary>
        /// Regular expression pattern for valid name
        /// </summary>
        /// <remarks>
        /// Match a single character present in the list <c>[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}_]</c>
        /// <list type="bullet">
        ///   <listheader>
        ///     <term>Regex</term>
        ///     <description>Description</description>
        ///   </listheader>
        ///   <item>
        ///     <term>\p{Lu}</term>
        ///     <description>matches an <c>uppercase letter</c> that has a <c>lowercase</c> variant</description>
        ///   </item>
        ///   <item>
        ///     <term>\p{Ll}</term>
        ///     <description>matches a <c>lowercase letter</c> that has an <c>uppercase</c> variant</description>
        ///   </item>
        ///   <item>
        ///     <term>\p{Lt}</term>
        ///     <description>matches a <c>letter</c> that appears at the <c>start</c> of a <c>word</c> when only the <c>first letter</c> of the <c>word</c> is <c>capitalized</c></description>
        ///   </item>
        ///   <item>
        ///     <term>\p{Lm}</term>
        ///     <description>matches a <c>special character</c> that is used like a <c>letter</c></description>
        ///   </item>
        ///   <item>
        ///     <term>\p{Lo}</term>
        ///     <description>matches a <c>letter</c> or <c>ideograph</c> that does not have <c>lowercase</c> and <c>uppercase</c> variants</description>
        ///   </item>
        ///   <item>
        ///     <term>\p{Nl}</term>
        ///     <description>matches a <c>number</c> that looks like a <c>letter</c>, such as a <c>Roman numeral</c></description>
        ///   </item>
        ///   <item>
        ///     <term>_</term>
        ///     <description>matches the character _ with index 9510 (5F16 or 1378) literally (case sensitive)</description>
        ///   </item>
        /// </list>
        /// 
        /// Match a single character present in the list <c>[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}_]</c><br/>
        ///     <c>*</c> matches the previous token between zero and unlimited times, as many times as possible, giving back as needed (greedy)<br/>
        ///     <c>\p{Lu}</c> matches an uppercase letter that has a lowercase variant<br/>
        ///     <c>\p{Ll}</c> matches a lowercase letter that has an uppercase variant<br/>
        ///     <c>\p{Lt}</c> matches a letter that appears at the start of a word when only the first letter of the word is capitalized<br/>
        ///     <c>\p{Lm}</c> matches a special character that is used like a letter<br/>
        ///     <c>\p{Lo}</c> matches a letter or ideograph that does not have lowercase and uppercase variants<br/>
        ///     <c>\p{Nl}</c> matches a number that looks like a letter, such as a Roman numeral<br/>
        ///     <c>\p{Mn}</c> matches a character intended to be combined with another character without taking up extra space (e.g. accents, umlauts, etc.)<br/>
        ///     <c>\p{Mc}</c> matches a character intended to be combined with another character that takes up extra space (vowel signs in many Eastern languages)<br/>
        ///     <c>\p{Nd}</c> matches a digit zero through nine in any script except ideographic scripts<br/>
        ///     <c>\p{Pc}</c> matches a punctuation character such as an underscore that connects words<br/>
        ///     <c>\p{Cf}</c> matches invisible formatting indicator<br/>
        ///     <c>_</c> matches the character _ with index 9510 (5F16 or 1378) literally (case sensitive)<br/>
        /// </remarks>
        public const string NameRegEx = @"[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}_]" +
                                        @"[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}_]*";

        /// <summary>
        /// Regular expression pattern for SubNamespace (including dot)
        /// </summary>
        public const string SubNamespaceRegEx = NameRegEx + @"\.";

        /// <summary>
        /// Regular expression pattern for namespace or namespace fragment
        /// </summary>
        public const string NamespaceRegEx = "(" + SubNamespaceRegEx + ")*";

        /// <summary>
        /// Creates a named capture group with the specified regular expression 
        /// </summary>
        /// <param name="groupName">Name of capture group to create</param>
        /// <param name="regEx">Regular expression pattern to capture</param>
        /// <returns>Regular expression capture group with the specified group name</returns>
        public static string GetCaptureGroup(string groupName, string regEx)
        {
            return string.Concat(@"(?<", groupName, ">", regEx, ")");
        }

        /// <summary>
        /// Converts a namespace (including wildcards) to a regular expression string
        /// </summary>
        /// <param name="srcNamespace">Source namespace to convert to regular expression</param>
        /// <returns>Namespace converted to a regular expression</returns>
        public static string NamespaceToRegEx(string srcNamespace)
        {
            // Need to escape the "." as it's a special character in regular expression syntax
            var namespaceEncoded = srcNamespace.Replace(".", @"\.");

            // Replace "*" wildcard with regular expression syntax
            namespaceEncoded = namespaceEncoded.Replace(@"*\.", NamespaceRegEx);
            return namespaceEncoded;
        }

        /// <summary>
        /// Creates a capture group for a valid name regular expression pattern
        /// </summary>
        /// <param name="groupName">Name of capture group to create</param>
        /// <returns>Regular expression capture group with the specified group name</returns>
        public static string GetNameCaptureGroup(string groupName)
        {
            return GetCaptureGroup(groupName, NameRegEx);
        }

        /// <summary>
        /// Creates a capture group for a namespace regular expression pattern
        /// </summary>
        /// <param name="groupName">Name of capture group to create</param>
        /// <returns>Regular expression capture group with the specified group name</returns>
        public static string GetNamespaceCaptureGroup(string groupName)
        {
            return GetCaptureGroup(groupName, NamespaceRegEx);
        }
    }
}
