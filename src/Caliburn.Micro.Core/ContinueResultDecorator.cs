using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// A result decorator which executes a coroutine when the wrapped result was cancelled.
    /// </summary>
    public class ContinueResultDecorator : ResultDecoratorBase
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(ContinueResultDecorator));
        private readonly Func<IResult> coroutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueResultDecorator"/> class.
        /// </summary>
        /// <param name="result">The result to decorate.</param>
        /// <param name="coroutine">The coroutine to execute when <paramref name="result"/> was canceled.</param>
        public ContinueResultDecorator(IResult result, Func<IResult> coroutine)
            : base(result)
        {
            this.coroutine = coroutine ?? throw new ArgumentNullException(nameof(coroutine));
        }

        /// <summary>
        /// Called when the execution of the decorated result has completed.
        /// </summary>
        /// <param name="methodContext">The context.</param>
        /// <param name="methodInnerResult">The decorated result.</param>
        /// <param name="args">The <see cref="ResultCompletionEventArgs" /> instance containing the event data.</param>
        protected override void OnInnerResultCompleted(CoroutineExecutionContext methodContext, IResult methodInnerResult, ResultCompletionEventArgs args)
        {
            if (args.Error != null || !args.WasCancelled)
            {
                OnCompleted(new ResultCompletionEventArgs { Error = args.Error });
            }
            else
            {
                Log.Info(string.Format("Executing coroutine because {0} was cancelled.", methodInnerResult.GetType().Name));
                Continue(methodContext);
            }
        }

        private void Continue(CoroutineExecutionContext context)
        {
            IResult continueResult;
            try
            {
                continueResult = coroutine();
            }
            catch (Exception ex)
            {
                OnCompleted(new ResultCompletionEventArgs { Error = ex });
                return;
            }

            try
            {
                continueResult.Completed += ContinueCompleted;
                IoC.BuildUp(continueResult);
                continueResult.Execute(context);
            }
            catch (Exception ex)
            {
                ContinueCompleted(continueResult, new ResultCompletionEventArgs { Error = ex });
            }
        }

        private void ContinueCompleted(object sender, ResultCompletionEventArgs args)
        {
            ((IResult)sender).Completed -= ContinueCompleted;
            OnCompleted(new ResultCompletionEventArgs { Error = args.Error, WasCancelled = (args.Error == null) });
        }
    }
}
