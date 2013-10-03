namespace Caliburn.Micro {
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// Represents a collection that is observable.
    /// </summary>
    /// <typeparam name = "T">The type of elements contained in the collection.</typeparam>
    public interface IObservableCollection<T> : IList<T>, INotifyPropertyChangedEx, INotifyCollectionChanged {
        /// <summary>
        ///   Adds the range.
        /// </summary>
        /// <param name = "items">The items.</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        ///   Removes the range.
        /// </summary>
        /// <param name = "items">The items.</param>
        void RemoveRange(IEnumerable<T> items);
    }
}
