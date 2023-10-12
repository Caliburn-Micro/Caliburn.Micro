using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms {
    /// <summary>
    /// Helper class to try and abtract the differences in TriggerAction across platforms.
    /// </summary>
    /// <typeparam name="T">The bindable object.</typeparam>
    public abstract class TriggerActionBase<T> : TriggerAction<T>
        where T : BindableObject {
        private VisualElement _associatedObject;

        /// <summary>
        /// Gets or sets the object to which this <see cref="TriggerActionBase{T}"/> is attached.
        /// </summary>
        public VisualElement AssociatedObject {
            get => _associatedObject;
            set {
                if (_associatedObject == value) {
                    return;
                }

                OnDetaching();
                _associatedObject = value;
                OnAttached();
            }
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
    }
}
