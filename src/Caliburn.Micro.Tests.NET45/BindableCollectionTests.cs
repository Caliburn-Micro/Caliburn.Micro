using System.Collections.Specialized;
using Xunit;

namespace Caliburn.Micro.WPF.Tests
{
    public class BindableCollection_AddRange
    {
        [Fact]
        public void RaisesCollectionChanged()
        {
            var collection = new BindableCollection<int>();
            var raised = false;
            collection.CollectionChanged += (s, e) => raised = true;

            collection.AddRange(new[] { 1, 2, 3, 4 });

            Assert.True(raised);
        }

        [Fact]
        public void CollectionChangedActionIsAdd()
        {
            var collection = new BindableCollection<int>();
            NotifyCollectionChangedEventArgs eventArgs = null;
            collection.CollectionChanged += (s, e) => eventArgs = e;

            collection.AddRange(new[] { 1, 2, 3, 4 });

            Assert.NotNull(eventArgs);
            Assert.Equal(NotifyCollectionChangedAction.Add, eventArgs.Action);
        }

        [Fact]
        public void CollectionChangedEventArgs_HasAddedItems()
        {
            var collection = new BindableCollection<int>();
            NotifyCollectionChangedEventArgs eventArgs = null;
            collection.CollectionChanged += (s, e) => eventArgs = e;

            collection.AddRange(new[] { 1, 2, 3, 4 });

            Assert.NotNull(eventArgs);
            Assert.True(eventArgs.NewItems.Contains(1));
            Assert.True(eventArgs.NewItems.Contains(2));
            Assert.True(eventArgs.NewItems.Contains(3));
            Assert.True(eventArgs.NewItems.Contains(4));
        }
    }

    public class BindableCollection_RemoveRange
    {
        [Fact]
        public void RaisesCollectionChanged()
        {
            var collection = new BindableCollection<int>(new[] { 1, 2, 3, 4 });
            var raised = false;
            collection.CollectionChanged += (s, e) => raised = true;

            collection.RemoveRange(new[] { 2, 3 });

            Assert.True(raised);
        }

        [Fact]
        public void CollectionChangedActionIsRemove()
        {
            var collection = new BindableCollection<int>(new[] { 1, 2, 3, 4 });
            NotifyCollectionChangedEventArgs eventArgs = null;
            collection.CollectionChanged += (s, e) => eventArgs = e;

            collection.RemoveRange(new[] { 1, 3 });

            Assert.Equal(NotifyCollectionChangedAction.Remove, eventArgs.Action);
        }

        [Fact]
        public void CollectionChangedEventArgs_HasRemovedItems()
        {
            var collection = new BindableCollection<int>(new[] { 1, 2, 3, 4 });
            NotifyCollectionChangedEventArgs eventArgs = null;
            collection.CollectionChanged += (s, e) => eventArgs = e;

            collection.RemoveRange(new[] { 1, 3 });

            Assert.True(eventArgs.OldItems.Contains(1));
            Assert.True(eventArgs.OldItems.Contains(3));
        }
    }
}