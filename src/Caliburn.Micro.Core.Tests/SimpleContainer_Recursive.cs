using System;
using System.Linq;
using Caliburn.Micro.Core.Tests.Services;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public partial class SimpleContainer_Recursive
    {

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

            Assert.Equal(2, ((Dependency1)instance.Dependency1).EnumerableDependencies.Count);
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

            Assert.NotNull(((EnumerableDependency1)((Dependency1)instance.Dependency1).EnumerableDependencies.First()).Dependency2);
        }

        [Fact]
        public void BuildUp_Throws_When_Multiple_Types_Found_For_Component()
        {
            var container = new SimpleContainer();
            RegisterAllComponents(container);
            container.RegisterPerRequest(typeof(IDependency1), null, typeof(SecondDependency1));

            var instance = (Component)container.GetInstance<IComponent>();


            Assert.Throws<InvalidOperationException>(() => container.BuildUp(instance));

        }

    }
}
