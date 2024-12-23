using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class SimpleContainerCreatingAChildContainer
    {
        [Fact]
        public void Singleton_instances_are_the_same_across_parent_and_child()
        {
            var container = new SimpleContainer();
            container.Singleton<object>();
            var childContainer = container.CreateChildContainer();

            var parentInstance = container.GetInstance(typeof(object), null);
            var childInstance = childContainer.GetInstance(typeof(object), null);

            Assert.Same(parentInstance, childInstance);
        }

        [Fact]
        public void The_child_container_returned_contains_parent_entries()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();
            var childContainer = container.CreateChildContainer();

            Assert.NotNull(childContainer.GetInstance(typeof(object), null));
        }

        [Fact]
        public void The_child_container_returned_is_not_the_same_instance_as_its_parent()
        {
            var container = new SimpleContainer();
            var childContainer = container.CreateChildContainer();

            Assert.NotSame(container, childContainer);
        }



        [Fact]
        public void CreateChildContainer_ShouldCreateChildContainer()
        {
            var container = new SimpleContainer();
            var childContainer = container.CreateChildContainer();

            Assert.NotNull(childContainer);
            Assert.NotEqual(container, childContainer);
        }
    }
}
