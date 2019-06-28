using System.Linq;
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
    }

    public class SimpleContainerCheckingForHandler
    {
        [Fact]
        public void HasHandler_returns_false_when_handler_does_not_exist()
        {
            Assert.False(new SimpleContainer().HasHandler(typeof(object), null));
            Assert.False(new SimpleContainer().HasHandler(null, "Object"));
        }

        [Fact]
        public void HasHandler_returns_true_when_handler_exists()
        {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "Object", typeof(object));

            Assert.True(container.HasHandler(typeof(object), null));
            Assert.True(container.HasHandler(null, "Object"));
        }
    }

    public class SimpleContainerGettingASingleInstance
    {
        [Fact]
        public void An_instance_is_returned_for_the_type_specified_if_found()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            Assert.NotNull(container.GetInstance(typeof(object), null));
        }

        [Fact]
        public void Instances_can_be_found_by_name_only()
        {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "AnObject", typeof(object));

            Assert.NotNull(container.GetInstance(null, "AnObject"));
        }

        [Fact]
        public void Null_is_returned_when_no_instance_is_found()
        {
            Assert.Null(new SimpleContainer().GetInstance(typeof(object), null));
        }
    }

    public class SimpleContainerRegisteringInstances
    {
        [Fact]
        public void Instances_registed_Singleton_return_the_same_instance_for_each_call()
        {
            var container = new SimpleContainer();
            container.Singleton<object>();

            var instanceA = container.GetInstance(typeof(object), null);
            var instanceB = container.GetInstance(typeof(object), null);

            Assert.Same(instanceA, instanceB);
        }

        [Fact]
        public void Instances_registered_PerRequest_returns_a_different_instance_for_each_call()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            var instanceA = container.GetInstance(typeof(object), null);
            var instanceB = container.GetInstance(typeof(object), null);

            Assert.NotSame(instanceA, instanceB);
        }

        [Fact]
        public void Instances_registered_with_different_keys_get_all_instances_return_all()
        {
            var container = new SimpleContainer();
            container.RegisterInstance(typeof(object), "test", new object());
            container.RegisterInstance(typeof(object), "test", new object());

            var results = container.GetAllInstances<object>("test");

            Assert.Equal(2, results.Count());
        }
    }

    public class SimpleContainer_Find_Constructor
    {

        public class SingleEmptyConstructorType
        {
            public SingleEmptyConstructorType()
            {

            }
        }

        [Fact]
        public void Container_Finds_Single_Constructor()
        {
            var container = new SimpleContainer();
            container.Singleton<SingleEmptyConstructorType>();
            container.GetInstance(typeof(SingleEmptyConstructorType), null);
        }

        public class SingleNonEmptyConstructorType
        {
            public SingleNonEmptyConstructorType(SimpleContainer_Find_Constructor.SingleEmptyConstructorType type)
            {
            }
        }

        [Fact]
        public void Container_No_EmptyConstructor()
        {
            var container = new SimpleContainer();
            container.Singleton<SingleNonEmptyConstructorType>();
            container.GetInstance(typeof(SingleNonEmptyConstructorType), null);
        }

        public class SingleIntConstructor
        {
            public int Value { get; private set; }

            public SingleIntConstructor(int x)
            {
                this.Value = x;
            }
        }

        [Fact]
        public void Container_SingleIntConstructor()
        {
            var container = new SimpleContainer();
            container.Singleton<SingleIntConstructor>();
            container.RegisterInstance(typeof(int), "x", 4);
            var inst = (SingleIntConstructor)container.GetInstance(typeof(SingleIntConstructor), null);
            Assert.Equal(4, inst.Value);
        }

        public class TwoConstructors
        {
            public int Value { get; set; }

            public TwoConstructors()
            {
                this.Value = 42;
            }

            public TwoConstructors(int value)
            {
                Value = value;
            }
        }

        [Fact]
        public void Container_ChooseConstructorWithRegisteredParameter()
        {
            var container = new SimpleContainer();
            container.Singleton<TwoConstructors>();
            container.RegisterInstance(typeof(int), null, 23);
            var inst = (TwoConstructors)container.GetInstance(typeof(TwoConstructors), null);
            Assert.Equal(23, inst.Value);
        }

        [Fact]
        public void Container_ChooseEmptyConstructorWithoutRegisteredParameter()
        {
            var container = new SimpleContainer();
            container.Singleton<TwoConstructors>();
            var inst = (TwoConstructors)container.GetInstance(typeof(TwoConstructors), null);
            Assert.Equal(42, inst.Value);
        }
    }
}
