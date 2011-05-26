namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic;

#if !SILVERLIGHT
    using System.Windows.Interop;
#endif

    /// <summary>
    /// A strategy for determining which view to use for a given model.
    /// </summary>
    public static class ViewLocator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));

        //Use KeyValuePair<string, string> to avoid defining a special class for pattern string / replace string pair
        private static List<KeyValuePair<string, string>> _TransformPairList = new List<KeyValuePair<string, string>>();

        static ViewLocator()
        {
            //Add pattern string / replace string pairs. Pattern string = Key, Replace string = Value.
            //Add to list by increasing order of specificity (i.e. less specific pattern to more specific pattern)
        
            AddTransformPair("ViewModel$", "View");         //less specific pattern
            AddTransformPair("PageViewModel$", "Page");     //more specific pattern
            //Add more default transforms here. Can also be called from the bootstrapper for project-specific transforms.
            //AddTransformPair("FormViewModel$", "Form");
        }

        /// <summary>
        /// Add more pattern string / replace string pairs
        /// </summary>
        /// <param name="patternStr">The regular expression pattern to match in the ViewModel name.</param>
        /// <param name="replaceStr">The string that replaces the matched string.</param>
        public static void AddTransformPair(string patternStr, string replaceStr)
        {
            _TransformPairList.Add(new KeyValuePair<string, string>(patternStr, replaceStr));
        }

        /// <summary>
        /// Retrieves the view from the IoC container or tries to create it if not found.
        /// </summary>
        /// <remarks>Pass the type of view as a parameter and recieve an instance of the view.</remarks>
        public static Func<Type, UIElement> GetOrCreateViewType = viewType =>{
            var view = IoC.GetAllInstances(viewType)
                .FirstOrDefault() as UIElement;

            if (view != null) {
                InitializeComponent(view);
                return view;
            }

            if (viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
                return new TextBlock { Text = string.Format("Cannot create {0}.", viewType.FullName) };

            view = (UIElement)Activator.CreateInstance(viewType);
            InitializeComponent(view);
            return view;
        };

        /// <summary>
        /// Locates the view type based on the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.</remarks>
        public static Func<Type, DependencyObject, object, Type> LocateTypeForModelType = (modelType, displayLocation, context) => {
            var viewTypeName = modelType.FullName.Substring(0, modelType.FullName.IndexOf("`") < 0
                ? modelType.FullName.Length
                : modelType.FullName.IndexOf("`")
                );

            Func<KeyValuePair<string,string>,string> funcGetReplaceStr;
            if (context == null)
            {
                funcGetReplaceStr = (k) => { return k.Value; };
            }
            else
            {
                funcGetReplaceStr = (k) => { return "." + context; };
            }

            //Get a reversed copy of the list so that the least specific transform is done last, if it ever falls through to it
            var reversedList = _TransformPairList.Reverse<KeyValuePair<string, string>>();

            foreach (var kvp in reversedList)
            {
                var replaceStr = funcGetReplaceStr(kvp);
                if (System.Text.RegularExpressions.Regex.IsMatch(viewTypeName, kvp.Key))
                {
                    viewTypeName = System.Text.RegularExpressions.Regex.Replace(viewTypeName, kvp.Key, replaceStr);
                    break;
                }
            }

            var viewType = (from assembly in AssemblySource.Instance
                            from type in assembly.GetExportedTypes()
                            where type.FullName == viewTypeName
                            select type).FirstOrDefault();

            return viewType;
        };

        /// <summary>
        /// Locates the view for the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view instance.</remarks>
        public static Func<Type, DependencyObject, object, UIElement> LocateForModelType = (modelType, displayLocation, context) =>{
            var viewType = LocateTypeForModelType(modelType, displayLocation, context);

            return viewType == null
                ? new TextBlock { Text = string.Format("Cannot find view for {0}.", modelType) }
                : GetOrCreateViewType(viewType);
        };

        /// <summary>
        /// Locates the view for the specified model instance.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>Pass the model instance, display location (or null) and the context (or null) as parameters and receive a view instance.</remarks>
        public static Func<object, DependencyObject, object, UIElement> LocateForModel = (model, displayLocation, context) =>{
            var viewAware = model as IViewAware;
            if(viewAware != null)
            {
                var view = viewAware.GetView(context) as UIElement;
                if(view != null)
                {
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
        /// When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.
        /// </summary>
        /// <param name="element">The element to initialize</param>
        public static void InitializeComponent(object element) {
            var method = element.GetType()
                .GetMethod("InitializeComponent", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if(method == null)
                return;

            method.Invoke(element, null);
        }
    }
}