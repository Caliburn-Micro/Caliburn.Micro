namespace Caliburn.Micro.WPF.Tests
{
    using Caliburn.Micro.Tests.NET45;
    using System.Linq;
    using Xunit;

    public class SimpleContainer_Creating_a_ChildContainer {
        [Fact]
        public void The_child_container_returned_is_not_the_same_instance_as_its_parent() {
            var container = new SimpleContainer();
            var childContainer = container.CreateChildContainer();

            Assert.NotSame(container, childContainer);
        }

        [Fact]
        public void The_child_container_returned_contains_parent_entries() {
            var container = new SimpleContainer();
            container.PerRequest<object>();
            var childContainer = container.CreateChildContainer();

            Assert.NotNull(childContainer.GetInstance(typeof (object), null));
        }

        [Fact]
        public void Singleton_instances_are_the_same_across_parent_and_child() {
            var container = new SimpleContainer();
            container.Singleton<object>();
            var childContainer = container.CreateChildContainer();

            var parentInstance = container.GetInstance(typeof (object), null);
            var childInstance = childContainer.GetInstance(typeof (object), null);

            Assert.Same(parentInstance, childInstance);
        }
    }

    public class SimpleContainer_Checking_for_Handler {
        [Fact]
        public void HasHandler_returns_true_when_handler_exists() {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof (object), "Object", typeof (object));
            
            Assert.True(container.HasHandler(typeof (object), null));
            Assert.True(container.HasHandler(null, "Object"));
        }

        [Fact]
        public void HasHandler_returns_false_when_handler_does_not_exist() {
            Assert.False(new SimpleContainer().HasHandler(typeof (object), null));
            Assert.False(new SimpleContainer().HasHandler(null, "Object"));
        }
    }

    public class SimpleContainer_Getting_a_Single_Instance {
        [Fact]
        public void Null_is_returned_when_no_instance_is_found() {
            Assert.Null(new SimpleContainer().GetInstance(typeof (object), null));
        }

        [Fact]
        public void An_instance_is_returned_for_the_type_specified_if_found() {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            Assert.NotNull(container.GetInstance(typeof(object), null));
        }

        [Fact]
        public void Instances_can_be_found_by_name_only() {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "AnObject", typeof(object));

            Assert.NotNull(container.GetInstance(null, "AnObject"));
        }

        [Fact]
        public void Instances_Are_Recursively_Property_Injected_When_Enabled() {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);
            container.EnablePropertyInjection = true;

            var instance = (Component)container.GetInstance<IComponent>();

            Assert.NotNull(((Dependency1)instance.Dependency1).Dependency2);
        }
    }

    public class SimpleContainer_Registering_Instances {
        [Fact]
        public void Instances_registered_PerRequest_returns_a_different_instance_for_each_call() {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            var instanceA = container.GetInstance(typeof (object), null);
            var instanceB = container.GetInstance(typeof (object), null);

            Assert.NotSame(instanceA, instanceB);
        }

        [Fact]
        public void Instances_registed_Singleton_return_the_same_instance_for_each_call() {
            var container = new SimpleContainer();
            container.Singleton<object>();

            var instanceA = container.GetInstance(typeof(object), null);
            var instanceB = container.GetInstance(typeof(object), null);

            Assert.Same(instanceA, instanceB);
        }

    }

    public class SimpleContainer_BuildUp {
        [Fact]
        public void BuildUp_Injects_Non_Interface_Dependencies()
        {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.NotNull(instance.NonInterfaceDependency);
        }

        [Fact]
        public void BuildUp_Injects_All_Registered_Dependencies_Non_Recursively() {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.Null(((Dependency1)instance.Dependency1).Dependency2);
        }

        [Fact]
        public void BuildUp_Injects_Dependencies_Recursively()
        {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance, true);

            Assert.NotNull(((Dependency1)instance.Dependency1).Dependency2);
        }

        [Fact]
        public void BuildUp_Injects_Enumerable_Dependencies()
        {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance, true);

            Assert.Equal(2, (((Dependency1)instance.Dependency1).EnumerableDependencies.Count));
        }

        [Fact]
        public void BuildUp_Injects_Properties_Of_Enumerable_Dependencies()
        {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance, true);

            Assert.NotNull(((EnumerableDependency1)(((Dependency1)instance.Dependency1).EnumerableDependencies.First())).Dependency2);
        }

        [Fact]
        public void BuildUp_Throws_When_Multiple_Types_Found_For_Component()
        {
            var container = new SimpleContainer();
            ContainerSetup.RegisterAllComponents(container);
            container.RegisterPerRequest(typeof(IDependency1), null, typeof(SecondDependency1));

            var instance = (Component)container.GetInstance<IComponent>();

            try {
                container.BuildUp(instance);
            }
            catch {
                return;
            }

            Assert.NotNull(null);
        }
    }

    public static class ContainerSetup {
        internal static void RegisterAllComponents(SimpleContainer container)
        {
            container.RegisterPerRequest(typeof(IComponent), null, typeof(Component));
            container.RegisterPerRequest(typeof(IDependency1), null, typeof(Dependency1));
            container.RegisterPerRequest(typeof(IDependency2), null, typeof(Dependency2));
            container.RegisterPerRequest(typeof(NonInterfaceDependency), null, typeof(NonInterfaceDependency));
            container.RegisterPerRequest(typeof(IEnumerableDependency), null, typeof(EnumerableDependency1));
            container.RegisterPerRequest(typeof(IEnumerableDependency), null, typeof(EnumerableDependency2));
        }
    }
}