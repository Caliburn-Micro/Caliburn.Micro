namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

#if !SILVERLIGHT
    using System.Windows.Interop;
#endif

    /// <summary>
    /// A strategy for determining which view to use for a given model.
    /// </summary>
    public static class ViewLocator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));

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
                ).Replace("Model", string.Empty);

            if (context != null)
            {
                viewTypeName = viewTypeName.Remove(viewTypeName.Length - 4, 4);
                viewTypeName = viewTypeName + "." + context;
            }

            var viewType = (from assmebly in AssemblySource.Instance
                            from type in assmebly.GetExportedTypes()
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