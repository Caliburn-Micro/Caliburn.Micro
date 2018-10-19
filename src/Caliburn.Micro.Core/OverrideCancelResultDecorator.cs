namespace Caliburn.Micro
{
    /// <summary>
    /// A result decorator that overrides <see cref="ResultCompletionEventArgs.WasCancelled"/> of the decorated <see cref="IResult"/> instance.
    /// </summary>
    public class OverrideCancelResultDecorator : ResultDecoratorBase
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(OverrideCancelResultDecorator));

        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideCancelResultDecorator"/> class.
        /// </summary>
        /// <param name="result">The result to decorate.</param>
        public OverrideCancelResultDecorator(IResult result)
            : base(result) { }

        /// <summary>
        /// Called when the execution of the decorated result has completed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="innerResult">The decorated result.</param>
        /// <param name="args">The <see cref="ResultCompletionEventArgs" /> instance containing the event data.</param>
        protected override void OnInnerResultCompleted(CoroutineExecutionContext context, IResult innerResult, ResultCompletionEventArgs args)
        {
            if (args.WasCancelled)
            {
                Log.Info(string.Format("Overriding WasCancelled from {0}.", innerResult.GetType().Name));
            }

            OnCompleted(new ResultCompletionEventArgs { Error = args.Error });
        }
    }
}
