namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Windows;

    public static class ViewLocator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewLocator));
        public static readonly object DefaultContext = new object();
        const string View = "View";
        const string Model = "Model";

        public static UIElement Locate(object viewModel, object context = null)
        {
            var viewAware = viewModel as IViewAware;
            if(viewAware != null)
            {
                var existing = viewAware.GetView(context ?? DefaultContext);
                if (existing != null)
                    return (UIElement)existing;
            }

            var viewTypeName = viewModel.GetType().FullName.Replace(Model, string.Empty);

            if (context != null)
            {
                viewTypeName = viewTypeName.Remove(viewTypeName.Length - View.Length, View.Length);
                viewTypeName = viewTypeName + "." + context;
            }

            var viewType = (from assmebly in AssemblySource.Known
                            from type in assmebly.GetExportedTypes()
                            where type.FullName == viewTypeName
                            select type).FirstOrDefault();

            if(viewType == null)
            {
                Log.Warn("View not found: {0}", viewTypeName);
                return null;
            }

            return GetOrCreateViewFromType(viewType);
        }

        static UIElement GetOrCreateViewFromType(Type type)
        {
            var view = IoC.GetAllInstances(type)
                .FirstOrDefault() as UIElement;

            if (view != null)
                return view;

            if (type.IsInterface || type.IsAbstract || !typeof(UIElement).IsAssignableFrom(type))
            {
                var ex = new Exception(string.Format("Cannot instantiate {0}.", type.FullName));
                Log.Error(ex);
                throw ex;
            }

            return (UIElement)Activator.CreateInstance(type);
        }
    }
}