using System;
using System.Threading;
using System.Threading.Tasks;

using Moq;
using Xunit;

namespace Caliburn.Micro.Core.Tests {
    public class EventAggregatorPublishingTests {
        [Fact]
        public async Task A_null_marshal_causes_an_ArgumentNullException()
            => await Assert.ThrowsAsync<ArgumentNullException>(async () => await new EventAggregator().PublishAsync(new object(), null, CancellationToken.None));

        [Fact]
        public async Task A_null_message_causes_an_ArgumentNullException()
            => await Assert.ThrowsAsync<ArgumentNullException>(async () => await new EventAggregator().PublishOnCurrentThreadAsync(null, CancellationToken.None));

        [Fact]
        public async Task A_valid_message_is_invoked_on_the_supplied_publication_marshaller() {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();
            bool marshallerCalled = false;

            eventAggregator.SubscribeOnPublishedThread(handlerMock.Object);

            await eventAggregator.PublishAsync(
                new object(),
                f => {
                    marshallerCalled = true;

                    return f();
                },
                CancellationToken.None);

            Assert.True(marshallerCalled);
        }

        [Fact]
        public async Task A_valid_message_is_invoked_on_the_supplied_subscription_marshaller() {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();
            bool marshallerCalled = false;

            eventAggregator.Subscribe(handlerMock.Object, f => {
                marshallerCalled = true;

                return f();
            });

            await eventAggregator.PublishOnCurrentThreadAsync(new object(), CancellationToken.None);

            Assert.True(marshallerCalled);
        }

        [Fact]
        public async Task A_valid_message_is_published_to_all_handlers() {
            var eventAggregator = new EventAggregator();

            var handlerMockA = new Mock<IHandle<object>>();
            var handlerMockB = new Mock<IHandle<object>>();

            eventAggregator.SubscribeOnPublishedThread(handlerMockA.Object);
            eventAggregator.SubscribeOnPublishedThread(handlerMockB.Object);

            await eventAggregator.PublishOnCurrentThreadAsync(new object(), CancellationToken.None);

            handlerMockA.Verify(
                handlerStub => handlerStub.HandleAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
            handlerMockB.Verify(
                handlerStub => handlerStub.HandleAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
        }

        [Fact]
        public async Task A_valid_message_is_published_to_all_handlers_respecting_inheritance() {
            var eventAggregator = new EventAggregator();

            var handlerMockA = new Mock<IHandle<MessageBase>>();
            var handlerMockB = new Mock<IHandle<Message>>();

            eventAggregator.SubscribeOnPublishedThread(handlerMockA.Object);
            eventAggregator.SubscribeOnPublishedThread(handlerMockB.Object);

            await eventAggregator.PublishOnCurrentThreadAsync(new Message(), CancellationToken.None);

            handlerMockA.Verify(
                handlerStub => handlerStub.HandleAsync(It.IsAny<MessageBase>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
            handlerMockB.Verify(
                handlerStub => handlerStub.HandleAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()),
                Times.AtLeastOnce());
        }

        public class MessageBase {
        }

        public sealed class Message : MessageBase {
        }
    }
}
