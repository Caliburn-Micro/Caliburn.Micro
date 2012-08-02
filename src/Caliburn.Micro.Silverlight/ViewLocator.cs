namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Collections.Generic;
#if !WinRT
    using System.Windows.Controls;
#else
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
#endif

#if !SILVERLIGHT && !WinRT
    using System.Windows.Interop;
#endif

    /// <summary>
    ///   A strategy for determining which view to use for a given model.
    /// </summary>
    public static class ViewLocator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));

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

        static ViewLocator()
        {
            ConfigureTypeMappings(new TypeMappingConfiguration());
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

            if (config.UseNameSuffixesInMappings)
            {
                if (String.IsNullOrEmpty(config.ViewModelSuffix))
                {
                    throw new ArgumentException("ViewModelSuffix field cannot be blank if UseNameSuffixesInMappings is true.");
                }
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
        public static void AddDefaultTypeMapping(string viewSuffix = "View")
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
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string[] nsTargetsRegEx, string viewSuffix = "View")
        {
            RegisterViewSuffix(viewSuffix);

            var replist = new List<string>();
            var repsuffix = useNameSuffixesInMappings ? viewSuffix : String.Empty;
            const string basegrp = "${basename}";

            foreach (var t in nsTargetsRegEx)
            {
                replist.Add(t + String.Format(nameFormat, basegrp, repsuffix));
            }

            string rxbase = RegExHelper.GetNameCaptureGroup("basename");
            string suffix = String.Empty;
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
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string nsTargetRegEx, string viewSuffix = "View")
        {
            AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new[] { nsTargetRegEx }, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping based on simple namespace mapping
        /// </summary>
        /// <param name="nsSource">Namespace of source type</param>
        /// <param name="nsTargets">Namespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = "View")
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
        public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
        {
            AddNamespaceMapping(nsSource, new[] { nsTarget }, viewSuffix);
        }

        /// <summary>
        /// Adds a standard type mapping by substituting one subnamespace for another
        /// </summary>
        /// <param name="nsSource">Subnamespace of source type</param>
        /// <param name="nsTargets">Subnamespaces of target type as an array</param>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddSubNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = "View")
        {
            //need to terminate with "." in order to concatenate with type name later
            var nsencoded = RegExHelper.NamespaceToRegEx(nsSource + ".");

            string rxbeforetgt, rxaftersrc, rxaftertgt;
            string rxbeforesrc = rxbeforetgt = rxaftersrc = rxaftertgt = String.Empty;

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
        public static void AddSubNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
        {
            AddSubNamespaceMapping(nsSource, new[] { nsTarget }, viewSuffix);
        }

        /// <summary>
        ///   Retrieves the view from the IoC container or tries to create it if not found.
        /// </summary>
        /// <remarks>
        ///   Pass the type of view as a parameter and recieve an instance of the view.
        /// </remarks>
        public static Func<Type, UIElement> GetOrCreateViewType = viewType =>
        {
            var view = IoC.GetAllInstances(viewType)
                           .FirstOrDefault() as UIElement;

            if (view != null)
            {
                InitializeComponent(view);
                return view;
            }

#if !WinRT
            if(viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
                return new TextBlock { Text = string.Format("Cannot create {0}.", viewType.FullName) };
#else
            var viewTypeInfo = viewType.GetTypeInfo();
            var uiElementInfo = typeof(UIElement).GetTypeInfo();

            if (viewTypeInfo.IsInterface || viewTypeInfo.IsAbstract || !uiElementInfo.IsAssignableFrom(viewTypeInfo))
                return new TextBlock { Text = string.Format("Cannot create {0}.", viewType.FullName) };
#endif

            view = (UIElement)System.Activator.CreateInstance(viewType);
            InitializeComponent(view);
            return view;
        };

        /// <summary>
        /// Modifies the name of the type to be used at design time.
        /// </summary>
        public static Func<string, string> ModifyModelTypeAtDesignTime = modelTypeName =>
        {
            if (modelTypeName.StartsWith("_"))
            {
                var index = modelTypeName.IndexOf(".");
                modelTypeName = modelTypeName.Substring(index + 1);
                index = modelTypeName.IndexOf(".");
                modelTypeName = modelTypeName.Substring(index + 1);
            }

            return modelTypeName;
        };

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
            string synonymregex = String.Empty, grpsuffix = String.Empty;
            if (useNameSuffixesInMappings)
            {
                //Create RegEx for matching any of the synonyms registered
                synonymregex = "(" + String.Join("|", ViewSuffixList.ToArray()) + ")";
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
        public static Func<Type, DependencyObject, object, Type> LocateTypeForModelType = (modelType, displayLocation, context) =>
        {
            var viewTypeName = modelType.FullName;

            if (Execute.InDesignMode)
            {
                viewTypeName = ModifyModelTypeAtDesignTime(viewTypeName);
            }

            viewTypeName = viewTypeName.Substring(
                0,
                viewTypeName.IndexOf("`") < 0
                    ? viewTypeName.Length
                    : viewTypeName.IndexOf("`")
                );

            var viewTypeList = TransformName(viewTypeName, context);
            var viewType = viewTypeList.Join(AssemblySource.Instance.SelectMany(a => a.GetExportedTypes()), n => n, t => t.FullName, (n, t) => t).FirstOrDefault();

            if (viewType == null)
            {
                Log.Warn("View not found. Searched: {0}.", string.Join(", ", viewTypeList.ToArray()));
            }

            return viewType;
        };

        /// <summary>
        ///   Locates the view for the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>
        ///   Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view instance.
        /// </remarks>
        public static Func<Type, DependencyObject, object, UIElement> LocateForModelType = (modelType, displayLocation, context) =>
        {
            var viewType = LocateTypeForModelType(modelType, displayLocation, context);

            return viewType == null
                       ? new TextBlock { Text = string.Format("Cannot find view for {0}.", modelType) }
                       : GetOrCreateViewType(viewType);
        };

        /// <summary>
        ///   Locates the view for the specified model instance.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>
        ///   Pass the model instance, display location (or null) and the context (or null) as parameters and receive a view instance.
        /// </remarks>
        public static Func<object, DependencyObject, object, UIElement> LocateForModel = (model, displayLocation, context) =>
        {
            var viewAware = model as IViewAware;
            if (viewAware != null)
            {
                var view = viewAware.GetView(context) as UIElement;
                if (view != null)
                {
#if !SILVERLIGHT && !WinRT
                    var windowCheck = view as Window;
                    if (windowCheck == null || (!windowCheck.IsLoaded && !(new WindowInteropHelper(windowCheck).Handle == IntPtr.Zero)))
                    {
                        Log.Info("Using cached view for {0}.", model);
                        return view;
                    }
#else
                    Log.Info("Using cached view for {0}.", model);
                    return view;
#endif
                }
            }

            return LocateForModelType(model.GetType(), displayLocation, context);
        };
#if !WinRT
        /// <summary>
        /// Transforms a view type into a pack uri.
        /// </summary>
        public static Func<Type, Type, string> DeterminePackUriFromType = (viewModelType, viewType) => {
            var assemblyName = viewType.Assembly.GetAssemblyName();
            var uri = viewType.FullName.Replace(assemblyName, string.Empty).Replace(".", "/") + ".xaml";

            if(!Application.Current.GetType().Assembly.GetAssemblyName().Equals(assemblyName)) {
                return "/" + assemblyName + ";component" + uri;
            }

            return uri;
        };
#else
        /// <summary>
        /// Transforms a view type into a pack uri.
        /// </summary>
        public static Func<Type, Type, string> DeterminePackUriFromType = (viewModelType, viewType) =>
        {
            var assemblyName = viewType.GetTypeInfo().Assembly.GetAssemblyName();
            var uri = viewType.FullName.Replace(assemblyName, string.Empty).Replace(".", "/") + ".xaml";

            if (!Application.Current.GetType().GetTypeInfo().Assembly.GetAssemblyName().Equals(assemblyName))
            {
                return "/" + assemblyName + ";component" + uri;
            }

            return uri;
        };
#endif

        /// <summary>
        ///   When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.
        /// </summary>
        /// <param name = "element">The element to initialize</param>
        public static void InitializeComponent(object element)
        {
#if !WinRT
            var method = element.GetType()
                .GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.Instance);
#else
            var method = element.GetType().GetTypeInfo()
                .GetDeclaredMethod("InitializeComponent");
#endif

            if (method == null)
                return;

            method.Invoke(element, null);
        }
    }
}