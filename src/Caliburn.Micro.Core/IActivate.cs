using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes an instance which requires activation.
    /// </summary>
    public interface IActivate
    {
        ///<summary>
        /// Indicates whether or not this instance is active.
        ///</summary>
        bool IsActive { get; }

        /// <summary>
        /// Activates this instance.
        /// </summary>
        Task ActivateAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Raised after activation occurs.
        /// </summary>
        event EventHandler<ActivationEventArgs> Activated;
    }
}
