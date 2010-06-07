namespace Caliburn.Micro
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using System.Windows.Interactivity;

    public class AttachedCollection<T> : ObservableCollection<T>, IAttachedObject
        where T : DependencyObject, IAttachedObject
    {
        DependencyObject associatedObject;

        public AttachedCollection()
        {
            CollectionChanged += OnCollectionChanged;
        }

        protected DependencyObject AssociatedObject
        {
            get { return associatedObject; }
        }

        public void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject == AssociatedObject)
                return;

            if (AssociatedObject != null)
                throw new InvalidOperationException();

            if ((Application.Current == null || Application.Current.RootVisual == null) || !Bootstrapper.IsInDesignMode)
                associatedObject = dependencyObject;

            OnAttached();
        }

        public void Detach()
        {
            OnDetaching();
            associatedObject = null;
        }

        DependencyObject IAttachedObject.AssociatedObject
        {
            get { return AssociatedObject; }
        }

        protected void OnItemAdded(T item)
        {
            if (AssociatedObject != null)
                item.Attach(AssociatedObject);
        }

        protected void OnItemRemoved(T item)
        {
            if (item.AssociatedObject != null)
                item.Detach();
        }

        protected void OnAttached()
        {
            this.Apply(x => x.Attach(AssociatedObject));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
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

        protected void OnDetaching()
        {
            this.Apply(x => x.Detach());
        }
    }
}