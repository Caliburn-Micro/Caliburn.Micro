using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods for the <see cref="IActivate"/> instance.
    /// </summary>
    public static class ActivateExtensions
    {
        /// <summary>
        /// Activates this instance.
        /// </summary>
        /// <param name="activate">The instance to activate</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task ActivateAsync(this IActivate activate) => activate.ActivateAsync(default);
    }
}
