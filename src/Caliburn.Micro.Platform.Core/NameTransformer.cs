﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caliburn.Micro
{
    /// <summary>
    ///  Class for managing the list of rules for doing name transformation.
    /// </summary>
    public class NameTransformer : BindableCollection<NameTransformer.Rule>
    {
        static readonly ILog Log = LogManager.GetLog(typeof(NameTransformer));
        private const RegexOptions options = RegexOptions.None;
        private bool useEagerRuleSelection = true;

        /// <summary>
        /// Flag to indicate if transformations from all matched rules are returned. Otherwise, transformations from only the first matched rule are returned.
        /// </summary>
        public bool UseEagerRuleSelection
        {
            get { return useEagerRuleSelection; }
            set { useEagerRuleSelection = value; }
        }

        /// <summary>
        ///  Adds a transform using a single replacement value and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddRule(string replacePattern, string replaceValue, string globalFilterPattern = null)
        {
            AddRule(replacePattern, new[] { replaceValue }, globalFilterPattern);
        }

        /// <summary>
        ///  Adds a transform using a list of replacement values and a global filter pattern.
        /// </summary>
        /// <param name = "replacePattern">Regular expression pattern for replacing text</param>
        /// <param name = "replaceValueList">The list of replacement values</param>
        /// <param name = "globalFilterPattern">Regular expression pattern for global filtering</param>
        public void AddRule(string replacePattern, IEnumerable<string> replaceValueList, string globalFilterPattern = null)
        {
            Log.Info("Adding rule");
            Add(new Rule
            {
                ReplacePattern = replacePattern,
                ReplacementValues = replaceValueList,
                GlobalFilterPattern = globalFilterPattern
            });
            Log.Info("Rule added");
        }

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list</param>
        /// <returns>The transformed names.</returns>
        public IEnumerable<string> Transform(string source)
        {
            return Transform(source, r => r);
        }

        /// <summary>
        /// Gets the list of transformations for a given name.
        /// </summary>
        /// <param name = "source">The name to transform into the resolved name list</param>
        /// <param name = "getReplaceString">A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform</param>
        /// <returns>The transformed names.</returns>
        public IEnumerable<string> Transform(string source, Func<string, string> getReplaceString)
        {
            var nameList = new List<string>();
            var rules = this.Reverse();

            foreach (var rule in rules)
            {
                if (!string.IsNullOrEmpty(rule.GlobalFilterPattern) && !rule.GlobalFilterPatternRegex.IsMatch(source))
                {
                    continue;
                }

                if (!rule.ReplacePatternRegex.IsMatch(source))
                {
                    continue;
                }

                nameList.AddRange(
                    rule.ReplacementValues
                        .Select(getReplaceString)
                        .Select(repString => rule.ReplacePatternRegex.Replace(source, repString))
                    );

                if (!useEagerRuleSelection)
                {
                    break;
                }
            }

            return nameList;
        }

        ///<summary>
        /// A rule that describes a name transform.
        ///</summary>
        public class Rule
        {
            private Regex replacePatternRegex;
            private Regex globalFilterPatternRegex;

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

            /// <summary>
            /// Regular expression for global filtering
            /// </summary>
            public Regex GlobalFilterPatternRegex
            {
                get
                {
                    return globalFilterPatternRegex ?? (globalFilterPatternRegex = new Regex(GlobalFilterPattern, options));
                }
            }

            /// <summary>
            /// Regular expression for replacing text
            /// </summary>
            public Regex ReplacePatternRegex
            {
                get
                {
                    return replacePatternRegex ?? (replacePatternRegex = new Regex(ReplacePattern, options));
                }
            }
        }
    }
}
