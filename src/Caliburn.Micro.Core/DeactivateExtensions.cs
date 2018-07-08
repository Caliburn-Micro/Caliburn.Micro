using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public static class DeactivateExtensions
    {
        public static Task DeactivateAsync(this IDeactivate deactivate, bool close) => deactivate.DeactivateAsync(close, CancellationToken.None);
    }
}
