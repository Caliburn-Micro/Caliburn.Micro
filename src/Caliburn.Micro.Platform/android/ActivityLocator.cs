using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caliburn.Micro
{
    public static class ActivityLocator
    {
        const string DefaultViewSuffix = "Activity";

        static readonly ILog Log = LogManager.GetLog(typeof(ActivityLocator));

        //These fields are used for configuring the default type mappings. They can be changed using ConfigureTypeMappings().
        static string defaultSubNsViews;
        static string defaultSubNsViewModels;
        static bool useNameSuffixesInMappings;
        static string nameFormat;
        static string viewModelSuffix;
        static readonly List<string> ViewSuffixList = new List<string>();
        static bool includeViewSuffixInVmNames;

        ///<summary>
        /// Used to transform names.
        ///</summary>
        public static NameTransformer NameTransformer = new NameTransformer();

        /// <summary>
        ///   Separator used when resolving View names for context instances.
        /// </summary>
        public static string ContextSeparator = ".";

        static ActivityLocator() {

            var configuration = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "Activities", 
                IncludeViewSuffixInViewModelNames = false
            };

            configuration.ViewSuffixList.Add("Activity");

            ConfigureTypeMappings(configuration);
        }

        /// <summary>
        /// Specifies how type mappings are created, including default type mappings. Calling this method will
        /// clear all existing name transformation rules and create new default type mappings according to the
        /// configuration.
        /// </summary>
        /// <param name="config">An instance of TypeMappingConfiguration that provides the settings for configuration</param>
        public static void ConfigureTypeMappings(TypeMappingConfiguration config)
        {
            if (String.IsNullOrEmpty(config.DefaultSubNamespaceForViews))
            {
                throw new ArgumentException("DefaultSubNamespaceForViews field cannot be blank.");
            }

            if (String.IsNullOrEmpty(config.DefaultSubNamespaceForViewModels))
            {
                throw new ArgumentException("DefaultSubNamespaceForViewModels field cannot be blank.");
            }

            if (String.IsNullOrEmpty(config.NameFormat))
            {
                throw new ArgumentException("NameFormat field cannot be blank.");
            }

            NameTransformer.Clear();
            ViewSuffixList.Clear();

            defaultSubNsViews = config.DefaultSubNamespaceForViews;
            defaultSubNsViewModels = config.DefaultSubNamespaceForViewModels;
            nameFormat = config.NameFormat;
            useNameSuffixesInMappings = config.UseNameSuffixesInMappings;
            viewModelSuffix = config.ViewModelSuffix;
            ViewSuffixList.AddRange(config.ViewSuffixList);
            includeViewSuffixInVmNames = config.IncludeViewSuffixInViewModelNames;

            SetAllDefaults();
        }


        private static void SetAllDefaults()
        {
            if (useNameSuffixesInMappings)
            {
                //Add support for all view suffixes
                ViewSuffixList.Apply(AddDefaultTypeMapping);
            }
            else
            {
                AddSubNamespaceMapping(defaultSubNsViewModels, defaultSubNsViews);
            }
        }

        /// <summary>
        /// Adds a default type mapping using the standard namespace mapping convention
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddDefaultTypeMapping(string viewSuffix = DefaultViewSuffix)
        {
            if (!useNameSuffixesInMappings)
            {
                return;
            }

            //Check for <Namespace>.<BaseName><ViewSuffix> construct
            AddNamespaceMapping(String.Empty, String.Empty, viewSuffix);

            //Check for <Namespace>.ViewModels.<NameSpace>.<BaseName><ViewSuffix> construct
            AddSubNamespaceMapping(defaultSubNsViewModels, defaultSubNsViews, viewSuffix);
        }

        /// <summary>
        /// This method registers a View suffix or synonym so that View Context resolution works properly.
        /// It is automatically called internally when calling AddNamespaceMapping(), AddDefaultTypeMapping(),
        /// or AddTypeMapping(). It should not need to be called explicitly unless a rule that handles synonyms
        /// is added directly through the NameTransformer.
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View".</param>
        public static void RegisterViewSuffix(string viewSuffix)
        {
            if (ViewSuffixList.Count(s => s == viewSuffix) == 0)
            {
                ViewSuffixList.Add(viewSuffix);
            }
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace</param>
        /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace</param>
        /// <param name="nsTargetsRegEx">Array of RegEx replace values for target namespaces</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string[] nsTargetsRegEx, string viewSuffix = DefaultViewSuffix)
        {
            RegisterViewSuffix(viewSuffix);

            var replist = new List<string>();
            var repsuffix = useNameSuffixesInMappings ? viewSuffix : String.Empty;
            const string basegrp = "${basename}";

            foreach (var t in nsTargetsRegEx)
            {
                replist.Add(t + String.Format(nameFormat, basegrp, repsuffix));
            }

            var rxbase = RegExHelper.GetNameCaptureGroup("basename");
            var suffix = String.Empty;
            if (useNameSuffixesInMappings)
            {
                suffix = viewModelSuffix;
                if (!viewModelSuffix.Contains(viewSuffix) && includeViewSuffixInVmNames)
                {
                    suffix = viewSuffix + suffix;
                }
            }
            var rxsrcfilter = String.IsNullOrEmpty(nsSourceFilterRegEx)
                ? null
                : String.Concat(nsSourceFilterRegEx, String.Format(nameFormat, RegExHelper.NameRegEx, suffix), "$");
            var rxsuffix = RegExHelper.GetCaptureGroup("suffix", suffix);

            NameTransformer.AddRule(
                String.Concat(nsSourceReplaceRegEx, String.Format(nameFormat, rxbase, rxsuffix), "$"),
                replist.ToArray(),
                rxsrcfilter
            );
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace</param>
        /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace</param>
        /// <param name="nsTargetRegEx">RegEx replace value for target namespace</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string nsTargetRegEx, string viewSuffix = DefaultViewSuffix)
        {
            AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new[] { nsTargetRegEx }, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping
        /// </summary>
        /// <param name="nsSource">Namespace of source type</param>
        /// <param name="nsTargets">Namespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = DefaultViewSuffix)
        {
            //need to terminate with "." in order to concatenate with type name later
            var nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            //Start pattern search from beginning of string ("^")
            //unless original string was blank (i.e. special case to indicate "append target to source")
            if (!String.IsNullOrEmpty(nsSource))
            {
                nsencoded = "^" + nsencoded;
            }

            //Capture namespace as "origns" in case we need to use it in the output in the future
            var nsreplace = RegExHelper.GetCaptureGroup("origns", nsencoded);

            var nsTargetsRegEx = nsTargets.Select(t => t + ".").ToArray();
            AddTypeMapping(nsreplace, null, nsTargetsRegEx, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping
        /// </summary>
        /// <param name="nsSource">Namespace of source type</param>
        /// <param name="nsTarget">Namespace of target type</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = DefaultViewSuffix)
        {
            AddNamespaceMapping(nsSource, new[] { nsTarget }, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping by substituting one subnamespace for another
        /// </summary>
        /// <param name="nsSource">Subnamespace of source type</param>
        /// <param name="nsTargets">Subnamespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddSubNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = DefaultViewSuffix)
        {
            //need to terminate with "." in order to concatenate with type name later
            var nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            string rxbeforetgt, rxaftersrc, rxaftertgt;
            var rxbeforesrc = rxbeforetgt = rxaftersrc = rxaftertgt = String.Empty;

            if (!String.IsNullOrEmpty(nsSource))
            {
                if (!nsSource.StartsWith("*"))
                {
                    rxbeforesrc = RegExHelper.GetNamespaceCaptureGroup("nsbefore");
                    rxbeforetgt = @"${nsbefore}";
                }

                if (!nsSource.EndsWith("*"))
                {
                    rxaftersrc = RegExHelper.GetNamespaceCaptureGroup("nsafter");
                    rxaftertgt = "${nsafter}";
                }
            }

            var rxmid = RegExHelper.GetCaptureGroup("subns", nsencoded);
            var nsreplace = String.Concat(rxbeforesrc, rxmid, rxaftersrc);

            var nsTargetsRegEx = nsTargets.Select(t => String.Concat(rxbeforetgt, t, ".", rxaftertgt)).ToArray();
            AddTypeMapping(nsreplace, null, nsTargetsRegEx, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping by substituting one subnamespace for another
        /// </summary>
        /// <param name="nsSource">Subnamespace of source type</param>
        /// <param name="nsTarget">Subnamespace of target type</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddSubNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = DefaultViewSuffix)
        {
            AddSubNamespaceMapping(nsSource, new[] { nsTarget }, viewSuffix);
        }

        /// <summary>
        /// Transforms a ViewModel type name into all of its possible View type names. Optionally accepts an instance
        /// of context object
        /// </summary>
        /// <returns>Enumeration of transformed names</returns>
        /// <remarks>Arguments:
        /// typeName = The name of the ViewModel type being resolved to its companion View.
        /// context = An instance of the context or null.
        /// </remarks>
        public static Func<string, object, IEnumerable<string>> TransformName = (typeName, context) =>
        {
            Func<string, string> getReplaceString;
            if (context == null)
            {
                getReplaceString = r => r;
                return NameTransformer.Transform(typeName, getReplaceString);
            }

            var contextstr = ContextSeparator + context;
            string grpsuffix = String.Empty;
            if (useNameSuffixesInMappings)
            {
                //Create RegEx for matching any of the synonyms registered
                var synonymregex = "(" + String.Join("|", ViewSuffixList.ToArray()) + ")";
                grpsuffix = RegExHelper.GetCaptureGroup("suffix", synonymregex);
            }

            const string grpbase = @"\${basename}";
            var patternregex = String.Format(nameFormat, grpbase, grpsuffix) + "$";

            //Strip out any synonym by just using contents of base capture group with context string
            var replaceregex = "${basename}" + contextstr;

            //Strip out the synonym
            getReplaceString = r => Regex.Replace(r, patternregex, replaceregex);

            //Return only the names for the context
            return NameTransformer.Transform(typeName, getReplaceString).Where(n => n.EndsWith(contextstr));
        };

        /// <summary>
        ///   Locates the view type based on the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>
        ///   Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.
        /// </remarks>
        public static Func<Type, object, Type> LocateTypeForModelType = (modelType, context) =>
        {
            var viewTypeName = modelType.FullName;

            viewTypeName = viewTypeName.Substring(
                0,
                viewTypeName.IndexOf('`') < 0
                    ? viewTypeName.Length
                    : viewTypeName.IndexOf('`')
                );

            var viewTypeList = TransformName(viewTypeName, context);
            var viewType = AssemblySource.FindTypeByNames(viewTypeList);

            if (viewType == null)
            {
                Log.Warn("View not found. Searched: {0}.", string.Join(", ", viewTypeList.ToArray()));
            }

            return viewType;
        };
    }
}