using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

#if WINDOWS_UWP
using Windows.UI.Xaml;
#elif XFORMS
using UIElement = Xamarin.Forms.Element;
#elif MAUI
using UIElement = Microsoft.Maui.Controls.Element;
#endif

#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    ///   A strategy for determining which view model to use for a given view.
    /// </summary>
    public static class ViewModelLocator {
        /// <summary>
        /// Used to transform names.
        /// </summary>
        public static readonly NameTransformer NameTransformer
            = new NameTransformer();

#if ANDROID
        private const string DefaultViewSuffix = "Activity";
#elif IOS
        private const string DefaultViewSuffix = "ViewController";
#else
        private const string DefaultViewSuffix = "View";
#endif

        private static readonly ILog Log = LogManager.GetLog(typeof(ViewModelLocator));
        private static readonly List<string> ViewSuffixList = new List<string>();

        // These fields are used for configuring the default type mappings. They can be changed using ConfigureTypeMappings().
        private static string defaultSubNsViews;
        private static string defaultSubNsViewModels;
        private static bool useNameSuffixesInMappings;
        private static string nameFormat;
        private static string viewModelSuffix;
        private static bool includeViewSuffixInVmNames;

        static ViewModelLocator() {
            var configuration = new TypeMappingConfiguration();

#if ANDROID
            configuration.DefaultSubNamespaceForViews = "Activities";
            configuration.ViewSuffixList.Add("Activity");
            configuration.IncludeViewSuffixInViewModelNames = false;
#elif IOS
            configuration.DefaultSubNamespaceForViews = "ViewControllers";
            configuration.ViewSuffixList.Add("ViewController");
            configuration.IncludeViewSuffixInViewModelNames = false;
#endif

            ConfigureTypeMappings(configuration);
        }

        /// <summary>
        /// Gets or sets the name of the capture group used as a marker for rules that return interface types.
        /// </summary>
        public static string InterfaceCaptureGroupName { get; set; }
            = "isinterface";

        /// <summary>
        /// Gets or sets func to transforms a View type name into all of its possible ViewModel type names. Accepts a flag
        /// to include or exclude interface types.
        /// </summary>
        /// <returns>Enumeration of transformed names.</returns>
        /// <remarks>Arguments:
        /// typeName = The name of the View type being resolved to its companion ViewModel.
        /// includeInterfaces = Flag to indicate if interface types are included.
        /// </remarks>
        public static Func<string, bool, IEnumerable<string>> TransformName { get; set; }
            = (typeName, includeInterfaces) => {
                Func<string, string> getReplaceString;
                if (includeInterfaces) {
                    getReplaceString = r => r;
                } else {
                    string interfacegrpregex = @"\${" + InterfaceCaptureGroupName + @"}$";
                    getReplaceString = r => Regex.IsMatch(r, interfacegrpregex) ? string.Empty : r;
                }

                return NameTransformer.Transform(typeName, getReplaceString).Where(n => !string.IsNullOrEmpty(n));
            };

        /// <summary>
        ///   Gets or sets func to determines the view model type based on the specified view type.
        /// </summary>
        /// <returns>The view model type.</returns>
        /// <remarks>
        ///   Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.
        /// </remarks>
        public static Func<Type, bool, Type> LocateTypeForViewType { get; set; }
            = (viewType, searchForInterface) => {
                string typeName = viewType.FullName;

                var viewModelTypeList = TransformName(typeName, searchForInterface).ToList();

                Type viewModelType = AssemblySource.FindTypeByNames(viewModelTypeList);

                if (viewModelType == null) {
                    Log.Warn("View Model not found. Searched: {0}.", string.Join(", ", viewModelTypeList.ToArray()));
                }

                return viewModelType;
            };

        /// <summary>
        ///   Gets or sets func to locates the view model for the specified view type.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>
        ///   Pass the view type as a parameter and receive a view model instance.
        /// </remarks>
        public static Func<Type, object> LocateForViewType { get; set; }
            = viewType => {
                Type viewModelType = LocateTypeForViewType(viewType, false);

                if (viewModelType != null) {
                    object viewModel = IoC.GetInstance(viewModelType, null);
                    if (viewModel != null) {
                        return viewModel;
                    }
                }

                viewModelType = LocateTypeForViewType(viewType, true);

                return viewModelType != null
                           ? IoC.GetInstance(viewModelType, null)
                           : null;
            };

        /// <summary>
        ///   Gets or sets func to locates the view model for the specified view instance.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <remarks>
        ///   Pass the view instance as a parameters and receive a view model instance.
        /// </remarks>
        public static Func<object, object> LocateForView { get; set; }
            = view => {
                if (view == null) {
                    return null;
                }

#if ANDROID || IOS
                return LocateForViewType(view.GetType());
#elif XFORMS
                return view is UIElement frameworkElement && frameworkElement.BindingContext != null
                    ? frameworkElement.BindingContext
                    : LocateForViewType(view.GetType());
#elif MAUI
                return view is UIElement frameworkElement && frameworkElement.BindingContext != null
                    ? frameworkElement.BindingContext
                    : LocateForViewType(view.GetType());
#else
                return view is FrameworkElement frameworkElement && frameworkElement.DataContext != null
                    ? frameworkElement.DataContext
                    : LocateForViewType(view.GetType());
#endif
            };

        /// <summary>
        /// Specifies how type mappings are created, including default type mappings. Calling this method will
        /// clear all existing name transformation rules and create new default type mappings according to the
        /// configuration.
        /// </summary>
        /// <param name="config">An instance of TypeMappingConfiguration that provides the settings for configuration.</param>
        public static void ConfigureTypeMappings(TypeMappingConfiguration config) {
            if (string.IsNullOrEmpty(config.DefaultSubNamespaceForViews)) {
                throw new ArgumentException("DefaultSubNamespaceForViews field cannot be blank.");
            }

            if (string.IsNullOrEmpty(config.DefaultSubNamespaceForViewModels)) {
                throw new ArgumentException("DefaultSubNamespaceForViewModels field cannot be blank.");
            }

            if (string.IsNullOrEmpty(config.NameFormat)) {
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

        /// <summary>
        /// Adds a default type mapping using the standard namespace mapping convention.
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddDefaultTypeMapping(string viewSuffix = DefaultViewSuffix) {
            if (!useNameSuffixesInMappings) {
                return;
            }

            // Check for <Namespace>.<BaseName><ViewSuffix> construct
            AddNamespaceMapping(string.Empty, string.Empty, viewSuffix);

            // Check for <Namespace>.Views.<NameSpace>.<BaseName><ViewSuffix> construct
            AddSubNamespaceMapping(defaultSubNsViews, defaultSubNsViewModels, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns.
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace.</param>
        /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace.</param>
        /// <param name="nsTargetsRegEx">Array of RegEx replace values for target namespaces.</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string[] nsTargetsRegEx, string viewSuffix = DefaultViewSuffix) {
            var replist = new List<string>();
            Action<string> func;

            const string basegrp = "${basename}";
            string interfacegrp = "${" + InterfaceCaptureGroupName + "}";

            if (useNameSuffixesInMappings) {
                if (viewModelSuffix.Contains(viewSuffix) || !includeViewSuffixInVmNames) {
                    string nameregex = string.Format(CultureInfo.InvariantCulture, nameFormat, basegrp, viewModelSuffix);
                    func = t => {
                        replist.Add(t + "I" + nameregex + interfacegrp);
                        replist.Add(t + "I" + basegrp + interfacegrp);
                        replist.Add(t + nameregex);
                        replist.Add(t + basegrp);
                    };
                } else {
                    string nameregex = string.Format(CultureInfo.InvariantCulture, nameFormat, basegrp, "${suffix}" + viewModelSuffix);
                    func = t => {
                        replist.Add(t + "I" + nameregex + interfacegrp);
                        replist.Add(t + nameregex);
                    };
                }
            } else {
                func = t => {
                    replist.Add(t + "I" + basegrp + interfacegrp);
                    replist.Add(t + basegrp);
                };
            }

            nsTargetsRegEx.ToList().Apply(t => func(t));

            string suffix = useNameSuffixesInMappings ? viewSuffix : string.Empty;

            string srcfilterregx = string.IsNullOrEmpty(nsSourceFilterRegEx)
                ? null
                : string.Concat(nsSourceFilterRegEx, string.Format(CultureInfo.InvariantCulture, nameFormat, RegExHelper.NameRegEx, suffix), "$");
            string rxbase = RegExHelper.GetNameCaptureGroup("basename");
            string rxsuffix = RegExHelper.GetCaptureGroup("suffix", suffix);

            // Add a dummy capture group -- place after the "$" so it can never capture anything
            string rxinterface = RegExHelper.GetCaptureGroup(InterfaceCaptureGroupName, string.Empty);
            NameTransformer.AddRule(
                string.Concat(nsSourceReplaceRegEx, string.Format(CultureInfo.InvariantCulture, nameFormat, rxbase, rxsuffix), "$", rxinterface),
                replist.ToArray(),
                srcfilterregx);
        }

        /// <summary>
        /// Adds a standard type mapping based on namespace RegEx replace and filter patterns.
        /// </summary>
        /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace.</param>
        /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace.</param>
        /// <param name="nsTargetRegEx">RegEx replace value for target namespace.</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string nsTargetRegEx, string viewSuffix = DefaultViewSuffix)
            => AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new[] { nsTargetRegEx }, viewSuffix);

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping.
        /// </summary>
        /// <param name="nsSource">Namespace of source type.</param>
        /// <param name="nsTargets">Namespaces of target type as an array.</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = DefaultViewSuffix) {
            // need to terminate with "." in order to concatenate with type name later
            string nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            // Start pattern search from beginning of string ("^")
            // unless original string was blank (i.e. special case to indicate "append target to source")
            if (!string.IsNullOrEmpty(nsSource)) {
                nsencoded = "^" + nsencoded;
            }

            // Capture namespace as "origns" in case we need to use it in the output in the future
            string nsreplace = RegExHelper.GetCaptureGroup("origns", nsencoded);

            string[] nsTargetsRegEx = nsTargets.Select(t => t + ".").ToArray();
            AddTypeMapping(nsreplace, null, nsTargetsRegEx, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping.
        /// </summary>
        /// <param name="nsSource">Namespace of source type.</param>
        /// <param name="nsTarget">Namespace of target type.</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = DefaultViewSuffix)
            => AddNamespaceMapping(nsSource, new[] { nsTarget }, viewSuffix);

        /// <summary>
        /// Adds a standard type mapping by substituting one subnamespace for another.
        /// </summary>
        /// <param name="nsSource">Subnamespace of source type.</param>
        /// <param name="nsTargets">Subnamespaces of target type as an array.</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddSubNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = DefaultViewSuffix) {
            // need to terminate with "." in order to concatenate with type name later
            string nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            string rxbeforetgt, rxaftersrc, rxaftertgt;
            string rxbeforesrc = rxbeforetgt = rxaftersrc = rxaftertgt = string.Empty;

            if (!string.IsNullOrEmpty(nsSource)) {
                if (!nsSource.StartsWith("*", StringComparison.OrdinalIgnoreCase)) {
                    rxbeforesrc = RegExHelper.GetNamespaceCaptureGroup("nsbefore");
                    rxbeforetgt = @"${nsbefore}";
                }

                if (!nsSource.EndsWith("*", StringComparison.OrdinalIgnoreCase)) {
                    rxaftersrc = RegExHelper.GetNamespaceCaptureGroup("nsafter");
                    rxaftertgt = "${nsafter}";
                }
            }

            string rxmid = RegExHelper.GetCaptureGroup("subns", nsencoded);
            string nsreplace = string.Concat(rxbeforesrc, rxmid, rxaftersrc);

            string[] nsTargetsRegEx = nsTargets.Select(t => string.Concat(rxbeforetgt, t, ".", rxaftertgt)).ToArray();
            AddTypeMapping(nsreplace, null, nsTargetsRegEx, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping by substituting one subnamespace for another.
        /// </summary>
        /// <param name="nsSource">Subnamespace of source type.</param>
        /// <param name="nsTarget">Subnamespace of target type.</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional).</param>
        public static void AddSubNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = DefaultViewSuffix)
            => AddSubNamespaceMapping(nsSource, new[] { nsTarget }, viewSuffix);

        /// <summary>
        ///   Makes a type name into an interface name.
        /// </summary>
        /// <param name = "typeName">The part.</param>
        public static string MakeInterface(string typeName) {
            string suffix = string.Empty;
            if (typeName.Contains("[[")) {
                // generic type
                int genericParStart = typeName.IndexOf("[[", StringComparison.OrdinalIgnoreCase);
                suffix = typeName.Substring(genericParStart);
                typeName = typeName.Remove(genericParStart);
            }

            int index = typeName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase);

            return typeName.Insert(index + 1, "I") + suffix;
        }

        private static void SetAllDefaults() {
            if (useNameSuffixesInMappings) {
                // Add support for all view suffixes
                ViewSuffixList.Apply(AddDefaultTypeMapping);
            } else {
                AddSubNamespaceMapping(defaultSubNsViews, defaultSubNsViewModels);
            }
        }
    }
}
