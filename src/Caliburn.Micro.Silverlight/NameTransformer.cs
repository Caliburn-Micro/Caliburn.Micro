namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///  Class for managing the list of rules for doing name transformation.
    /// </summary>
    public class NameTransformer : BindableCollection<NameTransformer.Rule> {
        bool useEagerRuleSelection = true;
        
        /// <summary>
        /// Flag to indicate if transformations from all matched rules are returned. Otherwise, transformations from only the first matched rule are returned.
        /// </summary>
        public bool UseEagerRuleSelection {
            get { return useEagerRuleSelection; }
            set { useEagerRuleSelection = value; }
        }

        /// <summary>
        ///  Adds a transform using a single replacement value and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddRule(string replacePattern, string replaceValue, string globalFilterPattern = null) {
            AddRule(replacePattern, new[] { replaceValue }, globalFilterPattern);
        }

        /// <summary>
        ///  Adds a transform using a list of replacement values and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text</param>
        /// <param name = "replaceValueList">The list of replacement values</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddRule(string replacePattern, IEnumerable<string> replaceValueList, string globalFilterPattern = null) {
            Add(new Rule {
                ReplacePattern = replacePattern,
                ReplacementValues = replaceValueList,
                GlobalFilterPattern = globalFilterPattern
            });
        }

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list</param>
        /// <returns>The transformed names.</returns>
        public IEnumerable<string> Transform(string source) {
            return Transform(source, r => r);
        }

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list</param>
        /// <param name = "getReplaceString">A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform</param>
        /// <returns>The transformed names.</returns>
        public IEnumerable<string> Transform(string source, Func<string, string> getReplaceString) {
            var nameList = new List<string>();
            var rules = this.Reverse();

            foreach(var rule in rules) {
                if(!string.IsNullOrEmpty(rule.GlobalFilterPattern) && !Regex.IsMatch(source, rule.GlobalFilterPattern)) {
                    continue;
                }

                if(!Regex.IsMatch(source, rule.ReplacePattern)) {
                    continue;
                }

                nameList.AddRange(
                    rule.ReplacementValues
                        .Select(getReplaceString)
                        .Select(repString => Regex.Replace(source, rule.ReplacePattern, repString))
                    );

                if (!useEagerRuleSelection) {
                    break;
                }
            }

            return nameList;
        }

        ///<summary>
        /// A rule that describes a name transform.
        ///</summary>
        public class Rule {
            /// <summary>
            /// Regular expression pattern for global filtering
            /// </summary>
            public string GlobalFilterPattern;

            /// <summary>
            /// Regular expression pattern for replacing text
            /// </summary>
            public string ReplacePattern;

            /// <summary>
            /// The list of replacement values
            /// </summary>
            public IEnumerable<string> ReplacementValues;
        }
    }
}