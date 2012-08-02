using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security;
using System.Text;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Implements a collection of dependency objects
    /// </summary>
    /// <typeparam name="T">Type of the DependencyObject contained</typeparam>
    public class DependencyObjectCollection<T> : FrameworkElement, IList<T>, ICollection<T>, IEnumerable<T>, INotifyCollectionChanged where T : FrameworkElement
    {
        private static readonly DependencyProperty CollectionProperty = DependencyProperty.Register("DOCollection", typeof(ObservableCollection<T>), typeof(DependencyObjectCollection<T>), null);
    
        /// <summary>
        /// Hosts the collection
        /// </summary>
        private ObservableCollection<T> items;

        /// <summary>
        /// Initializes the DependencyObjectCollection class
        /// </summary>
        public DependencyObjectCollection()
        {
            this.items = new ObservableCollection<T>();
            this.SetValue(DependencyObjectCollection<T>.CollectionProperty, this.items);
        }

        #region Various Interfaces
        
        /// <summary>
        /// Add an item to the collection
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item)
        {
            this.items.Add(item);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Verifies if the collection contains the instance of an element
        /// </summary>
        /// <param name="item">Element to check</param>
        /// <returns>true if the element is contained in the collection, otherwise false</returns>
        public bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        /// <summary>
        /// Copies an array to this instance
        /// </summary>
        /// <param name="array">Array to copy from</param>
        /// <param name="arrayIndex">Index to start from</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Number of items in the collection
        /// </summary>
        public int Count
        {
            get { return this.items.Count; }
        }

        /// <summary>
        /// Returns true if this collection cannot be modified
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes an instance from the collection
        /// </summary>
        /// <param name="item">Instance of the item to remove</param>
        /// <returns>True if the item was successfully removed, otherwise false</returns>
        public bool Remove(T item)
        {
            return this.items.Remove(item);
        }

        /// <summary>
        /// Retrieve an enumerator for this collection
        /// </summary>
        /// <returns>Instance of the enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Retrieve an enumerator for this collection
        /// </summary>
        /// <returns>Instance of the enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Returns the position of an element in the collection
        /// </summary>
        /// <param name="item">Item to search for</param>
        /// <returns>Index of the position where the item is found</returns>
        public int IndexOf(T item)
        {
            return this.items.IndexOf(item);
        }

        /// <summary>
        /// Inster an item into the collection
        /// </summary>
        /// <param name="index">index of the position to insert the item</param>
        /// <param name="item">item to insert</param>
        public void Insert(int index, T item)
        {
            this.items.Insert(index, item);
        }

        /// <summary>
        /// Removes an item at the specified position
        /// </summary>
        /// <param name="index">Index of the item to be removed</param>
        public void RemoveAt(int index)
        {
            this.items.RemoveAt(index);
        }

        /// <summary>
        /// Retrieve an item at the given position
        /// </summary>
        /// <param name="index">Index of the item</param>
        /// <returns>Instance of the item from the collection</returns>
        public T this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        /// <summary>
        /// Notifies changes in the collection
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { this.items.CollectionChanged += value; }
            remove { this.items.CollectionChanged -= value; }
        }

        #endregion
    }
}