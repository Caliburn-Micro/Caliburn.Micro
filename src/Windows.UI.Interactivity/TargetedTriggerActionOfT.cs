using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents an action that can be targeted to affect an object other than its AssociatedObject.
    /// 
    /// </summary>
    /// <typeparam name="T">The type constraint on the target.</typeparam>
    /// <remarks>
    /// TargetedTriggerAction extends TriggerAction to add knowledge of another element than the one it is attached to.
    ///             	This allows a user to invoke the action on an element other than the one it is attached to in response to a
    ///             	trigger firing. Override OnTargetChanged to hook or unhook handlers on the target element, and OnAttached/OnDetaching
    ///             	for the associated element. The type of the Target element can be constrained by the generic type parameter. If
    ///             	you need control over the type of the AssociatedObject, set a TypeConstraintAttribute on your derived type.
    /// 
    /// </remarks>
    public abstract class TargetedTriggerAction<T> : TargetedTriggerAction where T : class
    {
        /// <summary>
        /// Gets the target object. If TargetName is not set or cannot be resolved, defaults to the AssociatedObject.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The target.
        /// </value>
        /// 
        /// <remarks>
        /// In general, this property should be used in place of AssociatedObject in derived classes.
        /// </remarks>
        protected T Target
        {
            get
            {
                return (T)base.Target;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TargetedTriggerAction`1"/> class.
        /// 
        /// </summary>
        protected TargetedTriggerAction()
            : base(typeof(T))
        {
        }

        internal override sealed void OnTargetChangedImpl(object oldTarget, object newTarget)
        {
            base.OnTargetChangedImpl(oldTarget, newTarget);
            this.OnTargetChanged(oldTarget as T, newTarget as T);
        }

        /// <summary>
        /// Called when the target property changes.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Override this to hook and unhook functionality on the specified Target, rather than the AssociatedObject.
        /// </remarks>
        /// <param name="oldTarget">The old target.</param><param name="newTarget">The new target.</param>
        protected virtual void OnTargetChanged(T oldTarget, T newTarget)
        {
        }
    }
}
