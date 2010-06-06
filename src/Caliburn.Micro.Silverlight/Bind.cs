namespace Caliburn.Micro
{
    using System.Windows;

    public static class Bind
    {
        public static DependencyProperty ModelProperty =
            DependencyProperty.RegisterAttached(
                "Model",
                typeof(object),
                typeof(Bind),
                new PropertyMetadata(new PropertyChangedCallback(ModelChanged))
                );

        public static object GetModel(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(ModelProperty);
        }

        public static void SetModel(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(ModelProperty, value);
        }

        static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue != e.OldValue)
                ViewModelBinder.Bind(e.NewValue, d);
        }
    }
}