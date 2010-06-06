namespace Caliburn.Micro
{
    using System;

    public interface IResult
    {
        void Execute(ResultExecutionContext context);
        event EventHandler<ResultCompletionEventArgs> Completed;
    }
}