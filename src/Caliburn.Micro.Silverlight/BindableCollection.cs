namespace Caliburn.Micro
{
#if !SILVERLIGHT
    using System;
#endif
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// Represents a collection that is observable.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
    public interface IObservableCollection<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="items">The items.</param>
        void AddRange(IEnumerable<T> items);
    }

    /// <summary>
    /// A base collection class that supports automatic UI thread marshalling.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class BindableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
    {
        private bool raiseCollectionChanged = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableCollection&lt;T&gt;"/> class.
        /// </summary>
        public BindableCollection() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="collection"/> parameter cannot be null.
        /// </exception>
        public BindableCollection(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        /// <summary>
        /// Inserts the item to the specified position.
        /// </summary>
        /// <param name="index">The index to insert at.</param>
        /// <param name="item">The item to be inserted.</param>
        protected override void InsertItem(int index, T item)
        {
            Execute.OnUIThread(() => InsertItemBase(index, item));
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="InsertItem"/> function.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <remarks>Used to avoid compiler warning regarding unverifiable code.</remarks>
        private void InsertItemBase(int index, T item)
        {
            base.InsertItem(index, item);
        }

#if NET
        /// <summary>
        /// Moves the item within the collection.
        /// </summary>
        /// <param name="oldIndex">The old position of the item.</param>
        /// <param name="newIndex">The new position of the item.</param>
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            Execute.OnUIThread(() => MoveItemBase(oldIndex, newIndex));
        }

        /// <summary>
        /// Exposes the base implementation fo the <see cref="MoveItem"/> function.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        /// <remarks>Used to avoid compiler warning regarding unverificable code.</remarks>
        private void MoveItemBase(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
        }
#endif

        /// <summary>
        /// Sets the item at the specified position.
        /// </summary>
        /// <param name="index">The index to set the item at.</param>
        /// <param name="item">The item to set.</param>
        protected override void SetItem(int index, T item)
        {
            Execute.OnUIThread(() => SetItemBase(index, item));
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="SetItem"/> function.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <remarks>Used to avoid compiler warning regarding unverifiable code.</remarks>
        private void SetItemBase(int index, T item)
        {
            base.SetItem(index, item);
        }

        /// <summary>
        /// Removes the item at the specified position.
        /// </summary>
        /// <param name="index">The position used to identify the item to remove.</param>
        protected override void RemoveItem(int index)
        {
            Execute.OnUIThread(() => RemoveItemBase(index));
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="RemoveItem"/> function.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <remarks>Used to avoid compiler warning regarding unverifiable code.</remarks>
        private void RemoveItemBase(int index)
        {
            base.RemoveItem(index);
        }

        /// <summary>
        /// Clears the items contained by the collection.
        /// </summary>
        protected override void ClearItems()
        {
            Execute.OnUIThread(ClearItemsBase);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="ClearItems"/> function.
        /// </summary>
        /// <remarks>Used to avoid compiler warning regarding unverifiable code.</remarks>
        private void ClearItemsBase()
        {
            base.ClearItems();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged"/> event with the provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (raiseCollectionChanged)
                base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddRange(IEnumerable<T> items)
        {
            raiseCollectionChanged = false;
            items.Apply(Add);
            raiseCollectionChanged = true;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}