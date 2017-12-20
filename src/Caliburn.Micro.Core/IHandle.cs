using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// A marker interface for classes that subscribe to messages.
    /// </summary>
    public interface IHandle
    {
    }

    /// <summary>
    /// Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name = "TMessage">The type of message to handle.</typeparam>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IHandle<TMessage> : IHandle
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}
