namespace Caliburn.Micro
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
    public class Parameter : DependencyObject, IAttachedObject
    {
        /// <summary>
        /// A dependency property representing the parameter's value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(Parameter),
                new PropertyMetadata(OnValueChanged)
                );

        DependencyObject associatedObject;
        ActionMessage owner;

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>The value.</value>
        [Category("Common Properties")]
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

        /// <summary>
        /// Makes the parameter aware of the <see cref="ActionMessage"/> that it's attached to.
        /// </summary>
        /// <param name="owner">The action message.</param>
        internal void MakeAwareOf(ActionMessage owner)
        {
            this.owner = owner;
        }

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parameter = (Parameter)d;

            if(parameter.owner != null)
                parameter.owner.UpdateAvailability();
        }
    }
}