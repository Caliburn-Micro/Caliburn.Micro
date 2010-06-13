namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Windows;

#if !SILVERLIGHT
    using System.Windows.Interop;
#endif

    public static class ViewLocator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));
        public static readonly object DefaultContext = new object();
        const string View = "View";
        const string Model = "Model";

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

            if(viewType == null)
            {
                Log.Warn("View not found: {0}", viewTypeName);
                return null;
            }

            return GetOrCreateViewType(viewType);
        };

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

        public static UIElement GetOrCreateViewType(Type viewType)
        {
            var view = IoC.GetAllInstances(viewType)
                .FirstOrDefault() as UIElement;

            if (view != null)
                return view;

            if (viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
            {
                var ex = new Exception(string.Format("Cannot instantiate {0}.", viewType.FullName));
                Log.Error(ex);
                throw ex;
            }

            return (UIElement)Activator.CreateInstance(viewType);
        }
    }
}