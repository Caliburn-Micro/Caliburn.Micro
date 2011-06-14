namespace Caliburn.Micro {
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;

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

        static ViewLocator() {
            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)

            //NameTransformer.AddRule("ViewModel$", "View");         //less specific pattern
            //NameTransformer.AddRule("PageViewModel$", "Page");     //more specific pattern
            //Add more default transforms here. Can also be called from the bootstrapper for project-specific transforms.
            //NameTransformer.AddRule("FormViewModel$", "Form");

            NameTransformer.AddRule("Model$", string.Empty);
            NameTransformer.AddRule("ViewModel$", "View");
            NameTransformer.AddRule("PageViewModel$", "Page");

            //Check for <Namespace>.ViewModels.<BaseName>ViewModel construct
            NameTransformer.AddRule
                (
                    @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsvm>ViewModels\.)(?<nsafter>[A-Za-z_]\w*\.)*(?<basename>[A-Za-z_]\w*)(?<suffix>ViewModel$)",
                    @"${nsbefore}Views.${nsafter}${basename}View",
                    @"(([A-Za-z_]\w*\.)*)?ViewModels\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*ViewModel$"
                );

            //Check for <Namespace>.ViewModels.<BaseName>PageViewModel construct
            NameTransformer.AddRule
                (
                    @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsvm>ViewModels\.)(?<nsafter>[A-Za-z_]\w*\.)*(?<basename>[A-Za-z_]\w*)(?<suffix>PageViewModel$)",
                    @"${nsbefore}Views.${nsafter}${basename}Page",
                    @"(([A-Za-z_]\w*\.)*)?ViewModels\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*PageViewModel$"
                );
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
        ///   Locates the view type based on the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>
        ///   Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.
        /// </remarks>
        public static Func<Type, DependencyObject, object, Type> LocateTypeForModelType = (modelType, displayLocation, context) => {
            var viewTypeName = modelType.FullName.Substring(
                0,
                modelType.FullName.IndexOf("`") < 0
                    ? modelType.FullName.Length
                    : modelType.FullName.IndexOf("`")
                );

            Func<string, string> getReplaceString;
            if (context == null) {
                getReplaceString = r => { return r; };
            }
            else {
                getReplaceString = r => {
                    return Regex.Replace(r, Regex.IsMatch(r, "Page$") ? "Page$" : "View$", ContextSeparator + context);
                };
            }

            var viewTypeList = NameTransformer.Transform(viewTypeName, getReplaceString);
            var viewType = (from assembly in AssemblySource.Instance
                            from type in assembly.GetExportedTypes()
                            where viewTypeList.Contains(type.FullName)
                            select type).FirstOrDefault();

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