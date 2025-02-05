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
        public void InstancesAreRecursivelyPropertyInjectedWhenEnabled()
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
        public void BuildUpInjectsAllRegisteredDependenciesNonRecursively()
        {
            var container = new SimpleContainer();
            RegisterAllComponents(container);

            var instance = (Component)container.GetInstance<IComponent>();
            container.BuildUp(instance);

            Assert.Null(((Dependency1)instance.Dependency1).Dependency2);
        }

        [Fact]
        public void BuildUpInjectsDependenciesRecursively()
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
        public void BuildUpInjectsEnumerableDependencies()
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
        public void BuildUpInjectsPropertiesOfEnumerableDependencies()
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
        public void BuildUpThrowsWhenMultipleTypesFoundForComponent()
        {
            var container = new SimpleContainer();
            RegisterAllComponents(container);
            container.RegisterPerRequest(typeof(IDependency1), null, typeof(SecondDependency1));

            var instance = (Component)container.GetInstance<IComponent>();


            Assert.Throws<InvalidOperationException>(() => container.BuildUp(instance));

        }

    }
}
