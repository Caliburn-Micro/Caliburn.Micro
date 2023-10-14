using Moq;
using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class EventAggregatorHandlerExistenceTests {
        [Fact]
        public void False_returned_when_no_handlers_exist_for_a_given_message()
            => Assert.False(new EventAggregator().HandlerExistsFor(typeof(object)));

        [Fact]
        public void True_returned_when_a_handler_exists_for_a_given_message() {
            IHandle<object> handlerStub = new Mock<IHandle<object>>().Object;
            var aggregator = new EventAggregator();

            aggregator.SubscribeOnPublishedThread(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof(object)));
        }
    }
}
