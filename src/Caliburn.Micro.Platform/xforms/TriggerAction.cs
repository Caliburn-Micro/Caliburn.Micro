using System;
using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms
{
    public abstract class TriggerAction<T> : global::Xamarin.Forms.TriggerAction<T> 
        where T : BindableObject {

        private VisualElement associatedObject;

        /// <summary>
        /// Gets or sets the object to which this <see cref="TriggerAction&lt;T&gt;"/> is attached. 
        /// </summary>
        public VisualElement AssociatedObject
        {
            get { return associatedObject; }
            set
            {
                if (associatedObject == value)
                    return;

                OnDetaching();

                associatedObject = value;

                OnAttached();
            }
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
    }
}
