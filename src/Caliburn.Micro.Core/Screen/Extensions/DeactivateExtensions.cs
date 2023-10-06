using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods for the <see cref="IDeactivate"/> instance.
    /// </summary>
    public static class DeactivateExtensions
    {
        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        /// <param name="deactivate">The instance to deactivate</param>
        /// <param name="close">Indicates whether or not this instance is being closed.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task DeactivateAsync(this IDeactivate deactivate, bool close) => deactivate.DeactivateAsync(close, default);
    }
}
