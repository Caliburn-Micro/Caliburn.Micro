using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Caliburn.Micro.Core.Tests
{
    public class EventAggregatorSubscribing
    {
        [Fact]
        public void A_null_subscriber_causes_an_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new EventAggregator().SubscribeOnPublishedThread(null); });
        }

        [Fact]
        public void A_null_marshall_causes_an_ArgumentNullException()
        {
            var handlerStub = new Mock<IHandle<object>>().Object;
            Assert.Throws<ArgumentNullException>(() => { new EventAggregator().Subscribe(handlerStub, null); });
        }

        [Fact]
        public void A_valid_subscriber_is_assigned_as_a_handler_its_message_type()
        {
            var handlerStub = new Mock<IHandle<object>>().Object;
            var aggregator = new EventAggregator();

            Assert.False(aggregator.HandlerExistsFor(typeof(object)));

            aggregator.SubscribeOnPublishedThread(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof(object)));
        }
    }

    public class EventAggregatorUnsubscribing
    {
        [Fact]
        public void A_null_subscriber_throws_an_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new EventAggregator().Unsubscribe(null); });
        }

        [Fact]
        public void A_valid_subscriber_gets_removed_from_the_handler_list()
        {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();

            eventAggregator.SubscribeOnPublishedThread(handlerMock.Object);
            Assert.True(eventAggregator.HandlerExistsFor(typeof(object)));

            eventAggregator.Unsubscribe(handlerMock.Object);
            Assert.False(eventAggregator.HandlerExistsFor(typeof(object)));
        }
    }

    public class EventAggregatorPublishing
    {
        [Fact]
        public async Task A_null_marshal_causes_an_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await new EventAggregator().PublishAsync(new object(), null, CancellationToken.None); });
        }

        [Fact]
        public async Task A_null_message_causes_an_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await new EventAggregator().PublishOnCurrentThreadAsync(null, CancellationToken.None); });
        }

        [Fact]
        public async Task A_valid_message_is_invoked_on_the_supplied_publication_marshaller()
        {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();
            var marshallerCalled = false;

            eventAggregator.SubscribeOnPublishedThread(handlerMock.Object);

            await eventAggregator.PublishAsync(new object(), f =>
            {
                marshallerCalled = true;

                return f();

            }, CancellationToken.None);

            Assert.True(marshallerCalled);
        }

        [Fact]
        public async Task A_valid_message_is_invoked_on_the_supplied_subscription_marshaller()
        {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();
            var marshallerCalled = false;

            eventAggregator.Subscribe(handlerMock.Object, f =>
            {
                marshallerCalled = true;

                return f();

            });

            await eventAggregator.PublishOnCurrentThreadAsync(new object(), CancellationToken.None);

            Assert.True(marshallerCalled);
        }

        [Fact]
        public async Task A_valid_message_is_published_to_all_handlers()
        {
            var eventAggregator = new EventAggregator();

            var handlerMockA = new Mock<IHandle<object>>();
            var handlerMockB = new Mock<IHandle<object>>();

            eventAggregator.SubscribeOnPublishedThread(handlerMockA.Object);
            eventAggregator.SubscribeOnPublishedThread(handlerMockB.Object);

            await eventAggregator.PublishOnCurrentThreadAsync(new object(), CancellationToken.None);

            handlerMockA.Verify(handlerStub => handlerStub.HandleAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
            handlerMockB.Verify(handlerStub => handlerStub.HandleAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
        }

        [Fact]
        public async Task A_valid_message_is_published_to_all_handlers_respecting_inheritance()
        {
            var eventAggregator = new EventAggregator();

            var handlerMockA = new Mock<IHandle<MessageBase>>();
            var handlerMockB = new Mock<IHandle<Message>>();

            eventAggregator.SubscribeOnPublishedThread(handlerMockA.Object);
            eventAggregator.SubscribeOnPublishedThread(handlerMockB.Object);

            await eventAggregator.PublishOnCurrentThreadAsync(new Message(), CancellationToken.None);

            handlerMockA.Verify(handlerStub => handlerStub.HandleAsync(It.IsAny<MessageBase>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
            handlerMockB.Verify(handlerStub => handlerStub.HandleAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
        }

        public class MessageBase { }
        public class Message : MessageBase { }
    }

    public class EventAggregatorHandlerExistence
    {
        [Fact]
        public void False_returned_when_no_handlers_exist_for_a_given_message()
        {
            Assert.False(new EventAggregator().HandlerExistsFor(typeof(object)));
        }

        [Fact]
        public void True_returned_when_a_handler_exists_for_a_given_message()
        {
            var handlerStub = new Mock<IHandle<object>>().Object;
            var aggregator = new EventAggregator();

            aggregator.SubscribeOnPublishedThread(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof(object)));
        }
    }
}
