namespace Caliburn.Micro {
    using System.Collections.Generic;

    /// <summary>
    ///  Denotes a class which can handle a particular type of message and uses a Coroutine to do so.
    /// </summary>
    public interface IHandleWithCoroutine<TMessage> : IHandle {  //don't use contravariance here
        /// <summary>
        ///  Handle the message with a Coroutine.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The coroutine to execute.</returns>
        IEnumerable<IResult> Handle(TMessage message);
    }
}
