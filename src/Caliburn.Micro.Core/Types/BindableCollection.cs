using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Caliburn.Micro;

/// <summary>
/// A base collection class that supports automatic UI thread marshalling.
/// </summary>
/// <typeparam name="T">The type of elements contained in the collection.</typeparam>
public class BindableCollection<T> : ObservableCollection<T>, IObservableCollection<T> {
    /// <summary>
    /// Initializes a new instance of the <see cref = "BindableCollection&lt;T&gt;" /> class.
    /// </summary>
    public BindableCollection()
        => IsNotifying = true;

    /// <summary>
    /// Initializes a new instance of the <see cref = "BindableCollection&lt;T&gt;" /> class.
    /// </summary>
    /// <param name = "collection">The collection from which the elements are copied.</param>
    public BindableCollection(IEnumerable<T> collection)
        : base(collection)
        => IsNotifying = true;

    /// <summary>
    /// Gets or sets a value indicating whether to Enable/Disable property change notification.
    /// </summary>
    public bool IsNotifying { get; set; }

    /// <summary>
    /// Notifies subscribers of the property change.
    /// </summary>
    /// <param name = "propertyName">Name of the property.</param>
    public virtual void NotifyOfPropertyChange(string propertyName) {
        if (!IsNotifying) {
            return;
        }

        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

            return;
        }

        OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
    }

    /// <summary>
    /// Raises a change notification indicating that all bindings should be refreshed.
    /// </summary>
    public void Refresh() {
        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            return;
        }

        OnUIThread(() => {
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        });
    }

    /// <summary>
    /// Adds the range.
    /// </summary>
    /// <param name = "items">The items.</param>
    public virtual void AddRange(IEnumerable<T> items) {
        void AddRange() {
            bool previousNotificationSetting = IsNotifying;
            IsNotifying = false;
            int index = Count;
            foreach (T item in items) {
                InsertItemBase(index, item);
                index++;
            }

            IsNotifying = previousNotificationSetting;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            AddRange();

            return;
        }

        OnUIThread(AddRange);
    }

    /// <summary>
    /// Removes the range.
    /// </summary>
    /// <param name = "items">The items.</param>
    public virtual void RemoveRange(IEnumerable<T> items) {
        void RemoveRange() {
            bool previousNotificationSetting = IsNotifying;
            IsNotifying = false;

            items.Select(IndexOf)
                 .Where(index => index >= 0)
                 .Apply(RemoveItemBase);

            IsNotifying = previousNotificationSetting;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            RemoveRange();

            return;
        }

        OnUIThread(RemoveRange);
    }

    /// <summary>
    /// Executes the given action on the UI thread.
    /// </summary>
    /// <remarks>An extension point for subclasses to customise how property change notifications are handled.</remarks>
    protected virtual void OnUIThread(System.Action action)
        => action.OnUIThread();

    /// <summary>
    /// Inserts the item to the specified position.
    /// </summary>
    /// <param name = "index">The index to insert at.</param>
    /// <param name = "item">The item to be inserted.</param>
    protected sealed override void InsertItem(int index, T item) {
        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            InsertItemBase(index, item);

            return;
        }

        OnUIThread(() => InsertItemBase(index, item));
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
        => base.InsertItem(index, item);

    /// <summary>
    /// Sets the item at the specified position.
    /// </summary>
    /// <param name = "index">The index to set the item at.</param>
    /// <param name = "item">The item to set.</param>
    protected sealed override void SetItem(int index, T item) {
        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            SetItemBase(index, item);

            return;
        }

        OnUIThread(() => SetItemBase(index, item));
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
        => base.SetItem(index, item);

    /// <summary>
    /// Removes the item at the specified position.
    /// </summary>
    /// <param name = "index">The position used to identify the item to remove.</param>
    protected sealed override void RemoveItem(int index) {
        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            RemoveItemBase(index);

            return;
        }

        OnUIThread(() => RemoveItemBase(index));
    }

    /// <summary>
    /// Exposes the base implementation of the <see cref = "RemoveItem" /> function.
    /// </summary>
    /// <param name = "index">The index.</param>
    /// <remarks>
    ///   Used to avoid compiler warning regarding unverifiable code.
    /// </remarks>
    protected virtual void RemoveItemBase(int index)
        => base.RemoveItem(index);

    /// <summary>
    /// Clears the items contained by the collection.
    /// </summary>
    protected sealed override void ClearItems()
        => OnUIThread(ClearItemsBase);

    /// <summary>
    /// Exposes the base implementation of the <see cref = "ClearItems" /> function.
    /// </summary>
    /// <remarks>
    ///   Used to avoid compiler warning regarding unverifiable code.
    /// </remarks>
    protected virtual void ClearItemsBase()
        => base.ClearItems();

    /// <summary>
    /// Raises the <see cref = "System.Collections.ObjectModel.ObservableCollection{T}.CollectionChanged" /> event with the provided arguments.
    /// </summary>
    /// <param name = "e">Arguments of the event being raised.</param>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
        if (!IsNotifying) {
            return;
        }

        base.OnCollectionChanged(e);
    }

    /// <summary>
    /// Raises the PropertyChanged event with the provided arguments.
    /// </summary>
    /// <param name = "e">The event data to report in the event.</param>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
        if (!IsNotifying) {
            return;
        }

        base.OnPropertyChanged(e);
    }
}
