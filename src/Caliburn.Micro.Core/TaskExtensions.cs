using System;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods to bring <see cref="Task"/> and <see cref="IResult"/> together.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Executes an <see cref="IResult"/> asynchronous.
        /// </summary>
        /// <param name="result">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task ExecuteAsync(this IResult result, CoroutineExecutionContext context = null)
        {
            return InternalExecuteAsync<object>(result, context);
        }

        /// <summary>
        /// Executes an <see cref="IResult&lt;TResult&gt;"/> asynchronous.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task<TResult> ExecuteAsync<TResult>(this IResult<TResult> result,
                                                          CoroutineExecutionContext context = null)
        {
            return InternalExecuteAsync<TResult>(result, context);
        }

        private static Task<TResult> InternalExecuteAsync<TResult>(IResult result, CoroutineExecutionContext context)
        {
            var taskSource = new TaskCompletionSource<TResult>();

            void OnCompleted(object s, ResultCompletionEventArgs e)
            {
                result.Completed -= OnCompleted;

                if (e.Error != null)
                {
                    taskSource.SetException(e.Error);
                }
                else if (e.WasCancelled)
                {
                    taskSource.SetCanceled();
                }
                else
                {
                    taskSource.SetResult(result is IResult<TResult> rr ? rr.Result : default);
                }
            }

            try
            {
                IoC.BuildUp(result);
                result.Completed += OnCompleted;
                result.Execute(context ?? new CoroutineExecutionContext());
            }
            catch (Exception ex)
            {
                result.Completed -= OnCompleted;
                taskSource.SetException(ex);
            }

            return taskSource.Task;
        }

        /// <summary>
        /// Encapsulates a task inside a coroutine.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The coroutine that encapsulates the task.</returns>
        public static TaskResult AsResult(this Task task)
        {
            return new TaskResult(task);
        }

        /// <summary>
        /// Encapsulates a task inside a coroutine.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>The coroutine that encapsulates the task.</returns>
        public static TaskResult<TResult> AsResult<TResult>(this Task<TResult> task)
        {
            return new TaskResult<TResult>(task);
        }
    }
}
