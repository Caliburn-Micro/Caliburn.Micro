using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// A base class for various implementations of <see cref="IConductor"/> that maintain an active item.
    /// </summary>
    /// <typeparam name="T">The type that is being conducted.</typeparam>
    public abstract class ConductorBaseWithActiveItem<T> : ConductorBase<T>, IConductActiveItem where T : class
    {
        private T _activeItem;

        /// <summary>
        /// The currently active item.
        /// </summary>
        public T ActiveItem
        {
            get => _activeItem;
            set => ActivateItemAsync(value, CancellationToken.None);
        }

        /// <summary>
        /// The currently active item.
        /// </summary>
        /// <value></value>
        object IHaveActiveItem.ActiveItem
        {
            get => ActiveItem;
            set => ActiveItem = (T)value;
        }

        /// <summary>
        /// Changes the active item.
        /// </summary>
        /// <param name="newItem">The new item to activate.</param>
        /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected virtual async Task ChangeActiveItemAsync(T newItem, bool closePrevious, CancellationToken cancellationToken)
        {
            await ScreenExtensions.TryDeactivateAsync(_activeItem, closePrevious, cancellationToken);
            var oldItem = _activeItem;
            newItem = EnsureItem(newItem);

            _activeItem = newItem;
            NotifyOfPropertyChange(nameof(ActiveItem));

            if (IsActive)
                await ScreenExtensions.TryActivateAsync(newItem, cancellationToken);

            await DeactivateItemAsync(oldItem, closePrevious, cancellationToken);

            OnActivationProcessed(_activeItem, true);
        }

        /// <summary>
        /// Changes the active item.
        /// </summary>
        /// <param name="newItem">The new item to activate.</param>
        /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected Task ChangeActiveItemAsync(T newItem, bool closePrevious) => ChangeActiveItemAsync(newItem, closePrevious, default);
    }
}
