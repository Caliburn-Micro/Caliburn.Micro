using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents an object that can invoke actions conditionally.
    /// 
    /// </summary>
    /// <typeparam name="T">The type to which this trigger can be attached.</typeparam>
    /// <remarks>
    /// TriggerBase is the base class for controlling actions. Override OnAttached() and
    ///             	OnDetaching() to hook and unhook handlers on the AssociatedObject. You may
    ///             	constrain the types that a derived TriggerBase may be attached to by specifying
    ///             	the generic parameter. Call InvokeActions() to fire all Actions associated with
    ///             	this TriggerBase.
    /// 
    /// </remarks>
    public abstract class TriggerBase<T> : TriggerBase where T : FrameworkElement
    {
        /// <summary>
        /// Gets the object to which the trigger is attached.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The associated object.
        /// </value>
        protected T AssociatedObject
        {
            get
            {
                return (T)base.AssociatedObject;
            }
        }

        /// <summary>
        /// Gets the type constraint of the associated object.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The associated object type constraint.
        /// </value>
        protected override sealed Type AssociatedObjectTypeConstraint
        {
            get
            {
                return base.AssociatedObjectTypeConstraint;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TriggerBase`1"/> class.
        /// 
        /// </summary>
        protected TriggerBase()
            : base(typeof(T))
        {
        }
    }
}
