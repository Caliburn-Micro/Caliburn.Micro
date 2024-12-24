using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class SimpleContainerCreatingAChildContainer
    {
        [Fact]
        public void SingletonInstancesAreTheSameAcrossParentAndChild()
        {
            var container = new SimpleContainer();
            container.Singleton<object>();
            var childContainer = container.CreateChildContainer();

            var parentInstance = container.GetInstance(typeof(object), null);
            var childInstance = childContainer.GetInstance(typeof(object), null);

            Assert.Same(parentInstance, childInstance);
        }

        [Fact]
        public void TheChildContainerReturnedContainsParentEntries()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();
            var childContainer = container.CreateChildContainer();

            Assert.NotNull(childContainer.GetInstance(typeof(object), null));
        }

        [Fact]
        public void TheChildContainerReturnedIsNotTheSameInstanceAsItsParent()
        {
            var container = new SimpleContainer();
            var childContainer = container.CreateChildContainer();

            Assert.NotSame(container, childContainer);
        }



        [Fact]
        public void CreateChildContainerShouldCreateChildContainer()
        {
            var container = new SimpleContainer();
            var childContainer = container.CreateChildContainer();

            Assert.NotNull(childContainer);
            Assert.NotEqual(container, childContainer);
        }
    }
}
