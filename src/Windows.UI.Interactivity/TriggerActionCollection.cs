using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents a collection of actions with a shared AssociatedObject and provides change notifications to its contents when that AssociatedObject changes.
    /// 
    /// </summary>
    public class TriggerActionCollection : AttachableCollection<TriggerAction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TriggerActionCollection"/> class.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Internal, because this should not be inherited outside this assembly.
        /// </remarks>
        internal TriggerActionCollection()
        {
        }

        /// <summary>
        /// Called immediately after the collection is attached to an AssociatedObject.
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            foreach (TriggerAction triggerAction in this)
            {
                triggerAction.Attach(this.AssociatedObject);
            }
        }

        /// <summary>
        /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
        /// 
        /// </summary>
        protected override void OnDetaching()
        {
            foreach (TriggerAction triggerAction in this)
            {
                triggerAction.Detach();
            }
        }

        /// <summary>
        /// Called when a new item is added to the collection.
        /// 
        /// </summary>
        /// <param name="item">The new item.</param>
        internal override void ItemAdded(TriggerAction item)
        {
            if (item.IsHosted)
            {
                throw new InvalidOperationException("Cannot Host TriggerAction Multiple Times");
            }
            if (this.AssociatedObject != null)
            {
                item.Attach(this.AssociatedObject);
            }
            item.IsHosted = true;
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// 
        /// </summary>
        /// <param name="item">The removed item.</param>
        internal override void ItemRemoved(TriggerAction item)
        {
            if (((IAttachedObject)item).AssociatedObject != null)
            {
                item.Detach();
            }
            item.IsHosted = false;
        }
    }
}
