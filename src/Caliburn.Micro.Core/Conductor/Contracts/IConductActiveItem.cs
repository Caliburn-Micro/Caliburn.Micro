using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// An <see cref="IConductor"/> that also implements <see cref="IHaveActiveItem"/>.
    /// </summary>
    public interface IConductActiveItem : IConductor, IHaveActiveItem
    {
    }
}
