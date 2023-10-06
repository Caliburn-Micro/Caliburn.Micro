using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods for the <see cref="IConductor"/> instance.
    /// </summary>
    public static class ConductorExtensions
    {
        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="conductor">The conductor to activate the item with.</param>
        /// <param name="item">The item to activate.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task ActivateItemAsync(this IConductor conductor, object item) => conductor.ActivateItemAsync(item, default);
    }
}
