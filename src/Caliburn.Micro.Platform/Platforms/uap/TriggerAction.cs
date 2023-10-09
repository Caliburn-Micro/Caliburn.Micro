using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;

namespace Caliburn.Micro {
    /// <summary>
    /// Represents an attachable object that encapsulates a unit of functionality.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public abstract class TriggerAction<T> : DependencyObject, IAction {
        /// <summary>
        /// The associated object property.
        /// </summary>
        public static readonly DependencyProperty AssociatedObjectProperty
            = DependencyProperty.Register(
                name: "AssociatedObject",
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(TriggerAction<T>),
                typeMetadata: new PropertyMetadata(null, OnAssociatedObjectChanged));

        /// <summary>
        /// Gets or sets the object to which this <see cref="TriggerAction{T}"/> is attached.
        /// </summary>
        public FrameworkElement AssociatedObject {
            get => (FrameworkElement)GetValue(AssociatedObjectProperty);

            set => SetValue(AssociatedObjectProperty, value);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="sender">The <see cref="object" /> that is passed to the action by the behavior. Generally this is <seealso cref="IBehavior.AssociatedObject" /> or a target object.</param>
        /// <param name="parameter">The value of this parameter is determined by the caller.</param>
        /// <returns>
        /// Returns the result of the action.
        /// </returns>
        public virtual object Execute(object sender, object parameter) {
            if (AssociatedObject == null && sender is FrameworkElement element) {
                AssociatedObject = element;
            }

            Invoke(parameter);

            return null;
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parmeter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected abstract void Invoke(object parmeter);

        /// <summary>
        ///  Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected virtual void OnAttached() {
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected virtual void OnDetaching() {
        }

        private static void OnAssociatedObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is not TriggerAction<T> triggerAction) {
                return;
            }

            if (e.OldValue != null) {
                triggerAction.OnDetaching();
            }

            if (e.NewValue != null) {
                triggerAction.OnAttached();
            }
        }
    }
}
