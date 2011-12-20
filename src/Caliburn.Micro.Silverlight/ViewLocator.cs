namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic;

#if !SILVERLIGHT
    using System.Windows.Interop;
#endif

    /// <summary>
    ///   A strategy for determining which view to use for a given model.
    /// </summary>
    public static class ViewLocator {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));

        ///<summary>
        /// Used to transform names.
        ///</summary>
        public static NameTransformer NameTransformer = new NameTransformer();

        /// <summary>
        ///   Separator used when resolving View names for context instances.
        /// </summary>
        public static string ContextSeparator = ".";

        /// <summary>
        /// This keeps a list of view suffixes and synonyms that have been added through a mapping
        /// or directly through the NameTransformer
        /// </summary>
        private static List<string> ViewSuffixList;

        static ViewLocator() {
            ViewSuffixList = new List<string>();

            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)

            //NameTransformer.AddRule("ViewModel$", "View");         //less specific pattern
            //NameTransformer.AddRule("PageViewModel$", "Page");     //more specific pattern
            //Add more default transforms here. Can also be called from the bootstrapper for project-specific transforms.
            //NameTransformer.AddRule("FormViewModel$", "Form");

            //Fallback rule - just remove "Model" from end of name
            NameTransformer.AddRule("Model$", string.Empty);

            //Add support for two standard View suffixes
            AddDefaultTypeMapping("View");
            AddDefaultTypeMapping("Page");
        }

        /// <summary>
        /// Adds a default type mapping using the standard namespace mapping convention
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
        public static void AddDefaultTypeMapping(string viewSuffix = "View") {
            //Check for <Namespace>.<BaseName><ViewSuffix> construct
            AddNamespaceMapping(String.Empty, String.Empty, viewSuffix);

            //Check for <Namespace>.ViewModels.<NameSpace>.<BaseName><ViewSuffix> construct
            AddSubNamespaceMapping("ViewModels", "Views", viewSuffix);
        }

        /// <summary>
        /// This method registers a View suffix or synonym so that View Context resolution works properly.
        /// It is automatically called internally when calling AddNamespaceMapping(), AddDefaultTypeMapping(),
        /// or AddTypeMapping(). It should not need to be called explicitly unless a rule that handles synonyms
        /// is added directly through the NameTransformer.
        /// </summary>
        /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View".</param>
        public static void RegisterViewSuffix(string viewSuffix) {
            if (ViewSuffixList.Count(s => s == viewSuffix) == 0) {
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
        public static void AddTypeMapping(string nsSourceReplaceRegEx, string nsSourceFilterRegEx, string[] nsTargetsRegEx, string viewSuffix = "View") {
            RegisterViewSuffix(viewSuffix);

            var replist = new List<string>();

            foreach(var t in nsTargetsRegEx) {
                replist.Add(t + @"${basename}" + viewSuffix);
            }

            string synonym = (viewSuffix == "View") ? String.Empty : viewSuffix;
            string rxsrcfilter = String.IsNullOrEmpty(nsSourceFilterRegEx) 
                ? null 
                : String.Concat(nsSourceFilterRegEx, RegExHelper.NameRegEx, synonym, @"ViewModel$");
            string rxbase = RegExHelper.GetNameCaptureGroup("basename");
            string rxsuffix = RegExHelper.GetCaptureGroup("suffix", synonym + @"ViewModel$");

            NameTransformer.AddRule(
                String.Concat(nsSourceReplaceRegEx, rxbase, rxsuffix),
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
        ///   Retrieves the view from the IoC container or tries to create it if not found.
        /// </summary>
        /// <remarks>
        ///   Pass the type of view as a parameter and recieve an instance of the view.
        /// </remarks>
        public static Func<Type, UIElement> GetOrCreateViewType = viewType => {
            var view = IoC.GetAllInstances(viewType)
                           .FirstOrDefault() as UIElement;

            if(view != null) {
                InitializeComponent(view);
                return view;
            }

            if(viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
                return new TextBlock { Text = string.Format("Cannot create {0}.", viewType.FullName) };

            view = (UIElement)Activator.CreateInstance(viewType);
            InitializeComponent(view);
            return view;
        };

        /// <summary>
        /// Modifies the name of the type to be used at design time.
        /// </summary>
        public static Func<string, string> ModifyModelTypeAtDesignTime = modelTypeName => {
            if(modelTypeName.StartsWith("_")) {
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
        /// <param name="typeName">The name of the ViewModel type being resolved to its companion View.</param>
        /// <param name="context">An instance of the context. (Optional)</param>
        /// <returns></returns>
        public static IEnumerable<string> TransformName(string typeName, object context = null) {
            Func<string, string> getReplaceString;
            if (context == null) {
                getReplaceString = r => { return r; };
            }
            else {
                getReplaceString = r => {
                    //Create RegEx for matching any of the synonyms registered
                    var synonymregex = String.Join("|", ViewSuffixList.Select(s => @"(" + s + @"$)").ToArray());

                    //Strip out the synonym
                    return Regex.Replace(r, synonymregex, ContextSeparator + context);
                };
            }

            return NameTransformer.Transform(typeName, getReplaceString);
        }

        /// <summary>
        ///   Locates the view type based on the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>
        ///   Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.
        /// </remarks>
        public static Func<Type, DependencyObject, object, Type> LocateTypeForModelType = (modelType, displayLocation, context) => {
            var viewTypeName = modelType.FullName;

            if (Execute.InDesignMode) {
                viewTypeName = ModifyModelTypeAtDesignTime(viewTypeName);
            }

            viewTypeName = viewTypeName.Substring(
                0,
                viewTypeName.IndexOf("`") < 0
                    ? viewTypeName.Length
                    : viewTypeName.IndexOf("`")
                );

            var viewTypeList = TransformName(viewTypeName, context);
            var viewType = (from assembly in AssemblySource.Instance
                            from type in assembly.GetExportedTypes()
                            where viewTypeList.Contains(type.FullName)
                            select type).FirstOrDefault();

            if(viewType == null) {
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
        public static Func<Type, DependencyObject, object, UIElement> LocateForModelType = (modelType, displayLocation, context) => {
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
        public static Func<object, DependencyObject, object, UIElement> LocateForModel = (model, displayLocation, context) => {
            var viewAware = model as IViewAware;
            if(viewAware != null) {
                var view = viewAware.GetView(context) as UIElement;
                if(view != null) {
#if !SILVERLIGHT
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

        /// <summary>
        ///   When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.
        /// </summary>
        /// <param name = "element">The element to initialize</param>
        public static void InitializeComponent(object element) {
            var method = element.GetType()
                .GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.Instance);

            if(method == null)
                return;

            method.Invoke(element, null);
        }
    }
}