using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caliburn.Micro {
    /// <summary>
    ///  Class for managing the list of rules for doing name transformation.
    /// </summary>
    public class NameTransformer : BindableCollection<NameTransformer.Rule> {
        private const RegexOptions Options = RegexOptions.None;

        /// <summary>
        /// Gets or sets a value indicating whether if transformations from all matched rules are returned. Otherwise, transformations from only the first matched rule are returned.
        /// </summary>
        public bool UseEagerRuleSelection { get; set; } = true;

        /// <summary>
        ///  Adds a transform using a single replacement value and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text.</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering.</param>
        public void AddRule(string replacePattern, string replaceValue, string globalFilterPattern = null)
            => AddRule(replacePattern, new[] { replaceValue }, globalFilterPattern);

        /// <summary>
        ///  Adds a transform using a list of replacement values and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text.</param>
        /// <param name = "replaceValueList">The list of replacement values.</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering.</param>
        public void AddRule(string replacePattern, IEnumerable<string> replaceValueList, string globalFilterPattern = null) => Add(new Rule {
            ReplacePattern = replacePattern,
            ReplacementValues = replaceValueList,
            GlobalFilterPattern = globalFilterPattern,
        });

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list.</param>
        /// <returns>The transformed names.</returns>
        public IEnumerable<string> Transform(string source) => Transform(source, r => r);

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list.</param>
        /// <param name = "getReplaceString">A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform.</param>
        /// <returns>The transformed names.</returns>
        public IEnumerable<string> Transform(string source, Func<string, string> getReplaceString) {
            var nameList = new List<string>();
            IEnumerable<Rule> rules = this.Reverse();

            foreach (Rule rule in rules) {
                if (!string.IsNullOrEmpty(rule.GlobalFilterPattern) && !rule.GlobalFilterPatternRegex.IsMatch(source)) {
                    continue;
                }

                if (!rule.ReplacePatternRegex.IsMatch(source)) {
                    continue;
                }

                nameList.AddRange(
                    rule.ReplacementValues
                        .Select(getReplaceString)
                        .Select(repString => rule.ReplacePatternRegex.Replace(source, repString)));

                if (!UseEagerRuleSelection) {
                    break;
                }
            }

            return nameList;
        }

        /// <summary>
        /// A rule that describes a name transform.
        /// </summary>
        public class Rule {
            private Regex replacePatternRegex;
            private Regex globalFilterPatternRegex;

            /// <summary>
            /// Gets or sets regular expression pattern for global filtering.
            /// </summary>
            public string GlobalFilterPattern { get; set; }

            /// <summary>
            /// Gets or sets regular expression pattern for replacing text.
            /// </summary>
            public string ReplacePattern { get; set; }

            /// <summary>
            /// Gets or sets the list of replacement values.
            /// </summary>
            public IEnumerable<string> ReplacementValues { get; set; }

            /// <summary>
            /// Gets regular expression for global filtering.
            /// </summary>
            public Regex GlobalFilterPatternRegex
                => globalFilterPatternRegex ?? (globalFilterPatternRegex = new Regex(GlobalFilterPattern, Options));

            /// <summary>
            /// Gets regular expression for replacing text.
            /// </summary>
            public Regex ReplacePatternRegex
                => replacePatternRegex ?? (replacePatternRegex = new Regex(ReplacePattern, Options));
        }
    }
}
