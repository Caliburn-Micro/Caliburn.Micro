namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Collections.Generic;

    /// <summary>
    ///   A strategy for determining which view model to use for a given view.
    /// </summary>
    public static class ViewModelLocator {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewModelLocator));
        ///<summary>
        /// Used to transform names.
        ///</summary>
        public static readonly NameTransformer NameTransformer = new NameTransformer();

        static ViewModelLocator() {
            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)

            NameTransformer.AddRule(
                @"(?<fullname>^.*$)",
                @"${fullname}Model"
                );

            //Check for <Namespace>.<BaseName>View construct
            AddTypeMapping
                (
                    @"(?<namespace>([A-Za-z_]\w*\.)*)",
                    @"([A-Za-z_]\w*\.)*",
                    @"${namespace}"
                );

            //Check for <Namespace>.<BaseName>Page construct
            AddTypeMapping
                (
                    @"(?<namespace>([A-Za-z_]\w*\.)*)",
                    @"([A-Za-z_]\w*\.)*",
                    @"${namespace}",
                    "Page"
                );

            //Check for <Namespace>.Views.<Namespace>.<BaseName>View construct
            AddDefaultTypeMapping();

            //Check for <Namespace>.Views.<Namespace>.<BaseName>Page construct
            AddDefaultTypeMapping("Page");
        }

        /// <summary>
        /// Adds a default type mapping using the standard namespace mapping convention
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddDefaultTypeMapping(string viewSuffix = "View")
        {
            AddTypeMapping
                (
                    @"(?<nsbefore>([A-Za-z_]\w*\.)*)(?<nsview>Views\.)(?<nsafter>([A-Za-z_]\w*\.)*)",
                    @"([A-Za-z_]\w*\.)*Views\.([A-Za-z_]\w*\.)*",
                    @"${nsbefore}ViewModels.${nsafter}",
                    viewSuffix
                );
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">Namespace of source type as RegEx replace pattern</param>
        /// <param name="nsSourceFilterRegEx">Namespace of source type as RegEx filter pattern</param>
        /// <param name="nsTargetsRegEx">Namespaces of target type as an array of RegEx replace values</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string[] nsTargetsRegEx, string viewSuffix = "View")
        {
            var replist = new List<string>();

            Action<string> func;
            if (viewSuffix == "View")
            {
                func = t =>
                {
                    replist.Add(t + @"${basename}ViewModel");
                    replist.Add(t + @"${basename}");
                    replist.Add(t + @"I${basename}ViewModel");
                    replist.Add(t + @"I${basename}");
                };
            }
            else
            {
                func = t =>
                {
                    replist.Add(t + @"${basename}${suffix}ViewModel");
                    replist.Add(t + @"I${basename}${suffix}ViewModel");
                };
            }

            nsTargetsRegEx.ToList().ForEach(t => func(t));

            var suffixregex = viewSuffix + "$";
            NameTransformer.AddRule
                (
                    nsSourceReplaceRegEx + @"(?<basename>[A-Za-z_]\w*)(?<suffix>" + suffixregex + ")",
                    replist.ToArray(),
                    nsSourceFilterRegEx + @"[A-Za-z_]\w*" + suffixregex
                );
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">Namespace of source type as RegEx replace pattern</param>
        /// <param name="nsSourceFilterRegEx">Namespace of source type as RegEx filter pattern</param>
        /// <param name="nsTargetRegEx">Namespace of target type as RegEx replace value</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string nsTargetRegEx, string viewSuffix = "View")
        {
            AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new string[] { nsTargetRegEx }, viewSuffix);
        }


        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping
        /// </summary>
        /// <param name="nsSource">Namespace of source type</param>
        /// <param name="nsTargets">Namespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = "View")
        {
            var nsencoded = nsSource;

            nsencoded += "."; //need to terminate with "." in order to concatenate with type name later

            //Need to escape the "." as it's a special character in regular expression syntax
            nsencoded = nsencoded.Replace(".", @"\.");

            //Replace "*" wildcard with regular expression syntax
            nsencoded = nsencoded.Replace(@"*\.", @"([A-Za-z_]\w*\.)*");

            //Start pattern search from beginning of string ("^")
            //unless original string was blank (i.e. special case to indicate "append target to source")
            if (!String.IsNullOrEmpty(nsSource))
            {
                nsencoded = "^" + nsencoded;
            }

            //Capture namespace as "origns" in case we need to use it in the output in the future
            var nsreplace = @"(?<origns>" + nsencoded + @")";
            var nsfilter = @"(" + nsencoded + @")";

            var nsTargetsRegEx = nsTargets.Select(t => t + ".").ToArray();
            AddTypeMapping(nsreplace, nsfilter, nsTargetsRegEx, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping
        /// </summary>
        /// <param name="nsSource">Namespace of source type</param>
        /// <param name="nsTarget">Namespace of target type</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
        {
            AddNamespaceMapping(nsSource, new string[] { nsTarget }, viewSuffix);
        }

        /// <summary>
        ///   Makes a type name into an interface name.
        /// </summary>
        /// <param name = "typeName">The part.</param>
        /// <returns></returns>
        public static string MakeInterface(string typeName) {
            var suffix = string.Empty;
            if(typeName.Contains("[[")) {
                //generic type
                var genericParStart = typeName.IndexOf("[[");
                suffix = typeName.Substring(genericParStart);
                typeName = typeName.Remove(genericParStart);
            }

            var index = typeName.LastIndexOf(".");
            return typeName.Insert(index + 1, "I") + suffix;
        }

        /// <summary>
        /// Transforms a View type name into all of its possible ViewModel type names. Optionally accepts a flag
        /// to include interface types.
        /// </summary>
        /// <param name="typeName">The name of the ViewModel type being resolved to its companion View.</param>
        /// <param name="includeInterfaces">Include interface types (Optional)</param>
        /// <returns></returns>
        public static IEnumerable<string> TransformName(string typeName, bool includeInterfaces = false)
        {
            Func<string, string> getReplaceString;
            if (includeInterfaces)
            {
                getReplaceString = r => { return r; };
            }
            else
            {
                getReplaceString = r =>
                {
                    return Regex.IsMatch(r, @"I\${basename}") ? String.Empty : r;
                };
            }
            return NameTransformer.Transform(typeName, getReplaceString).Where(n => n != String.Empty);
        }

        /// <summary>
        ///   Determines the view model type based on the specified view type.
        /// </summary>
        /// <returns>The view model type.</returns>
        /// <remarks>
        ///   Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.
        /// </remarks>
        public static Func<Type, bool, Type> LocateTypeForViewType = (viewType, searchForInterface) => {
            var typeName = viewType.FullName;

            var viewModelTypeList = TransformName(typeName, searchForInterface);
            var viewModelType = (from assembly in AssemblySource.Instance
                                 from type in assembly.GetExportedTypes()
                                 where viewModelTypeList.Contains(type.FullName)
                                 select type).FirstOrDefault();

            if (viewModelType == null) {
                Log.Warn("View Model not found. Searched: {0}.", string.Join(", ", viewModelTypeList.ToArray()));
            }

            return viewModelType;
        };

        /// <summary>
        ///   Locates the view model for the specified view type.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>
        ///   Pass the view type as a parameter and receive a view model instance.
        /// </remarks>
        public static Func<Type, object> LocateForViewType = viewType => {
            var viewModelType = LocateTypeForViewType(viewType, false);

            if(viewModelType != null) {
                var viewModel = IoC.GetInstance(viewModelType, null);
                if(viewModel != null) {
                    return viewModel;
                }
            }

            viewModelType = LocateTypeForViewType(viewType, true);

            return viewModelType != null
                       ? IoC.GetInstance(viewModelType, null)
                       : null;
        };

        /// <summary>
        ///   Locates the view model for the specified view instance.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>
        ///   Pass the view instance as a parameters and receive a view model instance.
        /// </remarks>
        public static Func<object, object> LocateForView = view => {
            if(view == null)
                return null;

            var frameworkElement = view as FrameworkElement;
            if(frameworkElement != null && frameworkElement.DataContext != null)
                return frameworkElement.DataContext;

            return LocateForViewType(view.GetType());
        };
    }
}