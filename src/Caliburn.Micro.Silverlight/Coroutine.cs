namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Manages coroutine execution.
    /// </summary>
    public static class Coroutine {
        static readonly ILog Log = LogManager.GetLog(typeof(Coroutine));

        /// <summary>
        /// Creates the parent enumerator.
        /// </summary>
        public static Func<IEnumerator<IResult>, IResult> CreateParentEnumerator = inner => new SequentialResult(inner);

        /// <summary>
        /// Executes a coroutine.
        /// </summary>
        /// <param name="coroutine">The coroutine to execute.</param>
        public static void Execute(IEnumerator<IResult> coroutine) {
            Execute(coroutine, new ActionExecutionContext());
        }

        /// <summary>
        /// Executes a coroutine.
        /// </summary>
        /// <param name="coroutine">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        public static void Execute(IEnumerator<IResult> coroutine, ActionExecutionContext context) {
            Log.Info("Executing coroutine.");

            var enumerator = CreateParentEnumerator(coroutine);
            IoC.BuildUp(enumerator);

            enumerator.Completed += Completed;
            enumerator.Execute(context);
        }

        /// <summary>
        /// Called upon completion of a coroutine.
        /// </summary>
        public static event EventHandler<ResultCompletionEventArgs> Completed = (s, e) => {
            var enumerator = (IResult)s;
            enumerator.Completed -= Completed;

            if(e.Error != null)
                Log.Error(e.Error);
            else if(e.WasCancelled)
                Log.Info("Coroutine execution cancelled.");
            else
                Log.Info("Coroutine execution completed.");
        };
    }
}