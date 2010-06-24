namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name="T">The type of message to handle.</typeparam>
    public interface IHandle<in T>
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(T message);
    }
}