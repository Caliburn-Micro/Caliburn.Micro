using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Manages coroutine execution.
    /// </summary>
    public static class Coroutine
    {
        private static readonly ILog Log
            = LogManager.GetLog(typeof(Coroutine));

        /// <summary>
        /// Creates the parent enumerator.
        /// </summary>
        public static Func<IEnumerator<IResult>, IResult> CreateParentEnumerator
            = inner => new SequentialResult(inner);

        /// <summary>
        /// Executes a coroutine.
        /// </summary>
        /// <param name="coroutine">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// /// <param name="callback">The completion callback for the coroutine.</param>
        public static void BeginExecute(
            IEnumerator<IResult> coroutine,
            CoroutineExecutionContext context = null,
            EventHandler<ResultCompletionEventArgs> callback = null)
        {
            Log.Info("Executing coroutine.");

            IResult enumerator = CreateParentEnumerator(coroutine);
            IoC.BuildUp(enumerator);

            if (callback != null)
            {
                ExecuteOnCompleted(enumerator, callback);
            }

            ExecuteOnCompleted(enumerator, Completed);
            enumerator.Execute(context ?? new CoroutineExecutionContext());
        }

        /// <summary>
        /// Executes a coroutine asynchronous.
        /// </summary>
        /// <param name="coroutine">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task ExecuteAsync(
            IEnumerator<IResult> coroutine,
            CoroutineExecutionContext context = null)
        {
            var taskSource = new TaskCompletionSource<object>();

            BeginExecute(
                coroutine,
                context,
                (s, e) =>
                {
                    if (e.Error != null)
                    {
                        taskSource.SetException(e.Error);

                        return;
                    }

                    if (e.WasCancelled)
                    {
                        taskSource.SetCanceled();

                        return;
                    }

                    taskSource.SetResult(null);
                });

            return taskSource.Task;
        }

        private static void ExecuteOnCompleted(
            IResult result,
            EventHandler<ResultCompletionEventArgs> handler)
        {
            void OnCompledted(object s, ResultCompletionEventArgs e)
            {
                result.Completed -= OnCompledted;
                handler(s, e);
            }

            result.Completed += OnCompledted;
        }

        /// <summary>
        /// Called upon completion of a coroutine.
        /// </summary>
        public static event EventHandler<ResultCompletionEventArgs> Completed
            = (s, e) =>
            {
                if (e.Error != null)
                {
                    Log.Error(e.Error);

                    return;
                }

                if (e.WasCancelled)
                {
                    Log.Info("Coroutine execution cancelled.");

                    return;
                }

                Log.Info("Coroutine execution completed.");
            };
    }
}
