namespace Caliburn.Micro
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Markup;

    public static class View
    {
        public static readonly DependencyProperty ApplyConventionsProperty =
            DependencyProperty.RegisterAttached(
                "ApplyConventions",
                typeof(bool?),
                typeof(View),
                null
                );

        public static bool? GetApplyConventions(DependencyObject d)
        {
            return (bool?)d.GetValue(ApplyConventionsProperty);
        }

        public static void SetApplyConventions(DependencyObject d, bool? value)
        {
            d.SetValue(ApplyConventionsProperty, value);
        }

        public static DependencyProperty ModelProperty =
            DependencyProperty.RegisterAttached(
                "Model",
                typeof(object),
                typeof(View),
                new PropertyMetadata(OnModelChanged)
                );

        public static void SetModel(DependencyObject d, object value)
        {
            d.SetValue(ModelProperty, value);
        }

        public static object GetModel(DependencyObject d)
        {
            return d.GetValue(ModelProperty);
        }

        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.RegisterAttached(
                "Context",
                typeof(object),
                typeof(View),
                new PropertyMetadata(OnContextChanged)
                );

        public static object GetContext(DependencyObject d)
        {
            return d.GetValue(ContextProperty);
        }

        public static void SetContext(DependencyObject d, object value)
        {
            d.SetValue(ContextProperty, value);
        }

        private static void OnModelChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue)
                return;

            if (args.NewValue != null)
            {
                var context = GetContext(targetLocation);
                var view = ViewLocator.LocateForModel(args.NewValue, context);

                ViewModelBinder.Bind(args.NewValue, view, context);
                SetContentProperty(targetLocation, view);
            }
            else SetContentProperty(targetLocation, args.NewValue);
        }

        private static void OnContextChanged(DependencyObject targetLocation, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;

            var model = GetModel(targetLocation);
            if (model == null)
                return;

            var view = ViewLocator.LocateForModel(model, e.NewValue);
            ViewModelBinder.Bind(model, view, e.NewValue);

            SetContentProperty(targetLocation, view);
        }

        public static UIElement GetWithViewModel<TViewModel>(string context = null)
        {
            var viewModel = IoC.GetInstance<TViewModel>();
            var view = ViewLocator.LocateForModel(viewModel, context);

            ViewModelBinder.Bind(viewModel, view, context);

            return view;
        }

        private static void SetContentProperty(object targetLocation, object view)
        {
            var type = targetLocation.GetType();
            var contentProperty = type.GetAttributes<ContentPropertyAttribute>(true)
                .FirstOrDefault() ?? new ContentPropertyAttribute("Content");

            type.GetProperty(contentProperty.Name)
                .SetValue(targetLocation, view, null);
        }
    }
}