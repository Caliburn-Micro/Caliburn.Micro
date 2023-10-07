using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro;

/// <summary>
/// Used to gather the results from multiple child elements which may or may not prevent closing.
/// </summary>
/// <typeparam name="T">The type of child element.</typeparam>
public class DefaultCloseStrategy<T> : ICloseStrategy<T> {
    private readonly bool _closeConductedItemsWhenConductorCannotClose;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultCloseStrategy{T}"/> class.
    /// </summary>
    /// <param name="closeConductedItemsWhenConductorCannotClose">Indicates that even if all conducted items are not closable, those that are should be closed. The default is FALSE.</param>
    public DefaultCloseStrategy(bool closeConductedItemsWhenConductorCannotClose = false)
        => _closeConductedItemsWhenConductorCannotClose = closeConductedItemsWhenConductorCannotClose;

    /// <inheritdoc />
    public async Task<ICloseResult<T>> ExecuteAsync(IEnumerable<T> toClose, CancellationToken cancellationToken = default) {
        var closeable = new List<T>();
        bool closeCanOccur = true;
        foreach (T child in toClose) {
            if (child is not IGuardClose guard) {
                closeable.Add(child);
                continue;
            }

            bool canClose = await guard.CanCloseAsync(cancellationToken);
            if (canClose) {
                closeable.Add(child);
            }

            closeCanOccur = closeCanOccur && canClose;
        }

        if (!_closeConductedItemsWhenConductorCannotClose && !closeCanOccur) {
            closeable.Clear();
        }

        return new CloseResult<T>(closeCanOccur, closeable);
    }
}
