using System.Linq;

using Windows.Foundation.Collections;
using Windows.UI.Xaml;

namespace Caliburn.Micro {
    /// <summary>
    /// A collection that can exist as part of a behavior.
    /// </summary>
    /// <typeparam name="T">The type of item in the attached collection.</typeparam>
    public class AttachedCollection<T> : DependencyObjectCollection, IAttachedObject
        where T : DependencyObject, IAttachedObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachedCollection{T}"/> class.
        /// </summary>
        public AttachedCollection()
            => VectorChanged += OnVectorChanged;

        /// <summary>
        /// Gets the currently attached object.
        /// </summary>
        public DependencyObject AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches the collection.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to attach the collection to.</param>
        public void Attach(DependencyObject dependencyObject) {
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
        protected virtual void OnItemAdded(DependencyObject item) {
            if (AssociatedObject == null ||
                item is not IAttachedObject @object) {
                return;
            }

            @object.Attach(AssociatedObject);
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The item that was removed.</param>
        protected virtual void OnItemRemoved(DependencyObject item) {
            if (item is not IAttachedObject @object ||
                @object.AssociatedObject == null) {
                return;
            }

            @object.Detach();
        }

        private void OnVectorChanged(IObservableVector<DependencyObject> sender, IVectorChangedEventArgs @event) {
            switch (@event.CollectionChange) {
                case CollectionChange.ItemInserted:
                    OnItemAdded(this[(int)@event.Index]);
                    break;
                case CollectionChange.ItemRemoved:
                    OnItemRemoved(this[(int)@event.Index]);
                    break;
                case CollectionChange.Reset:
                    this.Apply(OnItemRemoved);
                    this.Apply(OnItemAdded);
                    break;
            }
        }
    }
}
