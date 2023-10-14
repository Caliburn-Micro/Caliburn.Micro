using System.Collections.Specialized;
using System.Linq;

using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms {
    /// <summary>
    /// A collection that can exist as part of a behavior.
    /// </summary>
    /// <typeparam name="T">The type of item in the attached collection.</typeparam>
    public class AttachedCollection<T> : BindableCollection<BindableObject>, IAttachedObject
        where T : BindableObject, IAttachedObject {
        /// <summary>
        /// Gets the currently attached object.
        /// </summary>
        public BindableObject AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches the collection.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to attach the collection to.</param>
        public void Attach(BindableObject dependencyObject) {
            AssociatedObject = dependencyObject;
            this.OfType<IAttachedObject>().Apply(x => x.Attach(AssociatedObject));
        }

        /// <summary>
        /// Detaches the collection.
        /// </summary>
        public void Detach() {
            this.OfType<IAttachedObject>().Apply(x => x.Detach());
            AssociatedObject = null;
        }

        /// <summary>
        /// Called when an item is added from the collection.
        /// </summary>
        /// <param name="item">The item that was added.</param>
        protected virtual void OnItemAdded(BindableObject item) {
            if (AssociatedObject == null ||
                !(item is IAttachedObject attachedObject)) {
                return;
            }

            attachedObject.Attach(AssociatedObject);
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The item that was removed.</param>
        protected virtual void OnItemRemoved(BindableObject item) {
            if (!(item is IAttachedObject attachedObject) ||
                attachedObject.AssociatedObject == null) {
                return;
            }

            attachedObject.Detach();
        }

        /// <summary>
        /// Raises the <see cref = "System.Collections.ObjectModel.ObservableCollection{T}.CollectionChanged" /> event with the provided arguments.
        /// </summary>
        /// <param name = "e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);

            e.OldItems?.OfType<BindableObject>().Apply(OnItemRemoved);
            e.NewItems?.OfType<BindableObject>().Apply(OnItemAdded);
        }
    }
}
