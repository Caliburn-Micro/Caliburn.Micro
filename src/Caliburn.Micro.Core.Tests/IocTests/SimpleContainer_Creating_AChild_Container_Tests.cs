using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class SimpleContainer_Creating_AChild_Container_Tests {
        [Fact]
        public void Singleton_instances_are_the_same_across_parent_and_child() {
            var container = new SimpleContainer();
            container.Singleton<object>();
            SimpleContainer childContainer = container.CreateChildContainer();

            object parentInstance = container.GetInstance(typeof(object), null);
            object childInstance = childContainer.GetInstance(typeof(object), null);

            Assert.Same(parentInstance, childInstance);
        }

        [Fact]
        public void The_child_container_returned_contains_parent_entries() {
            var container = new SimpleContainer();
            container.PerRequest<object>();
            SimpleContainer childContainer = container.CreateChildContainer();

            Assert.NotNull(childContainer.GetInstance(typeof(object), null));
        }

        [Fact]
        public void The_child_container_returned_is_not_the_same_instance_as_its_parent() {
            var container = new SimpleContainer();
            SimpleContainer childContainer = container.CreateChildContainer();

            Assert.NotSame(container, childContainer);
        }
    }
}
