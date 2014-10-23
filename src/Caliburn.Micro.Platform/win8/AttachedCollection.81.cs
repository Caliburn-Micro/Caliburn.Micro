namespace Caliburn.Micro {
    using System.Linq;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;

    /// <summary>
    /// A collection that can exist as part of a behavior.
    /// </summary>
    /// <typeparam name="T">The type of item in the attached collection.</typeparam>
    public class AttachedCollection<T> : DependencyObjectCollection, IAttachedObject
        where T : DependencyObject, IAttachedObject {
        private DependencyObject associatedObject;

        /// <summary>
        /// Creates an instance of <see cref="AttachedCollection&lt;T&gt;"/>
        /// </summary>
        public AttachedCollection() {
            VectorChanged += OnVectorChanged;
        }

        /// <summary>
        /// Attaches the collection.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to attach the collection to.</param>
        public void Attach(DependencyObject dependencyObject) {
            associatedObject = dependencyObject;
            this.OfType<IAttachedObject>().Apply(x => x.Attach(associatedObject));
        }

        /// <summary>
        /// Detaches the collection.
        /// </summary>
        public void Detach() {
            this.OfType<IAttachedObject>().Apply(x => x.Detach());
            associatedObject = null;
        }

        /// <summary>
        /// The currently attached object.
        /// </summary>
        public DependencyObject AssociatedObject {
            get { return associatedObject; }
        }

        /// <summary>
        /// Called when an item is added from the collection.
        /// </summary>
        /// <param name="item">The item that was added.</param>
        protected virtual void OnItemAdded(DependencyObject item) {
            if (associatedObject != null) {
                if (item is IAttachedObject)
                    ((IAttachedObject) item).Attach(associatedObject);
            }
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The item that was removed.</param>
        protected virtual void OnItemRemoved(DependencyObject item) {
            if (item is IAttachedObject) {
                if (((IAttachedObject) item).AssociatedObject != null)
                    ((IAttachedObject) item).Detach();
            }
        }

        private void OnVectorChanged(IObservableVector<DependencyObject> sender, IVectorChangedEventArgs @event) {
            switch (@event.CollectionChange) {
                case CollectionChange.ItemInserted:
                    OnItemAdded(this[(int) @event.Index]);
                    break;
                case CollectionChange.ItemRemoved:
                    OnItemRemoved(this[(int) @event.Index]);
                    break;
                case CollectionChange.Reset:
                    this.Apply(OnItemRemoved);
                    this.Apply(OnItemAdded);
                    break;
            }
        }
    }
}
