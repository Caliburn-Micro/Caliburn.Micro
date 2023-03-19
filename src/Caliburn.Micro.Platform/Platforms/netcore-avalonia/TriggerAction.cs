using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace Caliburn.Micro
{
    public abstract class TriggerAction<T> : TriggerAction
    {
    }

    /// <summary>
    /// Represents an attachable object that encapsulates a unit of functionality.
    /// </summary>
    public abstract class TriggerAction : AvaloniaObject, IAction
    {
        private Control _associatedObject;
        /*
        /// <summary>
        /// The associated object property.
        /// </summary>
        public static readonly AvaloniaProperty AssociatedObjectProperty =
            AvaloniaProperty.Register<TriggerAction<T>, Control>("AssociatedObject");

        static TriggerAction()
        {
            AssociatedObjectProperty.Changed.Subscribe(OnAssociatedObjectChanged);
        }
        
        /// <summary>
        /// Gets or sets the object to which this <see cref="TriggerAction&lt;T&gt;"/> is attached. 
        /// </summary>
        public Control AssociatedObject
        {
            get
            {
                return (Control)GetValue(AssociatedObjectProperty);
            }
            set
            {
                SetValue(AssociatedObjectProperty, value);
            }
        }
        */

        public Control AssociatedObject
        {
            get => _associatedObject;
            set
            {
                if (_associatedObject == value)
                    return;

                if (_associatedObject != null)
                    OnDetaching();
                _associatedObject = value;
                if (_associatedObject != null)
                    OnAttached();
            }
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parmeter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected abstract void Invoke(object parmeter);

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="sender">The <see cref="T:System.Object" /> that is passed to the action by the behavior. Generally this is <seealso cref="P:Microsoft.Xaml.Interactivity.IBehavior.AssociatedObject" /> or a target object.</param>
        /// <param name="parameter">The value of this parameter is determined by the caller.</param>
        /// <returns>
        /// Returns the result of the action.
        /// </returns>
        public virtual object Execute(object sender, object parameter)
        {

            if (AssociatedObject == null && sender is Control)
            {
                AssociatedObject = (Control)sender;
            }

            Invoke(parameter);
            return null;
        }

        /// <summary>
        ///  Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected virtual void OnDetaching()
        {
        }
        /*
        private static void OnAssociatedObjectChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var triggerAction = e.Sender as TriggerAction<T>;

            if (triggerAction == null)
                return;

            if (e.OldValue != null)
                triggerAction.OnDetaching();

            if (e.NewValue != null)
                triggerAction.OnAttached();
        }
        */
    }
}
