namespace Caliburn.Micro {
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods to bring <see cref="System.Threading.Tasks.Task"/> and <see cref="Caliburn.Micro.IResult"/> together.
    /// </summary>
    public static class TaskExtensions {
        /// <summary>
        /// Executes an <see cref="Caliburn.Micro.IResult"/> asynchronous.
        /// </summary>
        /// <param name="result">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task ExecuteAsync(this IResult result, CoroutineExecutionContext context = null) {
            return InternalExecuteAsync<object>(result, context);
        }

        /// <summary>
        /// Executes an <see cref="Caliburn.Micro.IResult&lt;TResult&gt;"/> asynchronous.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task<TResult> ExecuteAsync<TResult>(this IResult<TResult> result,
                                                          CoroutineExecutionContext context = null) {
            return InternalExecuteAsync<TResult>(result, context);
        }

        static Task<TResult> InternalExecuteAsync<TResult>(IResult result, CoroutineExecutionContext context) {
            var taskSource = new TaskCompletionSource<TResult>();

            EventHandler<ResultCompletionEventArgs> completed = null;
            completed = (s, e) => {
                result.Completed -= completed;

                if (e.Error != null)
                    taskSource.SetException(e.Error);
                else if (e.WasCancelled)
                    taskSource.SetCanceled();
                else {
                    var rr = result as IResult<TResult>;
                    taskSource.SetResult(rr != null ? rr.Result : default(TResult));
                }
            };

            try {
                IoC.BuildUp(result);
                result.Completed += completed;
                result.Execute(context ?? new CoroutineExecutionContext());
            }
            catch (Exception ex) {
                result.Completed -= completed;
                taskSource.SetException(ex);
            }

            return taskSource.Task;
        }

        /// <summary>
        /// Encapsulates a task inside a couroutine.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The coroutine that encapsulates the task.</returns>
        public static TaskResult AsResult(this Task task) {
            return new TaskResult(task);
        }

        /// <summary>
        /// Encapsulates a task inside a couroutine.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>The coroutine that encapsulates the task.</returns>
        public static TaskResult<TResult> AsResult<TResult>(this Task<TResult> task) {
            return new TaskResult<TResult>(task);
        }
    }
}
