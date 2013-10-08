using System;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;

namespace Caliburn.Micro
{
    public class AttachedCollection<T> : DependencyObjectCollection, IAttachedObject
         where T : DependencyObject, IAttachedObject
    {
        private DependencyObject associatedObject;

        public AttachedCollection()
        {
            VectorChanged += OnVectorChanged;
        }

        public void Attach(DependencyObject dependencyObject)
        {
            associatedObject = dependencyObject;
            this.OfType<IAttachedObject>().Apply(x => x.Attach(associatedObject));
        }

        public void Detach()
        {
            this.OfType<IAttachedObject>().Apply(x => x.Detach());
            associatedObject = null;
        }

        public DependencyObject AssociatedObject
        {
            get { return associatedObject; }
        }

        /// <summary>
        /// Called when an item is added from the collection.
        /// </summary>
        /// <param name="item">The item that was added.</param>
        protected void OnItemAdded(DependencyObject item)
        {
            if (associatedObject != null)
            {
                if (item is IAttachedObject)
                    ((IAttachedObject)item).Attach(associatedObject);
            }
        }

        /// <summary>
        /// Called when an item is removed from the collection.
        /// </summary>
        /// <param name="item">The item that was removed.</param>
        protected void OnItemRemoved(DependencyObject item)
        {
            if (item is IAttachedObject)
            {
                if (((IAttachedObject)item).AssociatedObject != null)
                    ((IAttachedObject) item).Detach();
            }
        }

        private void OnVectorChanged(IObservableVector<DependencyObject> sender, IVectorChangedEventArgs @event)
        {
            switch (@event.CollectionChange)
            {
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
