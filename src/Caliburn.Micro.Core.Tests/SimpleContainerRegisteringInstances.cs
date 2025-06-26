using System.Linq;
using Caliburn.Micro.Core.Tests.Services;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class SimpleContainerRegisteringInstances
    {
        [Fact]
        public void RegisterInstanceShouldRegisterInstance()
        {
            var container = new SimpleContainer();
            var instance = new TestService();

            container.RegisterInstance(typeof(ITestService), null, instance);

            var resolvedInstance = container.GetInstance(typeof(ITestService), null);

            Assert.NotNull(resolvedInstance);
            Assert.Equal(instance, resolvedInstance);
        }

        [Fact]
        public void RegisterPerRequestShouldRegisterPerRequest()
        {
            var container = new SimpleContainer();

            container.RegisterPerRequest(typeof(ITestService), null, typeof(TestService));

            var instance1 = container.GetInstance(typeof(ITestService), null);
            var instance2 = container.GetInstance(typeof(ITestService), null);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotEqual(instance1, instance2);
        }

        [Fact]
        public void RegisterSingletonShouldRegisterSingleton()
        {
            var container = new SimpleContainer();

            container.RegisterSingleton(typeof(ITestService), null, typeof(TestService));

            var instance1 = container.GetInstance(typeof(ITestService), null);
            var instance2 = container.GetInstance(typeof(ITestService), null);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.Equal(instance1, instance2);
        }

        [Fact]
        public void InstancesRegistedSingletonReturnTheSameInstanceForEachCall()
        {
            var container = new SimpleContainer();
            container.Singleton<object>();

            var instanceA = container.GetInstance(typeof(object), null);
            var instanceB = container.GetInstance(typeof(object), null);

            Assert.Same(instanceA, instanceB);
        }

        [Fact]
        public void InstancesRegisteredPerRequestReturnsaDifferentInstanceForEachCall()
        {
            var container = new SimpleContainer();
            container.PerRequest<object>();

            var instanceA = container.GetInstance(typeof(object), null);
            var instanceB = container.GetInstance(typeof(object), null);

            Assert.NotSame(instanceA, instanceB);
        }

        [Fact]
        public void InstancesRegisteredWithDifferentKeysGetAllInstancesReturnAll()
        {
            var container = new SimpleContainer();
            container.RegisterInstance(typeof(object), "test", new object());
            container.RegisterInstance(typeof(object), "test", new object());

            var results = container.GetAllInstances<object>("test");

            Assert.Equal(2, results.Count());
        }


        [Fact]
        public void UnregisterHandlerShouldUnregisterHandler()
        {
            var container = new SimpleContainer();
            var instance = new TestService();

            container.RegisterInstance(typeof(ITestService), null, instance);
            container.UnregisterHandler(typeof(ITestService), null);

            var resolvedInstance = container.GetInstance(typeof(ITestService), null);

            Assert.Null(resolvedInstance);
        }

    }
}
