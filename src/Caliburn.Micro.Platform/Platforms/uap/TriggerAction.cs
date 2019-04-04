namespace Caliburn.Micro {
    using Microsoft.Xaml.Interactivity;
    using Windows.UI.Xaml;

    /// <summary>
    /// Represents an attachable object that encapsulates a unit of functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TriggerAction<T> : DependencyObject, IAction {
        /// <summary>
        /// The associated object property.
        /// </summary>
        public static readonly DependencyProperty AssociatedObjectProperty =
            DependencyProperty.Register("AssociatedObject", typeof (FrameworkElement), typeof (TriggerAction<T>),
                new PropertyMetadata(null, OnAssociatedObjectChanged));

        /// <summary>
        /// Gets or sets the object to which this <see cref="TriggerAction&lt;T&gt;"/> is attached. 
        /// </summary>
        public FrameworkElement AssociatedObject {
            get {
                return (FrameworkElement) GetValue(AssociatedObjectProperty);
            }
            set {
                SetValue(AssociatedObjectProperty, value);
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
        public virtual object Execute(object sender, object parameter) {

            if (AssociatedObject == null && sender is FrameworkElement)
            {
                AssociatedObject = (FrameworkElement) sender;
            }

            Invoke(parameter);
            return null;
        }

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
            var triggerAction = (TriggerAction<T>) d;

            if (triggerAction == null)
                return;

            if (e.OldValue != null)
                triggerAction.OnDetaching();

            if (e.NewValue != null)
                triggerAction.OnAttached();
        }
    }
}
