using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name = "TMessage">The type of message to handle.</typeparam>
    public interface IHandle<in TMessage>
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}
