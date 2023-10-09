using System;

using Moq;
using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class EventAggregatorUnsubscribingTests {
        [Fact]
        public void A_null_subscriber_throws_an_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() => { new EventAggregator().Unsubscribe(null); });

        [Fact]
        public void A_valid_subscriber_gets_removed_from_the_handler_list() {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();

            eventAggregator.SubscribeOnPublishedThread(handlerMock.Object);
            Assert.True(eventAggregator.HandlerExistsFor(typeof(object)));

            eventAggregator.Unsubscribe(handlerMock.Object);
            Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));
        }
    }
}
