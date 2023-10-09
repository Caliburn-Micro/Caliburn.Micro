using System;

#if MAUI
using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;
#else
using Windows.UI.Xaml;
#endif

#if MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
#if WINDOWS_UWP || MAUI
    public class Parameter : DependencyObject, IAttachedObject {
        /// <summary>
        /// A dependency property representing the parameter's value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Value",
                typeof(object),
                typeof(Parameter),
                null,
                OnValueChanged);

        private DependencyObject associatedObject;
#else
    public class Parameter : FrameworkElement, IAttachedObject {
        private FrameworkElement associatedObject;
#endif
        private WeakReference owner;

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>The value.</value>
        public object Value {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

#if WINDOWS_UWP || MAUI
        DependencyObject IAttachedObject.AssociatedObject
#else
        FrameworkElement IAttachedObject.AssociatedObject
#endif
            => associatedObject;

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        protected ActionMessage Owner {
            get => owner == null ? null : owner.Target as ActionMessage;
            set => owner = new WeakReference(value);
        }

#if WINDOWS_UWP || MAUI
        void IAttachedObject.Attach(DependencyObject dependencyObject)
#else
        void IAttachedObject.Attach(FrameworkElement dependencyObject)
#endif
             => associatedObject = dependencyObject;

        void IAttachedObject.Detach()
            => associatedObject = null;

        /// <summary>
        /// Makes the parameter aware of the <see cref="ActionMessage"/> that it's attached to.
        /// </summary>
        /// <param name="owner">The action message.</param>
        internal void MakeAwareOf(ActionMessage owner)
            => Owner = owner;

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var parameter = (Parameter)d;
            ActionMessage owner = parameter.Owner;
            owner?.UpdateAvailability();
        }
    }
}
