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
        /// <summary>
        /// The default view context.
        /// </summary>
        public static readonly object DefaultContext = new object();

        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));
        const string View = "View";
        const string Model = "Model";

        /// <summary>
        /// Retrieves the view from the IoC container or tries to create it if not found.
        /// </summary>
        /// <remarks>Pass the type of view as a parameter and recieve an instance of the view.</remarks>
        public static Func<Type, UIElement> GetOrCreateViewType = viewType =>{
            var view = IoC.GetAllInstances(viewType)
                .FirstOrDefault() as UIElement;

            if(view != null)
                return view;

            if(viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
            {
                var ex = new Exception(string.Format("Cannot instantiate {0}.", viewType.FullName));
                Log.Error(ex);
                throw ex;
            }

            return (UIElement)Activator.CreateInstance(viewType);
        };

        /// <summary>
        /// Locates the view for the specified model type.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>Pass the model type and the context instance (or null) as parameters and recieve a view instance.</remarks>
        public static Func<Type, object, UIElement> LocateForModelType = (modelType, context) =>{
            var viewTypeName = modelType.FullName.Replace(Model, string.Empty);
            if(context != null)
            {
                viewTypeName = viewTypeName.Remove(viewTypeName.Length - View.Length, View.Length);
                viewTypeName = viewTypeName + "." + context;
            }

            var viewType = (from assmebly in AssemblySource.Instance
                            from type in assmebly.GetExportedTypes()
                            where type.FullName == viewTypeName
                            select type).FirstOrDefault();

            return viewType == null
                ? new TextBlock { Text = "View not found " + viewTypeName }
                : GetOrCreateViewType(viewType);
        };

        /// <summary>
        /// Locates the view for the specified model instance.
        /// </summary>
        /// <returns>The view.</returns>
        /// <remarks>Pass the model instance and the context (or null) as parameters and receive a view instance.</remarks>
        public static Func<object, object, UIElement> LocateForModel = (model, context) =>{
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
                        Log.Info("Cached view returned for {0}.", model);
                        return view;
                    }
#else
                    Log.Info("Cached view returned for {0}.", model);
                    return view;
#endif
                }
            }

            return LocateForModelType(model.GetType(), context);
        };
    }
}