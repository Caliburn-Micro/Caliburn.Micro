using System.Collections.Generic;
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

    public class SimpleContainer_Recursive
    {
        private interface IComponent { }
        private interface IDependency1 { }
        private interface IDependency2 { }
        private interface IEnumerableDependency { }

        private class Component : IComponent
        {
            public IDependency1 Dependency1 { get; set; }
            public NonInterfaceDependency NonInterfaceDependency { get; set; }
        }

        private class Dependency1 : IDependency1
        {
            public IDependency2 Dependency2 { get; set; }
            public IList<IEnumerableDependency> EnumerableDependencies { get; set; }
        }

        private class Dependency2 : IDependency2 { }

        private class EnumerableDependency1 : IEnumerableDependency
        {
            public IDependency2 Dependency2 { get; set; }
        }

        private class EnumerableDependency2 : IEnumerableDependency { }

        private class NonInterfaceDependency { }

        private class SecondDependency1 : Dependency1 { }

        private static void RegisterAllComponents(SimpleContainer container)
        {
            container.RegisterPerRequest(typeof(IComponent), null, typeof(Component));
            container.RegisterPerRequest(typeof(IDependency1), null, typeof(Dependency1));
            container.RegisterPerRequest(typeof(IDependency2), null, typeof(Dependency2));
            container.RegisterPerRequest(typeof(NonInterfaceDependency), null, typeof(NonInterfaceDependency));
            container.RegisterPerRequest(typeof(IEnumerableDependency), null, typeof(EnumerableDependency1));
            container.RegisterPerRequest(typeof(IEnumerableDependency), null, typeof(EnumerableDependency2));
        }

        [Fact]
        public void Instances_Are_Recursively_Property_Injected_When_Enabled()
        {
            var container = new SimpleContainer
            {
                EnablePropertyInjection = true
            };

            RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();

            Assert.NotNull(((Dependency1)instance.Dependency1).Dependency2);
        }

        [Fact]
        public void BuildUp_Injects_All_Registered_Dependencies_Non_Recursively()
        {
            var container = new SimpleContainer();
            RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.Null(((Dependency1)instance.Dependency1).Dependency2);
        }

        [Fact]
        public void BuildUp_Injects_Dependencies_Recursively()
        {
            var container = new SimpleContainer
            {
                EnablePropertyInjection = true
            };

            RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.NotNull(((Dependency1)instance.Dependency1).Dependency2);
        }

        [Fact]
        public void BuildUp_Injects_Enumerable_Dependencies()
        {
            var container = new SimpleContainer
            {
                EnablePropertyInjection = true
            };

            RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.Equal(2, (((Dependency1)instance.Dependency1).EnumerableDependencies.Count));
        }

        [Fact]
        public void BuildUp_Injects_Properties_Of_Enumerable_Dependencies()
        {
            var container = new SimpleContainer
            {
                EnablePropertyInjection = true
            };

            RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.NotNull(((EnumerableDependency1)(((Dependency1)instance.Dependency1).EnumerableDependencies.First())).Dependency2);
        }

        [Fact]
        public void BuildUp_Throws_When_Multiple_Types_Found_For_Component()
        {
            var container = new SimpleContainer();
            RegisterAllComponents(container);
            container.RegisterPerRequest(typeof(IDependency1), null, typeof(SecondDependency1));

            var instance = (Component)container.GetInstance<IComponent>();

            try
            {
                container.BuildUp(instance);
            }
            catch
            {
                return;
            }

            Assert.NotNull(null);
        }
    }
}
