namespace Caliburn.Micro
{
    /// <summary>
    /// A marker interface for classes that subscribe to messages.
    /// </summary>
    public interface IHandle{}

    /// <summary>
    /// Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name="TMessage">The type of message to handle.</typeparam>
#if WP7
    public interface IHandle<TMessage> : IHandle
#else
    public interface IHandle<in TMessage> : IHandle
#endif
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(TMessage message);
    }
}