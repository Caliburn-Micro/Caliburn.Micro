using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro;

/// <summary>
/// Denotes an instance which requires activation.
/// </summary>
public interface IActivate {
    /// <summary>
    /// Raised after activation occurs.
    /// </summary>
    event AsyncEventHandler<ActivationEventArgs> Activated;

    /// <summary>
    /// Gets a value indicating whether or not this instance is active.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Activates this instance.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ActivateAsync(CancellationToken cancellationToken = default);
}
