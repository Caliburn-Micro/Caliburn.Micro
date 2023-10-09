using System;

using Moq;
using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class EventAggregatorSubscribingTests {
        [Fact]
        public void A_null_subscriber_causes_an_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => { new EventAggregator().SubscribeOnPublishedThread(null); });

        [Fact]
        public void A_null_marshall_causes_an_ArgumentNullException() {
            IHandle<object> handlerStub = new Mock<IHandle<object>>().Object;
            Assert.Throws<ArgumentNullException>(() => { new EventAggregator().Subscribe(handlerStub, null); });
        }

        [Fact]
        public void A_valid_subscriber_is_assigned_as_a_handler_its_message_type() {
            IHandle<object> handlerStub = new Mock<IHandle<object>>().Object;
            var aggregator = new EventAggregator();

            Assert.False(aggregator.HandlerExistsFor(typeof(object)));

            aggregator.SubscribeOnPublishedThread(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof(object)));
        }
    }
}
