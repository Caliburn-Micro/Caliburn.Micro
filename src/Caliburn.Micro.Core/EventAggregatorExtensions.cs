using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extensions for <see cref="IEventAggregator"/>.
    /// </summary>
    public static class EventAggregatorExtensions
    {
        /// <summary>
        /// Publishes a message on the current thread (synchrone).
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public static Task PublishOnCurrentThreadAsync(this IEventAggregator eventAggregator, object message, CancellationToken cancellationToken)
        {
            return eventAggregator.PublishAsync(message, f => f(), cancellationToken);
        }

        /// <summary>
        /// Publishes a message on a background thread (async).
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public static Task PublishOnBackgroundThreadAsync(this IEventAggregator eventAggregator, object message, CancellationToken cancellationToken)
        {
            return eventAggregator.PublishAsync(message, f => Task.Factory.StartNew(f, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default), cancellationToken);
        }

        /// <summary>
        /// Publishes a message on the UI thread.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public static Task PublishOnUIThreadAsync(this IEventAggregator eventAggregator, object message, CancellationToken cancellationToken)
        {
            return eventAggregator.PublishAsync(message, f =>
            {
                var taskCompletionSource = new TaskCompletionSource<bool>();

                Execute.BeginOnUIThread(async () =>
                {
                    try
                    {
                        await f();

                        taskCompletionSource.SetResult(true);
                    }
                    catch (OperationCanceledException)
                    {
                        taskCompletionSource.SetCanceled();
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.SetException(ex);
                    }
                });

                return taskCompletionSource.Task;

            }, cancellationToken);
        }
    }
}
