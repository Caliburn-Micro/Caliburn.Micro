using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// An implementation of <see cref="IConductor"/> that holds on to and activates only one item at a time.
    /// </summary>
    public partial class Conductor<T> : ConductorBaseWithActiveItem<T> where T : class
    {
        /// <inheritdoc />
        public override async Task ActivateItemAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item != null && item.Equals(ActiveItem))
            {
                if (!IsActive)
                {
                    return;
                }

                await ScreenExtensions.TryActivateAsync(item, cancellationToken);
                OnActivationProcessed(item, true);

                return;
            }

            ICloseResult<T> closeResult = await CloseStrategy.ExecuteAsync(new[] { ActiveItem }, cancellationToken);
            if (!closeResult.CloseCanOccur)
            {
                OnActivationProcessed(item, false);

                return;
            }

            await ChangeActiveItemAsync(item, true, cancellationToken);
        }

        /// <summary>
        /// Deactivates the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async Task DeactivateItemAsync(T item, bool close, CancellationToken cancellationToken = default)
        {
            if (item == null || !item.Equals(ActiveItem))
            {
                return;
            }

            ICloseResult<T> closeResult = await CloseStrategy.ExecuteAsync(new[] { ActiveItem }, CancellationToken.None);
            if (!closeResult.CloseCanOccur)
            {
                return;
            }
            
            await ChangeActiveItemAsync(default, close, cancellationToken);
        }

        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
        {
            ICloseResult<T> closeResult = await CloseStrategy.ExecuteAsync(new[] { ActiveItem }, cancellationToken);

            return closeResult.CloseCanOccur;
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
            => ScreenExtensions.TryActivateAsync(ActiveItem, cancellationToken);

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name="close">Indicates whether this instance will be closed.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
            => ScreenExtensions.TryDeactivateAsync(ActiveItem, close, cancellationToken);

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>The collection of children.</returns>
        public override IEnumerable<T> GetChildren()
            => new[] { ActiveItem };
    }
}
