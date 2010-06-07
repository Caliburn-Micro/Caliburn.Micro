namespace Caliburn.Micro
{
    using System;
    using System.Windows;
    using System.Windows.Interactivity;

    public class Parameter : Freezable, IAttachedObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(Parameter),
                null
                );

        DependencyObject associatedObject;

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
            if(dependencyObject == associatedObject)
                return;

            if (associatedObject != null)
                throw new InvalidOperationException("Cannot host parameter multiple times.");

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
    }
}