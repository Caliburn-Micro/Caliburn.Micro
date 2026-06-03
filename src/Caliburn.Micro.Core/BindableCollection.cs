using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Caliburn.Micro
{
    /// <summary>
    /// A base collection class that supports automatic UI thread marshalling.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
    public class BindableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "BindableCollection&lt;T&gt;" /> class.
        /// </summary>
        public BindableCollection()
        {
            IsNotifying = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "BindableCollection&lt;T&gt;" /> class.
        /// </summary>
        /// <param name = "collection">The collection from which the elements are copied.</param>
        public BindableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            IsNotifying = true;
        }

        /// <summary>
        /// Enables/Disables property change notification.
        /// </summary>
        public bool IsNotifying { get; set; }

        /// <summary>
        /// Occurs after items have been removed from the collection without
        /// being included in the standard collection reset notification.
        /// </summary>
        /// <remarks>
        /// This event is raised by <c>Clear()</c> and <see cref="RemoveRange"/>
        /// after the corresponding <see cref="ObservableCollection{T}.CollectionChanged"/>
        /// reset notification has been raised on the same thread. Single-item
        /// removals are reported by the standard collection change notification.
        /// </remarks>
        public event EventHandler<CollectionItemsRemovedEventArgs<T>> CollectionItemsRemoved;

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
        public virtual void NotifyOfPropertyChange(string propertyName)
        {
            if (IsNotifying)
            {
                if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
                {
                    OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
                }
                else
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        /// <summary>
        /// Raises a change notification indicating that all bindings should be refreshed.
        /// </summary>
        public void Refresh()
        {
            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(() =>
                {
                    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                    OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                });
            }
            else
            {
                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Inserts the item to the specified position.
        /// </summary>
        /// <param name = "index">The index to insert at.</param>
        /// <param name = "item">The item to be inserted.</param>
        protected override sealed void InsertItem(int index, T item)
        {
            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(() => InsertItemBase(index, item));
            }
            else
            {
                InsertItemBase(index, item);
            }
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "InsertItem" /> function.
        /// </summary>
        /// <param name = "index">The index.</param>
        /// <param name = "item">The item.</param>
        /// <remarks>
        /// Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void InsertItemBase(int index, T item)
        {
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Sets the item at the specified position.
        /// </summary>
        /// <param name = "index">The index to set the item at.</param>
        /// <param name = "item">The item to set.</param>
        protected override sealed void SetItem(int index, T item)
        {
            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(() => SetItemBase(index, item));
            }
            else
            {
                SetItemBase(index, item);
            }
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "SetItem" /> function.
        /// </summary>
        /// <param name = "index">The index.</param>
        /// <param name = "item">The item.</param>
        /// <remarks>
        /// Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void SetItemBase(int index, T item)
        {
            base.SetItem(index, item);
        }

        /// <summary>
        /// Removes the item at the specified position.
        /// </summary>
        /// <param name = "index">The position used to identify the item to remove.</param>
        protected override sealed void RemoveItem(int index)
        {
            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(() => RemoveItemBase(index));
            }
            else
            {
                RemoveItemBase(index);
            }
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "RemoveItem" /> function.
        /// </summary>
        /// <param name = "index">The index.</param>
        /// <remarks>
        ///   Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void RemoveItemBase(int index)
        {
            base.RemoveItem(index);
        }

        /// <summary>
        /// Clears the items contained by the collection.
        /// </summary>
        protected override sealed void ClearItems()
        {
            OnUIThread(ClearItemsBase);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "ClearItems" /> function.
        /// </summary>
        /// <remarks>
        ///   Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void ClearItemsBase()
        {
            var collectionItemsRemoved = CollectionItemsRemoved;
            var removedItems = IsNotifying && collectionItemsRemoved != null && Count > 0
                ? new List<T>(collection: this)
                : null;

            base.ClearItems();

            if (removedItems != null)
            {
                collectionItemsRemoved(this, new CollectionItemsRemovedEventArgs<T>(removedItems));
            }
        }

        /// <summary>
        /// Raises the <see cref = "E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.
        /// </summary>
        /// <param name = "e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (IsNotifying)
            {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event with the provided arguments.
        /// </summary>
        /// <param name = "e">The event data to report in the event.</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (IsNotifying)
            {
                base.OnPropertyChanged(e);
            }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name = "items">The items.</param>
        public virtual void AddRange(IEnumerable<T> items)
        {
            void AddRange()
            {
                var previousNotificationSetting = IsNotifying;
                IsNotifying = false;
                var index = Count;
                foreach (var item in items)
                {
                    InsertItemBase(index, item);
                    index++;
                }
                IsNotifying = previousNotificationSetting;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(AddRange);
            }
            else
            {
                AddRange();
            }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name = "items">The items.</param>
        public virtual void RemoveRange(IEnumerable<T> items)
        {
            void RemoveRange()
            {
                var previousNotificationSetting = IsNotifying;
                var collectionItemsRemoved = CollectionItemsRemoved;
                var removedItems = previousNotificationSetting && collectionItemsRemoved != null
                    ? new List<T>()
                    : null;

                IsNotifying = false;
                foreach (var item in items)
                {
                    var index = IndexOf(item);
                    if (index >= 0)
                    {
                        removedItems?.Add(item);
                        RemoveItemBase(index);
                    }
                }
                IsNotifying = previousNotificationSetting;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                if (removedItems?.Count > 0)
                {
                    collectionItemsRemoved(this, new CollectionItemsRemovedEventArgs<T>(removedItems));
                }
            }

            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(RemoveRange);
            }
            else
            {
                RemoveRange();
            }
        }

        /// <summary>
        /// Executes the given action on the UI thread
        /// </summary>
        /// <remarks>An extension point for subclasses to customise how property change notifications are handled.</remarks>
        /// <param name="action"></param>
        protected virtual void OnUIThread(System.Action action) => action.OnUIThread();
    }
}
