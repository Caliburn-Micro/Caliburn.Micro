using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// Allows custom code to execute after the return of a action.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IResult<out TResult> : IResult
    {
        /// <summary>
        /// Gets the result of the asynchronous operation.
        /// </summary>
        TResult Result { get; }
    }
}
