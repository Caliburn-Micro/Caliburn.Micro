using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// Base class for all <see cref="IResult"/> decorators.
    /// </summary>
    public abstract class ResultDecoratorBase : IResult
    {
        private readonly IResult innerResult;
        private CoroutineExecutionContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDecoratorBase"/> class.
        /// </summary>
        /// <param name="result">The result to decorate.</param>
        protected ResultDecoratorBase(IResult result)
        {
            innerResult = result ?? throw new ArgumentNullException("result");
        }

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            this.context = context;

            try
            {
                innerResult.Completed += InnerResultCompleted;
                IoC.BuildUp(innerResult);
                innerResult.Execute(this.context);
            }
            catch (Exception ex)
            {
                InnerResultCompleted(innerResult, new ResultCompletionEventArgs { Error = ex });
            }
        }

        private void InnerResultCompleted(object sender, ResultCompletionEventArgs args)
        {
            innerResult.Completed -= InnerResultCompleted;
            OnInnerResultCompleted(context, innerResult, args);
            context = null;
        }

        /// <summary>
        /// Called when the execution of the decorated result has completed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="innerResult">The decorated result.</param>
        /// <param name="args">The <see cref="ResultCompletionEventArgs"/> instance containing the event data.</param>
        protected abstract void OnInnerResultCompleted(CoroutineExecutionContext context, IResult innerResult, ResultCompletionEventArgs args);

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        /// <summary>
        /// Raises the <see cref="Completed" /> event.
        /// </summary>
        /// <param name="args">The <see cref="ResultCompletionEventArgs"/> instance containing the event data.</param>
        protected void OnCompleted(ResultCompletionEventArgs args)
        {
            Completed(this, args);
        }
    }
}
