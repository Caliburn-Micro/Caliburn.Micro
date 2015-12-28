namespace Caliburn.Micro.Xamarin.Forms
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using global::Xamarin.Forms;

    /// <summary>
    /// A collection that can exist as part of a behavior.
    /// </summary>
    /// <typeparam name="T">The type of item in the attached collection.</typeparam>
    public class AttachedCollection<T> : BindableCollection<BindableObject>, IAttachedObject
        where T : BindableObject, IAttachedObject 
    {
        private BindableObject associatedObject;

        /// <summary>
        /// Attaches the collection.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to attach the collection to.</param>
        public void Attach(BindableObject dependencyObject)
        {
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
        public BindableObject AssociatedObject {
            get { return associatedObject; }
        }

        /// <summary>
        /// Called when an item is added from the collection.
        /// </summary>
        /// <param name="item">The item that was added.</param>
        protected virtual void OnItemAdded(BindableObject item) {
            if (associatedObject != null) {
                if (item is IAttachedObject)
                    ((IAttachedObject) item).Attach(associatedObject);
            }
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The item that was removed.</param>
        protected virtual void OnItemRemoved(BindableObject item) {
            if (item is IAttachedObject) {
                if (((IAttachedObject) item).AssociatedObject != null)
                    ((IAttachedObject) item).Detach();
            }
        }

        /// <summary>
        /// Raises the <see cref = "E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.
        /// </summary>
        /// <param name = "e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);

            if (e.OldItems != null)
                e.OldItems.OfType<BindableObject>().Apply(OnItemRemoved);

            if (e.NewItems != null)
                e.NewItems.OfType<BindableObject>().Apply(OnItemAdded);
        }
    }
}
