using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes an instance which may prevent closing.
    /// </summary>
    public interface IGuardClose : IClose
    {
        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="callback">The implementer calls this action with the result of the close check.</param>
        Task<bool> CanCloseAsync(CancellationToken cancellationToken);
    }
}
