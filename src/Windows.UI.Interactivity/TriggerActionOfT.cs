using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents an attachable object that encapsulates a unit of functionality.
    /// 
    /// </summary>
    /// <typeparam name="T">The type to which this action can be attached.</typeparam>
    public abstract class TriggerAction<T> : TriggerAction where T : FrameworkElement
    {
        /// <summary>
        /// Gets the object to which this <see cref="T:System.Windows.Interactivity.TriggerAction`1"/> is attached.
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
        /// Gets the associated object type constraint.
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
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TriggerAction`1"/> class.
        /// 
        /// </summary>
        protected TriggerAction() : base(typeof(T))
        {
        }
    }
}
