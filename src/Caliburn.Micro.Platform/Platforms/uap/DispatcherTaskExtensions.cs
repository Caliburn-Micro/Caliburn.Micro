using System;
using System.Threading.Tasks;

#if WinUI3
    using Windows.UI.Core;
#else
using Windows.UI.Core;
#endif

namespace Caliburn.Micro
{
    /// <summary>
    /// DispatcherTaskExtensions class.
    /// </summary>
    /// <remarks>
    /// Contains helper functions to run  tasks asynchronously on a <see cref="CoreDispatcher"/>.
    /// </remarks>
    public static class DispatcherTaskExtensions
    {
        /// <summary>
        /// Runs a task asynchronously on the specified <see cref="CoreDispatcher"/>.
        /// </summary>
        /// <typeparam name="T">The type of the result produced by the task.</typeparam>
        /// <param name="dispatcher">The dispatcher on which to run the task.</param>
        /// <param name="func">The function that returns the task to be run.</param>
        /// <param name="priority">The priority with which the task should be run.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task<T> RunTaskAsync<T>(this CoreDispatcher dispatcher,
            Func<Task<T>> func, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            await dispatcher.RunAsync(priority, async () =>
            {
                try
                {
                    taskCompletionSource.SetResult(await func());
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            });
            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Runs a task asynchronously on the specified <see cref="CoreDispatcher"/>.
        /// </summary>
        /// <param name="dispatcher">The dispatcher on which to run the task.</param>
        /// <param name="func">The function that returns the task to be run.</param>
        /// <param name="priority">The priority with which the task should be run.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// There is no <see cref="TaskCompletionSource{Void}"/> so a <see cref="bool"/> is used and discarded.
        /// </remarks>
        public static async Task RunTaskAsync(this CoreDispatcher dispatcher,
            Func<Task> func, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal) =>
            await RunTaskAsync(dispatcher, async () => { await func(); return false; }, priority);
    }
}
