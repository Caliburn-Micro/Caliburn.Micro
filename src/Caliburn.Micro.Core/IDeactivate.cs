using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes an instance which requires deactivation.
    /// </summary>
    public interface IDeactivate
    {
        /// <summary>
        /// Raised before deactivation.
        /// </summary>
        event EventHandler<DeactivationEventArgs> AttemptingDeactivation;

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        /// <param name="close">Indicates whether or not this instance is being closed.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeactivateAsync(bool close, CancellationToken cancellationToken);

        /// <summary>
        /// Raised after deactivation.
        /// </summary>
        event EventHandler<DeactivationEventArgs> Deactivated;
    }
}
