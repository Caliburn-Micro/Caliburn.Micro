using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caliburn.Micro
{
    /// <summary>
    /// Class for managing the list of conventions for doing name resolution.
    /// </summary>
    public class NameResolutionManager
    {
        private List<NameTransformConvention> _TransformConventionList = new List<NameTransformConvention>();

        /// <summary>
        /// Adds a NameTransformConvention to the list using a single replacement value and no global filter pattern.
        /// </summary>
        /// <param name="replacePattern">Regular expression pattern for replacing text</param>
        /// <param name="replaceValue">The replacement value.</param>
        public void AddTransformConvention(string replacePattern, string replaceValue)
        {
            AddTransformConvention(replacePattern, replaceValue, null);
        }

        /// <summary>
        /// Adds a NameTransformConvention to the list using a single replacement value and a global filter pattern.
        /// </summary>
        /// <param name="replacePattern">Regular expression pattern for replacing text</param>
        /// <param name="replaceValue">The replacement value.</param>
        /// <param name="globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddTransformConvention(string replacePattern, string replaceValue, string globalFilterPattern)
        {
            AddTransformConvention(replacePattern, new string[] { replaceValue }, globalFilterPattern);
        }

        /// <summary>
        /// Adds a NameTransformConvention to the list using a list of replacement values and no global filter pattern.
        /// </summary>
        /// <param name="replacePattern">Regular expression pattern for replacing text</param>
        /// <param name="replaceValueList">The list of replacement values</param>
        public void AddTransformConvention(string replacePattern, IEnumerable<string> replaceValueList)
        {
            AddTransformConvention(replacePattern, replaceValueList, null);
        }

        /// <summary>
        /// Adds a NameTransformConvention to the list using a list of replacement values and a global filter pattern.
        /// </summary>
        /// <param name="replacePattern">Regular expression pattern for replacing text</param>
        /// <param name="replaceValueList">The list of replacement values</param>
        /// <param name="globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddTransformConvention(string replacePattern, IEnumerable<string> replaceValueList, string globalFilterPattern)
        {
            var conv = new NameTransformConvention
            {
                ReplacePattern = replacePattern,
                ReplaceValueList = replaceValueList,
                GlobalFilterPattern = globalFilterPattern
            };
            _TransformConventionList.Add(conv);
        }

        /// <summary>
        /// Gets the resolved name list for a given name.
        /// </summary>
        /// <param name="sourceTypeName">The name to transform into the resolved name list</param>
        /// <returns></returns>
        public List<string> GetResolvedNameList(string sourceTypeName)
        {
            return GetResolvedNameList(sourceTypeName, (r) => { return r; });
        }

        /// <summary>
        /// Gets the resolved name list for a given name.
        /// </summary>
        /// <param name="sourceTypeName">The name to transform into the resolved name list</param>
        /// <param name="getReplaceStr">A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform</param>
        /// <returns></returns>
        public List<string> GetResolvedNameList(string sourceTypeName, Func<string, string> getReplaceStr)
        {
            List<string> nameList = new List<string>();

            var reversedList = _TransformConventionList.Reverse<NameTransformConvention>();

            foreach (var conv in reversedList)
            {
                if (!String.IsNullOrEmpty(conv.GlobalFilterPattern) && !Regex.IsMatch(sourceTypeName, conv.GlobalFilterPattern))
                {
                    continue;
                }

                if (!Regex.IsMatch(sourceTypeName, conv.ReplacePattern))
                {
                    continue;
                }

                foreach (var repValue in conv.ReplaceValueList)
                {
                    var repString = getReplaceStr(repValue);
                    nameList.Add(Regex.Replace(sourceTypeName, conv.ReplacePattern, repString));
                }

                break;
            }
            return nameList;
        }
    }
}
