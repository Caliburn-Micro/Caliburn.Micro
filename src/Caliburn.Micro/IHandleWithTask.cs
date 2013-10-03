namespace Caliburn.Micro {
    using System.Threading.Tasks;

    /// <summary>
    ///  Denotes a class which can handle a particular type of message and uses a Task to do so.
    /// </summary>
    public interface IHandleWithTask<TMessage> : IHandle { //don't use contravariance here
        /// <summary>
        ///  Handle the message with a Task.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The Task that represents the operation.</returns>
        Task Handle(TMessage message);
    }
}
