namespace Caliburn.Micro.WPF.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class EventAggregator_Subscribing {
        [Fact]
        public void A_null_subscriber_causes_an_ArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => {
                new EventAggregator().Subscribe(null);
            });
        }

        [Fact]
        public void A_valid_subscriber_is_assigned_as_a_handler_its_message_type() {
            var handlerStub = new Mock<IHandle<object>>().Object;
            var aggregator = new EventAggregator();

            Assert.False(aggregator.HandlerExistsFor(typeof (object)));

            aggregator.Subscribe(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof (object)));
        }
    }

    public class EventAggregator_Unsubscribing {
        [Fact]
        public void A_null_subscriber_throws_an_ArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => {
                new EventAggregator().Unsubscribe(null);
            });
        }

        [Fact]
        public void A_valid_subscriber_gets_removed_from_the_handler_list() {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();

            eventAggregator.Subscribe(handlerMock.Object);
            Assert.True(eventAggregator.HandlerExistsFor(typeof (object)));

            eventAggregator.Unsubscribe(handlerMock.Object);
            Assert.False(eventAggregator.HandlerExistsFor(typeof (object)));
        }
    }

    public class EventAggregator_Publishing {
        [Fact]
        public void A_null_message_causes_an_ArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => {
                new EventAggregator().Publish(null);
            });
        }

        [Fact]
        public void A_null_marshal_causes_an_ArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => {
                new EventAggregator().Publish(new object(), null);
            });
        }

        [Fact]
        public void A_valid_message_is_published_to_all_handlers() {
            var eventAggregator = new EventAggregator();
            var handlerMockA = new Mock<IHandle<object>>();
            var handlerMockB = new Mock<IHandle<object>>();


            eventAggregator.Subscribe(handlerMockA.Object);
            eventAggregator.Subscribe(handlerMockB.Object);
            eventAggregator.Publish(new object());

            handlerMockA.Verify(handlerStub => handlerStub.Handle(It.IsAny<object>()),
                                Times.AtLeastOnce());
            handlerMockB.Verify(handlerStub => handlerStub.Handle(It.IsAny<object>()),
                                Times.AtLeastOnce());

        }

        [Fact]
        public void A_valid_message_invokes_HandlerResultProcessing_when_expected_to() {
            var eventAggregator = new EventAggregator();
            var coroutineHandlerMock = new Mock<IHandleWithCoroutine<object>>();
            var taskHandlerMock = new Mock<IHandleWithTask<object>>();
            var coroutineHandlerProcessed = false;
            var taskHandlerProcessed = false;

            coroutineHandlerMock.Setup(handlerStub => handlerStub.Handle(It.IsAny<object>()))
                                .Returns(() => new List<IResult>());

            taskHandlerMock.Setup(handlerStub => handlerStub.Handle(It.IsAny<object>()))
                           .Returns(() => new Task(() => { }));

            EventAggregator.HandlerResultProcessing = (target, result) => {

                if (target is IHandleWithCoroutine<object>) {
                    coroutineHandlerProcessed = true;
                }
                else if (target is IHandleWithTask<object>) {
                    taskHandlerProcessed = true;
                }
            };

            eventAggregator.Subscribe(coroutineHandlerMock.Object);
            eventAggregator.Subscribe(taskHandlerMock.Object);
            eventAggregator.Publish(new object());

            Assert.True(coroutineHandlerProcessed);
            Assert.True(taskHandlerProcessed);
        }

        [Fact]
        public void A_valid_message_is_invoked_on_the_supplied_marshaller() {
            var eventAggregator = new EventAggregator();
            var handlerMock = new Mock<IHandle<object>>();
            var marshallerCalled = false;

            eventAggregator.Subscribe(handlerMock.Object);
            eventAggregator.Publish(new object(), action => marshallerCalled = true);

            Assert.True(marshallerCalled);
        }
    }

    public class EventAggregator_HandlerExistence {
        [Fact]
        public void True_returned_when_a_handler_exists_for_a_given_message() {
            var handlerStub = new Mock<IHandle<object>>().Object;
            var aggregator = new EventAggregator();

            aggregator.Subscribe(handlerStub);

            Assert.True(aggregator.HandlerExistsFor(typeof (object)));
        }

        [Fact]
        public void False_returned_when_no_handlers_exist_for_a_given_message() {
            Assert.False(new EventAggregator().HandlerExistsFor(typeof (object)));
        }

    }
}
