using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Xaml.Interactivity;

namespace Caliburn.Micro
{
    /// <summary>
    /// Represents a parameter of an <see cref="ActionMessage"/>.
    /// </summary>
    public class Parameter : AvaloniaObject, IBehavior
    {
        /// <summary>
        /// A dependency property representing the parameter's value.
        /// </summary>
        public static readonly AvaloniaProperty ValueProperty =
            AvaloniaProperty.Register<Parameter, object>("Value");

        AvaloniaObject associatedObject;
        WeakReference owner;

        static Parameter()
        {
            ValueProperty.Changed.Subscribe(OnValueChanged);
        }

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

        AvaloniaObject IBehavior.AssociatedObject
        {
            get
            {
                return associatedObject;
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        protected ActionMessage Owner
        {
            get { return owner == null ? null : owner.Target as ActionMessage; }
            set { owner = new WeakReference(value); }
        }

        void IBehavior.Attach(AvaloniaObject AvaloniaObject)
        {
            associatedObject = AvaloniaObject;
        }

        void IBehavior.Detach()
        {
            associatedObject = null;
        }

        /// <summary>
        /// Makes the parameter aware of the <see cref="ActionMessage"/> that it's attached to.
        /// </summary>
        /// <param name="actionMessageOwner">The action message.</param>
        internal void MakeAwareOf(ActionMessage actionMessageOwner)
        {
            Owner = actionMessageOwner;
        }

        static void OnValueChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var parameter = (Parameter)e.Sender;
            var owner = parameter.Owner;

            if (owner != null)
            {
                owner.UpdateAvailability();
            }
        }
    }
}
