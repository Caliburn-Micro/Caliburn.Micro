namespace Caliburn.Micro {
    using System;

    public static class TaskExtensionMethods {
        public static void RequestTask<TTask>(this IEventAggregator events, Action<TTask> configure = null, object state = null)
            where TTask : new() {
            var task = new TTask();

            if(configure != null)
                configure(task);

            events.Publish(new TaskExecutionRequested
            {
                State = state,
                Task = task
            });
        }
    }
}