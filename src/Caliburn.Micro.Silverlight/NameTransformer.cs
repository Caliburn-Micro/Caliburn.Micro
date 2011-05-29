namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///   Class for managing the list of rules for doing name transformation.
    /// </summary>
    public class NameTransformer {
        readonly List<TransformRule> rules = new List<TransformRule>();

        /// <summary>
        ///   Adds a transform using a single replacement value and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddRule(string replacePattern, string replaceValue, string globalFilterPattern = null) {
            AddRule(replacePattern, new[] { replaceValue }, globalFilterPattern);
        }

        /// <summary>
        ///   Adds a transform using a list of replacement values and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text</param>
        /// <param name = "replaceValueList">The list of replacement values</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddRule(string replacePattern, IEnumerable<string> replaceValueList, string globalFilterPattern = null) {
            var conv = new TransformRule {
                ReplacePattern = replacePattern,
                ReplaceValueList = replaceValueList,
                GlobalFilterPattern = globalFilterPattern
            };

            rules.Add(conv);
        }

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list</param>
        /// <returns></returns>
        public List<string> Transform(string source) {
            return Transform(source, r => r);
        }

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list</param>
        /// <param name = "getReplaceString">A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform</param>
        /// <returns></returns>
        public List<string> Transform(string source, Func<string, string> getReplaceString) {
            var nameList = new List<string>();
            var reversedList = rules.Reverse<TransformRule>();

            foreach(var conv in reversedList) {
                if(!string.IsNullOrEmpty(conv.GlobalFilterPattern) && !Regex.IsMatch(source, conv.GlobalFilterPattern)) {
                    continue;
                }

                if(!Regex.IsMatch(source, conv.ReplacePattern)) {
                    continue;
                }

                nameList.AddRange(
                    conv.ReplaceValueList
                        .Select(getReplaceString)
                        .Select(repString => Regex.Replace(source, conv.ReplacePattern, repString))
                    );

                break;
            }

            return nameList;
        }

        class TransformRule {
            public string GlobalFilterPattern;
            public string ReplacePattern;
            public IEnumerable<string> ReplaceValueList;
        }
    }
}