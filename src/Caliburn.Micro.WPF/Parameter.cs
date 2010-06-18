namespace Caliburn.Micro
{
    using System.Windows;
    using System.Windows.Interactivity;

    public class Parameter : Freezable, IAttachedObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(Parameter),
                new PropertyMetadata(OnValueChanged)
                );

        DependencyObject associatedObject;
        ActionMessage owner;

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        DependencyObject IAttachedObject.AssociatedObject
        {
            get
            {
                ReadPreamble();
                return associatedObject;
            }
        }

        void IAttachedObject.Attach(DependencyObject dependencyObject)
        {
            WritePreamble();
            associatedObject = dependencyObject;
            WritePostscript();
        }

        void IAttachedObject.Detach()
        {
            WritePreamble();
            associatedObject = null;
            WritePostscript();
        }

        protected override Freezable CreateInstanceCore()
        {
            return new Parameter();
        }

        internal void MakeAwareOf(ActionMessage owner)
        {
            this.owner = owner;
        }

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parameter = (Parameter)d;

            if (parameter.owner != null)
                parameter.owner.UpdateAvailability();
        }
    }
}