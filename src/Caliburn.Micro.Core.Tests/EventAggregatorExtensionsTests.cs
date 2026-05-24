using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
namespace Caliburn.Micro.Core.Tests
{
    public class EventAggregatorExtensionsTests
    {
        const int NumberOfMockTargets = 20;
        [Fact]
        public void SubscribeOnUIThread_CallsSubscribeWithUIThreadMarshaller()
        {
            var mockAggregator = new Mock<IEventAggregator>();
            var subscriber = new object();

            Func<Func<Task>, Task> capturedMarshaller = null;
            mockAggregator.Setup(x => x.Subscribe(subscriber, It.IsAny<Func<Func<Task>, Task>>()))
                .Callback<object, Func<Func<Task>, Task>>((_, m) => capturedMarshaller = m);

            mockAggregator.Object.SubscribeOnUIThread(subscriber);

            Assert.NotNull(capturedMarshaller);
            // UI thread marshaler returns a Task
            var taskSource = new TaskCompletionSource<bool>();
            var task = capturedMarshaller(() => { taskSource.SetResult(true); return taskSource.Task; });
            Assert.IsAssignableFrom<Task>(task);
        }

        [Fact]
        public async Task PublishOnCurrentThreadAsync_CallsPublishAsyncWithDirectMarshaller()
        {
            var eventAggregator = new EventAggregator();
            string message = "Test";
            using (var tokenSource = new CancellationTokenSource())
            {
                var cancellationToken = tokenSource.Token;
                var mockTarget1 = new Mock<IHandle<string>>();
                var mockTarget2 = new Mock<IHandle<string>>();
                eventAggregator.SubscribeOnPublishedThread(mockTarget1.Object);
                eventAggregator.SubscribeOnPublishedThread(mockTarget2.Object);
                await eventAggregator.PublishOnCurrentThreadAsync(message, cancellationToken);

                mockTarget1.Verify(x => x.HandleAsync(message, cancellationToken), Times.Once);
                mockTarget2.Verify(x => x.HandleAsync(message, cancellationToken), Times.Once);
            }
        }


        [Fact]
        public async Task PublishOnBackgroundThreadAsync_CallsPublishAsyncWithBackgroundMarshaller()
        {
            var eventAggregator = new EventAggregator();
            var message = new object();
            using (var tokenSource = new CancellationTokenSource())
            {
                var cancellationToken = tokenSource.Token;
                var mockTarget1 = new Mock<IHandle<object>>();
                eventAggregator.SubscribeOnPublishedThread(mockTarget1.Object);


                await eventAggregator.PublishOnBackgroundThreadAsync(message, cancellationToken);

                mockTarget1.Verify(x => x.HandleAsync(message, cancellationToken), Times.Once);
            }
        }

        [Fact]
        public async Task PublishOnCurrentThreadAsync_CallsHandleAsyncOnAllSubscribedTargets()
        {
            var eventAggregator = new EventAggregator();
            string message = "Test";
            using (var tokenSource = new CancellationTokenSource())
            {
                var cancellationToken = tokenSource.Token;

                // Create 20 mock targets
                var mockTargets = new Mock<IHandle<string>>[NumberOfMockTargets];
                for (int i = 0; i < mockTargets.Length; i++)
                {
                    mockTargets[i] = new Mock<IHandle<string>>();
                    eventAggregator.SubscribeOnPublishedThread(mockTargets[i].Object);
                }

                await eventAggregator.PublishOnCurrentThreadAsync(message, cancellationToken);

                // Verify HandleAsync was called on each
                foreach (var mockTarget in mockTargets)
                {
                    mockTarget.Verify(x => x.HandleAsync(message, cancellationToken), Times.Once);
                }
            }
        }
    }
}
