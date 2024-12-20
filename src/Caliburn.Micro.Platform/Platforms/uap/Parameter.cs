namespace Caliburn.Micro
{
    using System;
    using Windows.UI.Xaml;

    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
#if WINDOWS_UWP
    public class Parameter : DependencyObject, IAttachedObject
    {
        DependencyObject _associatedObject;
#else
    public class Parameter : FrameworkElement, IAttachedObject {
        FrameworkElement associatedObject;
#endif
        WeakReference _owner;

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

#if WINDOWS_UWP
        DependencyObject IAttachedObject.AssociatedObject
        {
#else
        FrameworkElement IAttachedObject.AssociatedObject {
#endif
            get { return _associatedObject; }
        }


        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        protected ActionMessage Owner
        {
            get { return _owner == null ? null : _owner.Target as ActionMessage; }
            set { _owner = new WeakReference(value); }
        }

#if WINDOWS_UWP
        void IAttachedObject.Attach(DependencyObject dependencyObject)
        {
#else
        void IAttachedObject.Attach(FrameworkElement dependencyObject) {
#endif
            _associatedObject = dependencyObject;
        }

        void IAttachedObject.Detach()
        {
            _associatedObject = null;
        }

        /// <summary>
        /// Makes the parameter aware of the <see cref="ActionMessage"/> that it's attached to.
        /// </summary>
        /// <param name="actionMessageOwner">The action message.</param>
        internal void MakeAwareOf(ActionMessage actionMessageOwner)
        {
            Owner = actionMessageOwner;
        }

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parameter = (Parameter)d;
            var owner = parameter.Owner;

            if (owner != null)
            {
                owner.UpdateAvailability();
            }
        }
    }
}
