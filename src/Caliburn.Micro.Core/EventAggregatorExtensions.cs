using System;
using System.Net;
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
        /// Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />.
        /// </summary>
        /// <remarks>The subscription is invoked on the thread chosen by the publisher.</remarks>
        /// <param name="eventAggregator"></param>
        /// <param name = "subscriber">The instance to subscribe for event publication.</param>
        public static void SubscribeOnPublishedThread(this IEventAggregator eventAggregator, object subscriber)
        {
            eventAggregator.Subscribe(subscriber, f => f());
        }

        /// <summary>
        /// Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />.
        /// </summary>
        /// <remarks>The subscription is invoked on the thread chosen by the publisher.</remarks>
        /// <param name="eventAggregator"></param>
        /// <param name = "subscriber">The instance to subscribe for event publication.</param>
        [Obsolete("Use SubscribeOnPublishedThread")]
        public static void Subscribe(this IEventAggregator eventAggregator, object subscriber)
        {
            eventAggregator.SubscribeOnPublishedThread(subscriber);
        }

        /// <summary>
        /// Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />.
        /// </summary>
        /// <remarks>The subscription is invoked on a new background thread.</remarks>
        /// <param name="eventAggregator"></param>
        /// <param name = "subscriber">The instance to subscribe for event publication.</param>
        public static void SubscribeOnBackgroundThread(this IEventAggregator eventAggregator, object subscriber)
        {
            eventAggregator.Subscribe(subscriber, f => Task.Factory.StartNew(f, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default));
        }

        /// <summary>
        /// Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />.
        /// </summary>
        /// <remarks>The subscription is invoked on the UI thread.</remarks>
        /// <param name="eventAggregator"></param>
        /// <param name = "subscriber">The instance to subscribe for event publication.</param>
        public static void SubscribeOnUIThread(this IEventAggregator eventAggregator, object subscriber)
        {
            eventAggregator.Subscribe(subscriber, f =>
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

            });
        }

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
        /// Publishes a message on the current thread (synchrone).
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        public static Task PublishOnCurrentThreadAsync(this IEventAggregator eventAggregator, object message)
        {
            return eventAggregator.PublishOnCurrentThreadAsync(message, CancellationToken.None);
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
        /// Publishes a message on a background thread (async).
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        public static Task PublishOnBackgroundThreadAsync(this IEventAggregator eventAggregator, object message)
        {
            return eventAggregator.PublishOnBackgroundThreadAsync(message, CancellationToken.None);
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

        /// <summary>
        /// Publishes a message on the UI thread.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        public static Task PublishOnUIThreadAsync(this IEventAggregator eventAggregator, object message)
        {
            return eventAggregator.PublishOnUIThreadAsync(message, CancellationToken.None);
        }
    }
}
