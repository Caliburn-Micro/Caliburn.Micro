namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name="TMessage">The type of message to handle.</typeparam>
#if WP7
    public interface IHandle<TMessage>
#else
    public interface IHandle<in TMessage>
#endif
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(TMessage message);
    }
}