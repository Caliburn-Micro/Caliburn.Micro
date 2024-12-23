using Caliburn.Micro.Core.Tests.Services;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class SimpleContainerCheckingForHandler
    {
        [Fact]
        public void HasHandlerReturnsRalseWhenHandlerDoesNotExist()
        {
            Assert.False(new SimpleContainer().HasHandler(typeof(object), null));
            Assert.False(new SimpleContainer().HasHandler(null, "Object"));
        }

        [Fact]
        public void HasHandlerReturnsTrueWhenHandlerExists()
        {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "Object", typeof(object));

            Assert.True(container.HasHandler(typeof(object), null));
            Assert.True(container.HasHandler(null, "Object"));
        }

        [Fact]
        public void GetInstanceShouldReturnNullIfNoHandler()
        {
            var container = new SimpleContainer();

            var resolvedInstance = container.GetInstance(typeof(ITestService), null);

            Assert.Null(resolvedInstance);
        }

        [Fact]
        public void HasHandlerShouldReturnTrueIfHandlerExists()
        {
            var container = new SimpleContainer();
            var instance = new TestService();

            container.RegisterInstance(typeof(ITestService), null, instance);

            var hasHandler = container.HasHandler(typeof(ITestService), null);

            Assert.True(hasHandler);
        }

        [Fact]
        public void HasHandlerShouldReturnFalseIfHandlerDoesNotExist()
        {
            var container = new SimpleContainer();

            var hasHandler = container.HasHandler(typeof(ITestService), null);

            Assert.False(hasHandler);
        }
    }
}
