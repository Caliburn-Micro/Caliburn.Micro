using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class BindableCollectionTests
    {
        [Fact]
        public void Clear_ThenCollectionClearedIsRaisedWithAllItems()
        {
            var objects = new[] { new object(), new object(), new object() };
            var bindableCollection = new BindableCollection<object>(objects);

            var collectionClearedEvent = Assert.Raises<CollectionClearedEventArgs<object>>(
                h => bindableCollection.CollectionCleared += h, h => bindableCollection.CollectionCleared -= h,
                () => bindableCollection.Clear()
            );

            Assert.Equal(objects, collectionClearedEvent.Arguments.ClearedItems);
        }
    }
}
