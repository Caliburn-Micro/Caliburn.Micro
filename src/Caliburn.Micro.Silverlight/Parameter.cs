namespace Caliburn.Micro
{
    using System.Windows;
    using System.Windows.Interactivity;

    public class Parameter : DependencyObject, IAttachedObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(Parameter),
                new PropertyMetadata(OnValueChanged)
                );

        DependencyObject associatedObject;

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        DependencyObject IAttachedObject.AssociatedObject
        {
            get { return associatedObject; }
        }

        void IAttachedObject.Attach(DependencyObject dependencyObject)
        {
            associatedObject = dependencyObject;
        }

        void IAttachedObject.Detach()
        {
            associatedObject = null;
        }

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}