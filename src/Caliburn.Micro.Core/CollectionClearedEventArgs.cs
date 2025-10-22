using System;
using System.Collections.Generic;

namespace Caliburn.Micro
{
    public class CollectionClearedEventArgs<T> : EventArgs
    {
        public CollectionClearedEventArgs(IReadOnlyCollection<T> clearedItems)
        {
            ClearedItems = clearedItems;
        }

        public IReadOnlyCollection<T> ClearedItems { get; }
    }
}
