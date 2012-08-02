namespace Caliburn.Micro
{
    using System;
    using Windows.UI.Interactivity;
    using Windows.UI.Xaml;

    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
    public class Parameter : FrameworkElement, IAttachedObject
    {
        FrameworkElement associatedObject;
        WeakReference owner;

        /// <summary>
        /// A dependency property representing the parameter's value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(Parameter),
                new PropertyMetadata(null, OnValueChanged)
                );


        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        FrameworkElement IAttachedObject.AssociatedObject
        {
            get { return associatedObject; }
        }

        ActionMessage Owner
        {
            get { return owner == null ? null : owner.Target as ActionMessage; }
            set { owner = new WeakReference(value); }
        }

        void IAttachedObject.Attach(FrameworkElement dependencyObject)
        {
            associatedObject = dependencyObject;
        }

        void IAttachedObject.Detach()
        {
            associatedObject = null;
        }

        /// <summary>
        /// Makes the parameter aware of the <see cref="ActionMessage"/> that it's attached to.
        /// </summary>
        /// <param name="owner">The action message.</param>
        internal void MakeAwareOf(ActionMessage owner)
        {
            Owner = owner;
        }

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parameter = (Parameter)d;

            if (parameter.Owner != null)
            {
                parameter.Owner.UpdateAvailability();
            }
        }
    }
}