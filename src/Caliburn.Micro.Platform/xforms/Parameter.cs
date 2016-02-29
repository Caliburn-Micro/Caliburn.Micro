#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#else
namespace Caliburn.Micro
#endif
{
    using System;
#if XFORMS
    using global::Xamarin.Forms;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
#else
    using Windows.UI.Xaml;
#endif

    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
#if WinRT81 || XFORMS
    public class Parameter : DependencyObject, IAttachedObject {
        DependencyObject associatedObject;
#else
    public class Parameter : FrameworkElement, IAttachedObject
    {
        FrameworkElement associatedObject;
#endif
        WeakReference owner;

        /// <summary>
        /// A dependency property representing the parameter's value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Value",
                typeof(object),
                typeof(Parameter),
                null, 
                OnValueChanged
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

#if WinRT81 || XFORMS
        DependencyObject IAttachedObject.AssociatedObject {
#else
        FrameworkElement IAttachedObject.AssociatedObject
        {
#endif
            get { return associatedObject; }
        }


        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        protected ActionMessage Owner
        {
            get { return owner == null ? null : owner.Target as ActionMessage; }
            set { owner = new WeakReference(value); }
        }

#if WinRT81 || XFORMS
        void IAttachedObject.Attach(DependencyObject dependencyObject) {
#else
        void IAttachedObject.Attach(FrameworkElement dependencyObject)
        {
#endif
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
            var owner = parameter.Owner;

            if (owner != null)
            {
                owner.UpdateAvailability();
            }
        }
    }
}
