namespace Caliburn.Micro {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// A base collection class that supports automatic UI thread marshalling.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
    public class BindableCollection<T> : ObservableCollection<T>, IObservableCollection<T> {
        /// <summary>
        /// Initializes a new instance of the <see cref = "Caliburn.Micro.BindableCollection&lt;T&gt;" /> class.
        /// </summary>
        public BindableCollection() {
            IsNotifying = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "Caliburn.Micro.BindableCollection&lt;T&gt;" /> class.
        /// </summary>
        /// <param name = "collection">The collection from which the elements are copied.</param>
        public BindableCollection(IEnumerable<T> collection)
            : base(collection) {
            IsNotifying = true;
        }

        /// <summary>
        /// Enables/Disables property change notification.
        /// </summary>
        public bool IsNotifying { get; set; }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
        public virtual void NotifyOfPropertyChange(string propertyName) {
            if (IsNotifying)
                OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
        }

        /// <summary>
        /// Raises a change notification indicating that all bindings should be refreshed.
        /// </summary>
        public void Refresh() {
            OnUIThread(() => {
                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        /// <summary>
        ///   Inserts the item to the specified position.
        /// </summary>
        /// <param name = "index">The index to insert at.</param>
        /// <param name = "item">The item to be inserted.</param>
        protected override sealed void InsertItem(int index, T item) {
            OnUIThread(() => InsertItemBase(index, item));
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "InsertItem" /> function.
        /// </summary>
        /// <param name = "index">The index.</param>
        /// <param name = "item">The item.</param>
        /// <remarks>
        ///   Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void InsertItemBase(int index, T item) {
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Sets the item at the specified position.
        /// </summary>
        /// <param name = "index">The index to set the item at.</param>
        /// <param name = "item">The item to set.</param>
        protected override sealed void SetItem(int index, T item) {
            OnUIThread(() => SetItemBase(index, item));
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "SetItem" /> function.
        /// </summary>
        /// <param name = "index">The index.</param>
        /// <param name = "item">The item.</param>
        /// <remarks>
        ///   Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void SetItemBase(int index, T item) {
            base.SetItem(index, item);
        }

        /// <summary>
        /// Removes the item at the specified position.
        /// </summary>
        /// <param name = "index">The position used to identify the item to remove.</param>
        protected override sealed void RemoveItem(int index) {
            Execute.OnUIThread(() => RemoveItemBase(index));
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "RemoveItem" /> function.
        /// </summary>
        /// <param name = "index">The index.</param>
        /// <remarks>
        ///   Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void RemoveItemBase(int index) {
            base.RemoveItem(index);
        }

        /// <summary>
        /// Clears the items contained by the collection.
        /// </summary>
        protected override sealed void ClearItems() {
            OnUIThread(ClearItemsBase);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref = "ClearItems" /> function.
        /// </summary>
        /// <remarks>
        ///   Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void ClearItemsBase() {
            base.ClearItems();
        }

        /// <summary>
        /// Raises the <see cref = "E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.
        /// </summary>
        /// <param name = "e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (IsNotifying) {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event with the provided arguments.
        /// </summary>
        /// <param name = "e">The event data to report in the event.</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (IsNotifying) {
                base.OnPropertyChanged(e);
            }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name = "items">The items.</param>
        public virtual void AddRange(IEnumerable<T> items) {
            OnUIThread(() => {
                var previousNotificationSetting = IsNotifying;
                IsNotifying = false;
                var index = Count;
                foreach (var item in items) {
                    InsertItemBase(index, item);
                    index++;
                }
                IsNotifying = previousNotificationSetting;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name = "items">The items.</param>
        public virtual void RemoveRange(IEnumerable<T> items) {
            OnUIThread(() => {
                var previousNotificationSetting = IsNotifying;
                IsNotifying = false;
                foreach (var item in items) {
                    var index = IndexOf(item);
                    if (index >= 0) {
                        RemoveItemBase(index);
                    }
                }
                IsNotifying = previousNotificationSetting;

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            });
        }

        /// <summary>
        /// Executes the given action on the UI thread
        /// </summary>
        /// <remarks>An extension point for subclasses to customise how property change notifications are handled.</remarks>
        /// <param name="action"></param>
        protected virtual void OnUIThread(System.Action action) => action.OnUIThread();
    }
}
