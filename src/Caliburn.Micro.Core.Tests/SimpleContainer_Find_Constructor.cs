using Caliburn.Micro.Core.Tests.Services;
using Xunit;
namespace Caliburn.Micro.Core.Tests
{
    public partial class SimpleContainer_Find_Constructor
    {

        [Fact]
        public void ContainerFindsSingleConstructor()
        {
            var container = new SimpleContainer();
            container.Singleton<SingleEmptyConstructorType>();
            container.GetInstance(typeof(SingleEmptyConstructorType), null);
        }

        [Fact]
        public void ContainerNoEmptyConstructor()
        {
            var container = new SimpleContainer();
            container.Singleton<SingleNonEmptyConstructorType>();
            container.GetInstance(typeof(SingleNonEmptyConstructorType), null);
        }

        [Fact]
        public void ContainerSingleIntConstructor()
        {
            var container = new SimpleContainer();
            container.Singleton<SingleIntConstructor>();
            container.RegisterInstance(typeof(int), "x", 4);
            var inst = (SingleIntConstructor)container.GetInstance(typeof(SingleIntConstructor), null);
            Assert.Equal(4, inst.Value);
        }

        [Fact]
        public void ContainerChooseConstructorWithRegisteredParameter()
        {
            var container = new SimpleContainer();
            container.Singleton<TwoConstructors>();
            container.RegisterInstance(typeof(int), null, 23);
            var inst = (TwoConstructors)container.GetInstance(typeof(TwoConstructors), null);
            Assert.Equal(23, inst.Value);
        }

        [Fact]
        public void ContainerChooseEmptyConstructorWithoutRegisteredParameter()
        {
            var container = new SimpleContainer();
            container.Singleton<TwoConstructors>();
            var inst = (TwoConstructors)container.GetInstance(typeof(TwoConstructors), null);
            Assert.Equal(42, inst.Value);
        }

        [Fact]
        public void BuildUpShouldInjectProperties()
        {
            var container = new SimpleContainer();
            var instance = new TestServiceWithDependency();

            container.RegisterInstance(typeof(ITestService), null, new TestService());

            container.BuildUp(instance);

            Assert.NotNull(instance.Dependency);
        }
    }
}
