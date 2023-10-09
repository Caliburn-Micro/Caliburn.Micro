using System;
using System.ComponentModel;
using System.Windows;

using Microsoft.Xaml.Behaviors;

namespace Caliburn.Micro {
    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
    public class Parameter : Freezable, IAttachedObject {
        /// <summary>
        /// A dependency property representing the parameter's value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(Parameter),
                new PropertyMetadata(OnValueChanged));

        private DependencyObject associatedObject;
        private WeakReference owner;

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>The value.</value>
        [Category("Common Properties")]
        public object Value {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        DependencyObject IAttachedObject.AssociatedObject {
            get {
                ReadPreamble();

                return associatedObject;
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        protected ActionMessage Owner {
            get => owner == null ? null : owner.Target as ActionMessage;
            set => owner = new WeakReference(value);
        }

        void IAttachedObject.Attach(DependencyObject dependencyObject) {
            WritePreamble();
            associatedObject = dependencyObject;
            WritePostscript();
        }

        void IAttachedObject.Detach() {
            WritePreamble();
            associatedObject = null;
            WritePostscript();
        }

        /// <summary>
        /// Makes the parameter aware of the <see cref="ActionMessage"/> that it's attached to.
        /// </summary>
        /// <param name="owner">The action message.</param>
        internal void MakeAwareOf(ActionMessage owner)
            => Owner = owner;

        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="Freezable"/> derived class.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore()
            => new Parameter();

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var parameter = (Parameter)d;
            ActionMessage owner = parameter.Owner;

            owner?.UpdateAvailability();
        }
    }
}
