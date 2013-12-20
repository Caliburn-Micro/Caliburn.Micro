namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// A result decorator which rescues errors from the decorated result by executing a rescue coroutine.
    /// </summary>
    /// <typeparam name="TException">The type of the exception we want to perform the rescue on</typeparam>
    public class RescueResultDecorator<TException> : ResultDecoratorBase where TException : Exception {
        static readonly ILog Log = LogManager.GetLog(typeof(RescueResultDecorator<>));
        readonly bool cancelResult;
        readonly Func<TException, IResult> coroutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="RescueResultDecorator&lt;TException&gt;"/> class.
        /// </summary>
        /// <param name="result">The result to decorate.</param>
        /// <param name="coroutine">The rescue coroutine.</param>
        /// <param name="cancelResult">Set to true to cancel the result after executing rescue.</param>
        public RescueResultDecorator(IResult result, Func<TException, IResult> coroutine, bool cancelResult = true) : base(result) {
            if (coroutine == null)
                throw new ArgumentNullException("coroutine");

            this.coroutine = coroutine;
            this.cancelResult = cancelResult;
        }

        /// <summary>
        /// Called when the execution of the decorated result has completed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="innerResult">The decorated result.</param>
        /// <param name="args">The <see cref="ResultCompletionEventArgs" /> instance containing the event data.</param>
        protected override void OnInnerResultCompleted(CoroutineExecutionContext context, IResult innerResult, ResultCompletionEventArgs args) {
            var error = args.Error as TException;
            if (error == null) {
                OnCompleted(args);
            }
            else {
                Log.Error(error);
                Log.Info(string.Format("Executing coroutine because {0} threw an exception.", innerResult.GetType().Name));
                Rescue(context, error);
            }
        }

        void Rescue(CoroutineExecutionContext context, TException exception) {
            IResult rescueResult;
            try {
                rescueResult = coroutine(exception);
            }
            catch (Exception ex) {
                OnCompleted(new ResultCompletionEventArgs {Error = ex});
                return;
            }

            try {
                rescueResult.Completed += RescueCompleted;
                IoC.BuildUp(rescueResult);
                rescueResult.Execute(context);
            }
            catch (Exception ex) {
                RescueCompleted(rescueResult, new ResultCompletionEventArgs {Error = ex});
            }
        }

        void RescueCompleted(object sender, ResultCompletionEventArgs args) {
            ((IResult)sender).Completed -= RescueCompleted;
            OnCompleted(new ResultCompletionEventArgs
                {
                    Error = args.Error,
                    WasCancelled = (args.Error == null && (args.WasCancelled || cancelResult))
                });
        }
    }
}
