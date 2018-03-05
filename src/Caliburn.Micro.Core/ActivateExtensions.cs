using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public static class ActivateExtensions
    {
        public static Task ActivateAsync(this IActivate activate) => activate.ActivateAsync(CancellationToken.None);
    }
}
