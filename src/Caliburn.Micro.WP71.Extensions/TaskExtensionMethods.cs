namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Extension methods related to phone tasks.
    /// </summary>
    public static class TaskExtensionMethods {
        /// <summary>
        /// Creates a task and publishes it using the <see cref="EventAggregator"/>.
        /// </summary>
        /// <typeparam name="TTask">The task to create.</typeparam>
        /// <param name="events">The event aggregator.</param>
        /// <param name="configure">Optional configuration for the task.</param>
        /// <param name="state">Optional state to be passed along to the task completion message.</param>
        public static void RequestTask<TTask>(this IEventAggregator events, Action<TTask> configure = null, object state = null)
            where TTask : new() {
            var task = new TTask();

            if(configure != null) {
                configure(task);
            }

            events.Publish(new TaskExecutionRequested {
                State = state,
                Task = task
            });
        }
    }
}