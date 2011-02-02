namespace Caliburn.Micro
{
    using System.Windows;

    /// <summary>
    /// Hosts dependency properties for binding.
    /// </summary>
    public static class Bind
    {
        /// <summary>
        /// Allows binding on an existing view.
        /// </summary>
        public static DependencyProperty ModelProperty =
            DependencyProperty.RegisterAttached(
                "Model",
                typeof(object),
                typeof(Bind),
                new PropertyMetadata(new PropertyChangedCallback(ModelChanged))
                );

        /// <summary>
        /// Gets the model to bind to.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to bind to.</param>
        /// <returns>The model.</returns>
        public static object GetModel(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(ModelProperty);
        }

        /// <summary>
        /// Sets the model to bind to.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to bind to.</param>
        /// <param name="value">The model.</param>
        public static void SetModel(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(ModelProperty, value);
        }

        static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Bootstrapper.IsInDesignMode || e.NewValue == null || e.NewValue == e.OldValue)
                return;

            var fe = d as FrameworkElement;
            if (fe == null)
                return;

            RoutedEventHandler handler = null;
            handler = delegate{
                var target = e.NewValue;
                var containerKey = e.NewValue as string;

                if (containerKey != null)
                    target = IoC.GetInstance(null, containerKey);

                d.SetValue(View.IsLoadedProperty, true);
                d.SetValue(View.IsScopeRootProperty, true);

                ViewModelBinder.Bind(target, d, null);

                fe.Loaded -= handler;
            };

#if NET
            if(fe.IsLoaded)
                handler(fe, new RoutedEventArgs());
            else fe.Loaded += handler;
#else
            fe.Loaded += handler;
#endif
        }
    }
}