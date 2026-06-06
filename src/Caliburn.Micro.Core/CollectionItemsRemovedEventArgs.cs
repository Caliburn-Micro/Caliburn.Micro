using System;
using System.Collections.Generic;

namespace Caliburn.Micro;

/// <summary>
/// EventArgs sent after items have been removed from a collection.
/// </summary>
/// <typeparam name="T">The type of elements contained in the collection.</typeparam>
public class CollectionItemsRemovedEventArgs<T> : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionItemsRemovedEventArgs{T}"/> class.
    /// </summary>
    /// <param name="removedItems">The items that were removed from the collection.</param>
    public CollectionItemsRemovedEventArgs(IReadOnlyCollection<T> removedItems)
    {
        RemovedItems = removedItems ?? throw new ArgumentNullException(nameof(removedItems));
    }

    /// <summary>
    /// Gets the items that were removed from the collection.
    /// </summary>
    public IReadOnlyCollection<T> RemovedItems { get; }
}
