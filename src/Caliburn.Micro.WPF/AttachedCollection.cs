namespace Caliburn.Micro
{
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using System.Windows.Interactivity;

    public class AttachedCollection<T> : FreezableCollection<T>, IAttachedObject
        where T : DependencyObject, IAttachedObject
    {
        DependencyObject associatedObject;

        public AttachedCollection()
        {
            ((INotifyCollectionChanged)this).CollectionChanged += OnCollectionChanged;
        }

        public void Attach(DependencyObject dependencyObject)
        {
            WritePreamble();
            associatedObject = dependencyObject;
            WritePostscript();

            this.Apply(x => x.Attach(associatedObject));
        }

        public void Detach()
        {
            this.Apply(x => x.Detach());
            WritePreamble();
            associatedObject = null;
            WritePostscript();
        }

        DependencyObject IAttachedObject.AssociatedObject
        {
            get { return associatedObject; }
        }

        protected void OnItemAdded(T item)
        {
            if (associatedObject != null)
                item.Attach(associatedObject);
        }

        protected void OnItemRemoved(T item)
        {
            if(item.AssociatedObject != null)
                item.Detach();
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.OfType<T>().Where(x => !Contains(x)).Apply(OnItemAdded);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.OfType<T>().Apply(OnItemRemoved);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    e.OldItems.OfType<T>().Apply(OnItemRemoved);
                    e.NewItems.OfType<T>().Where(x => !Contains(x)).Apply(OnItemAdded);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.Apply(OnItemRemoved);
                    this.Apply(OnItemAdded);
                    break;
                default:
                    return;
            }
        }
    }
}