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

        //These fields are used for configuring the default type mappings. They can be changed using ConfigureTypeMappings().
        private static string _DefaultSubNSViews;
        private static string _DefaultSubNSViewModels;
        private static bool _UseNameSuffixesInMappings;
        private static string _NameFormat;
        private static string _ViewModelSuffix;
        private static List<string> _ViewSuffixList = new List<string>();
        private static bool _IncludeViewSuffixInVMNames;

        /// <summary>
        /// The name of the capture group used as a marker for rules that return interface types
        /// </summary>
        public static string InterfaceCaptureGroupName
        {
            get { return _InterfaceCaptureGroupName; }
        }

        private const string _InterfaceCaptureGroupName = "isinterface";

        static ViewModelLocator() {
            ConfigureTypeMappings(new TypeMappingConfiguration());
        }

        /// <summary>
        /// Specifies how type mappings are created, including default type mappings. Calling this method will
        /// clear all existing name transformation rules and create new default type mappings according to the
        /// configuration.
        /// </summary>
        /// <param name="config">An instance of TypeMappingConfiguration that provides the settings for configuration</param>
        public static void ConfigureTypeMappings(TypeMappingConfiguration config) {
            if (String.IsNullOrEmpty(config.DefaultSubNSViews)) {
                throw new ArgumentException("DefaultSubNSViews field cannot be blank.");
            }

            if (String.IsNullOrEmpty(config.DefaultSubNSViewModels)) {
                throw new ArgumentException("DefaultSubNSViewModels field cannot be blank.");
            }

            if (String.IsNullOrEmpty(config.NameFormat))
            {
                throw new ArgumentException("NameFormat field cannot be blank.");
            }

            if (config.UseNameSuffixesInMappings) {
                if (String.IsNullOrEmpty(config.ViewModelSuffix)) {
                    throw new ArgumentException("ViewModelSuffix field cannot be blank if UseNameSuffixesInMappings is true.");
                }
            }

            NameTransformer.Clear();
            _ViewSuffixList.Clear();

            _DefaultSubNSViews = config.DefaultSubNSViews;
            _DefaultSubNSViewModels = config.DefaultSubNSViewModels;
            _NameFormat = config.NameFormat;
            _UseNameSuffixesInMappings = config.UseNameSuffixesInMappings;
            _ViewModelSuffix = config.ViewModelSuffix;
            _ViewSuffixList.AddRange(config.ViewSuffixList);
            _IncludeViewSuffixInVMNames = config.IncludeViewSuffixInVMNames;

            SetAllDefaults();
        }

        private static void SetAllDefaults()
        {
            if (_UseNameSuffixesInMappings) {
                //Add support for all view suffixes
                _ViewSuffixList.ForEach(v => { AddDefaultTypeMapping(v); });
            }
            else {
                AddSubNamespaceMapping(_DefaultSubNSViews, _DefaultSubNSViewModels);
            }
        }

        /// <summary>
        /// Adds a default type mapping using the standard namespace mapping convention
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddDefaultTypeMapping(string viewSuffix = "View") {
            if (!_UseNameSuffixesInMappings) {
                return;
            }

            //Check for <Namespace>.<BaseName><ViewSuffix> construct
            AddNamespaceMapping(String.Empty, String.Empty, viewSuffix);

            //Check for <Namespace>.Views.<NameSpace>.<BaseName><ViewSuffix> construct
            AddSubNamespaceMapping(_DefaultSubNSViews, _DefaultSubNSViewModels, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace</param>
        /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace</param>
        /// <param name="nsTargetsRegEx">Array of RegEx replace values for target namespaces</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string[] nsTargetsRegEx, string viewSuffix = "View") {
            var replist = new List<string>();
            Action<string> func;

            var basegrp = "${basename}";
            var interfacegrp = "${" + _InterfaceCaptureGroupName + "}";

            if (_UseNameSuffixesInMappings) {
                if (_ViewModelSuffix.Contains(viewSuffix) || !_IncludeViewSuffixInVMNames) {

                    var nameregex = String.Format(_NameFormat, basegrp, _ViewModelSuffix);
                    
                    func = t => {
                        replist.Add(t + "I" + nameregex + interfacegrp);
                        replist.Add(t + "I" + basegrp + interfacegrp);
                        replist.Add(t + nameregex);
                        replist.Add(t + basegrp);
                    };
                }
                else {
                    var nameregex = String.Format(_NameFormat, basegrp, "${suffix}" + _ViewModelSuffix);
                    func = t => {
                        replist.Add(t + "I" + nameregex + interfacegrp);
                        replist.Add(t + nameregex);
                    };
                }
            }
            else {
                func = t => {
                    replist.Add(t + "I" + basegrp + interfacegrp);
                    replist.Add(t + basegrp);
                };
            }

            nsTargetsRegEx.ToList().ForEach(t => func(t));


            string suffix = _UseNameSuffixesInMappings ? viewSuffix : String.Empty;

            var srcfilterregx = String.IsNullOrEmpty(nsSourceFilterRegEx) 
                ? null
                : String.Concat(nsSourceFilterRegEx, String.Format(_NameFormat, RegExHelper.NameRegEx, suffix), "$");
            string rxbase = RegExHelper.GetNameCaptureGroup("basename");
            string rxsuffix = RegExHelper.GetCaptureGroup("suffix", suffix);
            
            //Add a dummy capture group -- place after the "$" so it can never capture anything
            string rxinterface = RegExHelper.GetCaptureGroup(_InterfaceCaptureGroupName, String.Empty);
            NameTransformer.AddRule(
                String.Concat(nsSourceReplaceRegEx, String.Format(_NameFormat, rxbase, rxsuffix), "$", rxinterface),
                replist.ToArray(),
                srcfilterregx
            );
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace</param>
        /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace</param>
        /// <param name="nsTargetRegEx">RegEx replace value for target namespace</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string nsTargetRegEx, string viewSuffix = "View") {
            AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new string[] { nsTargetRegEx }, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping
        /// </summary>
        /// <param name="nsSource">Namespace of source type</param>
        /// <param name="nsTargets">Namespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = "View") {
            //need to terminate with "." in order to concatenate with type name later
            var nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            //Start pattern search from beginning of string ("^")
            //unless original string was blank (i.e. special case to indicate "append target to source")
            if (!String.IsNullOrEmpty(nsSource)) {
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
        public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View") {
            AddNamespaceMapping(nsSource, new string[] { nsTarget }, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping by substituting one subnamespace for another
        /// </summary>
        /// <param name="nsSource">Subnamespace of source type</param>
        /// <param name="nsTargets">Subnamespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddSubNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = "View") {
            //need to terminate with "." in order to concatenate with type name later
            var nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            string rxbeforesrc, rxbeforetgt, rxaftersrc, rxaftertgt;
            rxbeforesrc = rxbeforetgt = rxaftersrc = rxaftertgt = String.Empty;

            if (!String.IsNullOrEmpty(nsSource)) {
                if (!nsSource.StartsWith("*")) {
                    rxbeforesrc = RegExHelper.GetNSCaptureGroup("nsbefore");
                    rxbeforetgt = @"${nsbefore}";
                }

                if (!nsSource.EndsWith("*")) {
                    rxaftersrc = RegExHelper.GetNSCaptureGroup("nsafter");
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
        public static void AddSubNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View") {
            AddSubNamespaceMapping(nsSource, new string[] { nsTarget }, viewSuffix);
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
        /// Transforms a View type name into all of its possible ViewModel type names. Accepts a flag
        /// to include or exclude interface types.
        /// </summary>
        /// <returns>Enumeration of transformed names</returns>
        /// <remarks>Arguments:
        /// typeName = The name of the View type being resolved to its companion ViewModel.
        /// includeInterfaces = Flag to indicate if interface types are included
        /// </remarks>
        public static Func<string, bool, IEnumerable<string>> TransformName = (typeName, includeInterfaces) => {
            Func<string, string> getReplaceString;
            if (includeInterfaces) {
                getReplaceString = r => { return r; };
            }
            else {
                var interfacegrpregex = @"\${" + _InterfaceCaptureGroupName + @"}$";
                getReplaceString = r => {
                    return Regex.IsMatch(r, interfacegrpregex) ? String.Empty : r;
                };
            }
            return NameTransformer.Transform(typeName, getReplaceString).Where(n => n != String.Empty);
        };

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
            var viewModelType = viewModelTypeList.Join(AssemblySource.Instance.SelectMany(a => a.GetExportedTypes()), n => n, t => t.FullName, (n, t) => t).FirstOrDefault();

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