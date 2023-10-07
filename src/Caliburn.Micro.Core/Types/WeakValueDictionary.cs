using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Caliburn.Micro
{
    /// <summary>
    /// A dictionary in which the values are weak references.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    internal class WeakValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TValue : class
    {
        private readonly Dictionary<TKey, WeakReference> _inner;
        private readonly WeakReference _gcSentinel = new(new object());

        private bool IsCleanupNeeded()
        {
            if (_gcSentinel.Target == null)
            {
                _gcSentinel.Target = new object();

                return true;
            }

            return false;
        }

        private void CleanAbandonedItems()
        {
            var keysToRemove = _inner.Where(pair => !pair.Value.IsAlive)
                .Select(pair => pair.Key)
                .ToList();

            keysToRemove.Apply(key => _inner.Remove(key));
        }

        private void CleanIfNeeded()
        {
            if (IsCleanupNeeded())
            {
                CleanAbandonedItems();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public WeakValueDictionary()
            => _inner = new Dictionary<TKey, WeakReference>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> class that contains elements copied from the specified <see cref="IDictionary&lt;TKey, TValue&gt;"/> and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary&lt;TKey, TValue&gt;"/> whose elements are copied to the new <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.</param>
        public WeakValueDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _inner = new Dictionary<TKey, WeakReference>();
            dictionary.Apply(item => _inner.Add(item.Key, new WeakReference(item.Value)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> class that contains elements copied from the specified <see cref="IDictionary&lt;TKey, TValue&gt;"/> and uses the specified <see cref="IEqualityComparer&lt;T&gt;"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary&lt;TKey, TValue&gt;"/> whose elements are copied to the new <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer&lt;T&gt;"/> implementation to use when comparing keys, or null to use the default <see cref="EqualityComparer&lt;T&gt;"/> for the type of the key.</param>
        public WeakValueDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _inner = new Dictionary<TKey, WeakReference>(comparer);
            dictionary.Apply(item => _inner.Add(item.Key, new WeakReference(item.Value)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> class that is empty, has the default initial capacity, and uses the specified <see cref="IEqualityComparer&lt;T&gt;"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer&lt;T&gt;"/> implementation to use when comparing keys, or null to use the default <see cref="EqualityComparer&lt;T&gt;"/> for the type of the key.</param>
        public WeakValueDictionary(IEqualityComparer<TKey> comparer)
            => _inner = new Dictionary<TKey, WeakReference>(comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> can contain.</param>
        public WeakValueDictionary(int capacity)
            => _inner = new Dictionary<TKey, WeakReference>(capacity);

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="IEqualityComparer&lt;T&gt;"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> can contain.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer&lt;T&gt;"/> implementation to use when comparing keys, or null to use the default <see cref="EqualityComparer&lt;T&gt;"/> for the type of the key.</param>
        public WeakValueDictionary(int capacity, IEqualityComparer<TKey> comparer)
            => _inner = new Dictionary<TKey, WeakReference>(capacity, comparer);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            CleanIfNeeded();
            IEnumerable<KeyValuePair<TKey, TValue>> enumerable
                = _inner
                    .Select(pair => new KeyValuePair<TKey, TValue>(pair.Key, (TValue)pair.Value.Target))
                    .Where(pair => pair.Value != null);
            return enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
            => Add(item.Key, item.Value);

        /// <summary>
        /// Removes all keys and values from the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.
        /// </summary>
        public void Clear()
            => _inner.Clear();

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (!TryGetValue(item.Key, out TValue value))
            {
                return false;
            }

            return value == item.Value;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if ((arrayIndex + Count) > array.Length)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            }

            this.ToArray().CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!TryGetValue(item.Key, out TValue value))
            {
                return false;
            }

            if (value != item.Value)
            {
                return false;
            }

            return _inner.Remove(item.Key);
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.
        /// </summary>
        /// <remarks>
        /// Since the items in the dictionary are held by weak reference, the count value
        /// cannot be relied upon to guarantee the number of objects that would be discovered via
        /// enumeration. Treat the Count as an estimate only.
        /// </remarks>
        public int Count
        {
            get
            {
                CleanIfNeeded();

                return _inner.Count;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
            => false;

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        public void Add(TKey key, TValue value)
        {
            CleanIfNeeded();
            _inner.Add(key, new WeakReference(value));
        }

        /// <summary>
        /// Determines whether the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
            => TryGetValue(key, out _);

        /// <summary>
        /// Removes the value with the specified key from the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if key is not found in the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.</returns>
        public bool Remove(TKey key)
        {
            CleanIfNeeded();

            return _inner.Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified key, 
        /// if the key is found; otherwise, the default value for the type of the value parameter.
        /// This parameter is passed uninitialized.</param>
        /// <returns>true if the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/> contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            CleanIfNeeded();

            if (!_inner.TryGetValue(key, out WeakReference wr))
            {
                value = null;

                return false;
            }

            var result = (TValue)wr.Target;
            if (result == null)
            {
                _inner.Remove(key);
                value = null;

                return false;
            }

            value = result;

            return true;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="KeyNotFoundException"/>, 
        /// and a set operation creates a new element with the specified key.
        /// </returns>
        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out TValue result))
                {
                    throw new KeyNotFoundException();
                }

                return result;
            }
            set
            {
                CleanIfNeeded();
                _inner[key] = new WeakReference(value);
            }
        }

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.
        /// </summary>
        public ICollection<TKey> Keys
            => _inner.Keys;

        /// <summary>
        /// Gets a collection containing the values in the <see cref="WeakValueDictionary&lt;TKey, TValue&gt;"/>.
        /// </summary>
        public ICollection<TValue> Values
            => new ValueCollection(this);

        private sealed class ValueCollection : ICollection<TValue>
        {
            private readonly WeakValueDictionary<TKey, TValue> _inner;

            public ValueCollection(WeakValueDictionary<TKey, TValue> dictionary)
                => _inner = dictionary;

            public IEnumerator<TValue> GetEnumerator()
                => _inner.Select(pair => pair.Value).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            void ICollection<TValue>.Add(TValue item)
                => throw new NotSupportedException();

            void ICollection<TValue>.Clear()
                => throw new NotSupportedException();

            bool ICollection<TValue>.Contains(TValue item)
                => _inner.Any(pair => pair.Value == item);

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }

                if (arrayIndex < 0 || arrayIndex >= array.Length)
                {
                    throw new ArgumentOutOfRangeException("arrayIndex");
                }

                if ((arrayIndex + Count) > array.Length)
                {
                    throw new ArgumentException(
                        "The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
                }

                this.ToArray().CopyTo(array, arrayIndex);
            }

            bool ICollection<TValue>.Remove(TValue item)
                => throw new NotSupportedException();

            public int Count
                => _inner.Count;

            bool ICollection<TValue>.IsReadOnly
                => true;
        }
    }
}
