using System;
using System.Threading.Tasks;

using Windows.UI.Core;

namespace Caliburn.Micro {
    /// <summary>
    /// CoreDispatcher Extension methods.
    /// </summary>
    public static class DispatcherTaskExtensions {
        /// <summary>
        /// Run task async and get the result.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="dispatcher">Core Dispatcher.</param>
        /// <param name="func">Func to get the result.</param>
        /// <param name="priority">Priority of the task.</param>
        /// <returns>Task with the result.</returns>
        public static async Task<T> RunTaskAsync<T>(
            this CoreDispatcher dispatcher,
            Func<Task<T>> func,
            CoreDispatcherPriority priority = CoreDispatcherPriority.Normal) {
            var taskCompletionSource = new TaskCompletionSource<T>();
            await dispatcher.RunAsync(priority, async () => {
                try {
                    taskCompletionSource.SetResult(await func());
                } catch (Exception ex) {
                    taskCompletionSource.SetException(ex);
                }
            });

            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Run task async.
        /// There is no TaskCompletionSource&lt;void&gt; so we use a bool that we throw away.
        /// </summary>
        /// <param name="dispatcher">Core Dispatcher.</param>
        /// <param name="func">Func to get the result.</param>
        /// <param name="priority">Priority of the task.</param>
        /// <returns>Task.</returns>
        public static async Task RunTaskAsync(
            this CoreDispatcher dispatcher,
            Func<Task> func,
            CoreDispatcherPriority priority = CoreDispatcherPriority.Normal) =>
            await RunTaskAsync(
                dispatcher,
                async () => {
                    await func();

                    return false;
                },
                priority);
    }
}
