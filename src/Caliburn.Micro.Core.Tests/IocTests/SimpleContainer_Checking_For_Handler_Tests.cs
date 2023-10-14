using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class SimpleContainer_Checking_For_Handler_Tests {
        [Fact]
        public void HasHandler_returns_false_when_handler_does_not_exist() {
            Assert.False(new SimpleContainer().HasHandler(typeof(object), null));
            Assert.False(new SimpleContainer().HasHandler(null, "Object"));
        }

        [Fact]
        public void HasHandler_returns_true_when_handler_exists() {
            var container = new SimpleContainer();
            container.RegisterPerRequest(typeof(object), "Object", typeof(object));

            Assert.True(container.HasHandler(typeof(object), null));
            Assert.True(container.HasHandler(null, "Object"));
        }
    }
}
