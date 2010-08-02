namespace Caliburn.Micro
{
    using Microsoft.Phone.Tasks;

    /// <summary>
    /// Implemented by classes that request the execution of a chooser.
    /// </summary>
    /// <typeparam name="TResult">The result that is returned from the chooser.</typeparam>
    public interface ILaunchChooser<TResult> : ILaunchTask
        where TResult : TaskEventArgs
    {
        /// <summary>
        /// Called to handle the result returned by the chooser.
        /// </summary>
        /// <param name="result">The chooser's result.</param>
        void Handle(TResult result);
    }
}