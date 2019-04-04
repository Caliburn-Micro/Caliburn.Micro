using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.
    /// </summary>
    /// <remarks>Conducted instances can optin to the lifecycle by impelenting any of the follosing <see cref="IActivate"/>, <see cref="IDeactivate"/>, <see cref="IGuardClose"/>.</remarks>
    public interface IConductor : IParent, INotifyPropertyChangedEx
    {
        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
         /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task ActivateItemAsync(object item, CancellationToken cancellationToken);

        /// <summary>
        /// Deactivates the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeactivateItemAsync(object item, bool close, CancellationToken cancellationToken);

        /// <summary>
        /// Occurs when an activation request is processed.
        /// </summary>
        event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
    }

    /// <summary>
    /// An <see cref="IConductor"/> that also implements <see cref="IHaveActiveItem"/>.
    /// </summary>
    public interface IConductActiveItem : IConductor, IHaveActiveItem
    {
    }
}
