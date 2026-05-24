using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class BindableCollectionTests
    {
        [Fact]
        public void Clear_ThenCollectionItemsRemovedIsRaisedWithAllItems()
        {
            var objects = new[] { new object(), new object(), new object() };
            var bindableCollection = new BindableCollection<object>(objects);

            var collectionItemsRemovedEvent = Assert.Raises<CollectionItemsRemovedEventArgs<object>>(
                h => bindableCollection.CollectionItemsRemoved += h, h => bindableCollection.CollectionItemsRemoved -= h,
                () => bindableCollection.Clear()
            );

            Assert.Equal(objects, collectionItemsRemovedEvent.Arguments.RemovedItems);
        }

        [Fact]
        public void RemoveRange_ThenCollectionItemsRemovedIsRaisedWithRemovedItems()
        {
            var objects = new[] { new object(), new object(), new object() };
            var bindableCollection = new BindableCollection<object>(objects);

            var collectionItemsRemovedEvent = Assert.Raises<CollectionItemsRemovedEventArgs<object>>(
                h => bindableCollection.CollectionItemsRemoved += h, h => bindableCollection.CollectionItemsRemoved -= h,
                () => bindableCollection.RemoveRange(new[] { objects[0], objects[2] })
            );

            Assert.Equal(new[] { objects[0], objects[2] }, collectionItemsRemovedEvent.Arguments.RemovedItems);
        }

        [Fact]
        public void Clear_WhenNotNotifying_ThenCollectionItemsRemovedIsNotRaised()
        {
            var bindableCollection = new BindableCollection<object>(new[] { new object() })
            {
                IsNotifying = false
            };

            var raised = false;
            bindableCollection.CollectionItemsRemoved += (s, e) => raised = true;

            bindableCollection.Clear();

            Assert.False(raised);
        }

        [Fact]
        public void Clear_WhenCollectionChangedDisablesNotifying_ThenCollectionItemsRemovedIsStillRaised()
        {
            var item = new object();
            var bindableCollection = new BindableCollection<object>(new[] { item });

            bindableCollection.CollectionChanged += (s, e) => bindableCollection.IsNotifying = false;

            var collectionItemsRemovedEvent = Assert.Raises<CollectionItemsRemovedEventArgs<object>>(
                h => bindableCollection.CollectionItemsRemoved += h, h => bindableCollection.CollectionItemsRemoved -= h,
                () => bindableCollection.Clear()
            );

            Assert.Equal(new[] { item }, collectionItemsRemovedEvent.Arguments.RemovedItems);
        }

        [Fact]
        public void RemoveRange_WhenNotNotifying_ThenCollectionItemsRemovedIsNotRaised()
        {
            var item = new object();
            var bindableCollection = new BindableCollection<object>(new[] { item })
            {
                IsNotifying = false
            };

            var raised = false;
            bindableCollection.CollectionItemsRemoved += (s, e) => raised = true;

            bindableCollection.RemoveRange(new[] { item });

            Assert.False(raised);
        }

        [Fact]
        public void RemoveRange_WhenCollectionChangedDisablesNotifying_ThenCollectionItemsRemovedIsStillRaised()
        {
            var item = new object();
            var bindableCollection = new BindableCollection<object>(new[] { item });

            bindableCollection.CollectionChanged += (s, e) => bindableCollection.IsNotifying = false;

            var collectionItemsRemovedEvent = Assert.Raises<CollectionItemsRemovedEventArgs<object>>(
                h => bindableCollection.CollectionItemsRemoved += h, h => bindableCollection.CollectionItemsRemoved -= h,
                () => bindableCollection.RemoveRange(new[] { item })
            );

            Assert.Equal(new[] { item }, collectionItemsRemovedEvent.Arguments.RemovedItems);
        }

        [Fact]
        public void Clear_ThenCollectionItemsRemovedIsRaisedAfterCollectionChanged()
        {
            var bindableCollection = new BindableCollection<object>(new[] { new object() });
            var events = new List<string>();

            bindableCollection.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    events.Add(nameof(bindableCollection.CollectionChanged));
                }
            };

            bindableCollection.CollectionItemsRemoved += (s, e) => events.Add(nameof(bindableCollection.CollectionItemsRemoved));

            bindableCollection.Clear();

            Assert.Equal(new[] { nameof(bindableCollection.CollectionChanged), nameof(bindableCollection.CollectionItemsRemoved) }, events);
        }

        [Fact]
        public void AddRange_ThenCollectionItemsRemovedIsNotRaised()
        {
            var bindableCollection = new BindableCollection<object>();

            var raised = false;
            bindableCollection.CollectionItemsRemoved += (s, e) => raised = true;

            bindableCollection.AddRange(new[] { new object() });

            Assert.False(raised);
        }

        [Fact]
        public void CollectionItemsRemovedEventArgs_WhenRemovedItemsIsNull_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new CollectionItemsRemovedEventArgs<object>(null));
        }
    }
}
