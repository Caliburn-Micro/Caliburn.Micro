using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public static class ConductorExtensions
    {
        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="conductor">The conductor to activate the item with.</param>
        /// <param name="item">The item to activate.</param>
        public static Task ActivateItemAsync(this IConductor conductor, object item) => conductor.ActivateItemAsync(item, CancellationToken.None);
    }
}
