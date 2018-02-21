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
        /// <param name="cancellationToken"></param>
        protected virtual async Task ChangeActiveItemAsync(T newItem, bool closePrevious, CancellationToken cancellationToken)
        {
            ScreenExtensions.TryDeactivate(_activeItem, closePrevious);

            newItem = EnsureItem(newItem);

            if (IsActive)
                await ScreenExtensions.TryActivateAsync(newItem, cancellationToken);

            _activeItem = newItem;
            NotifyOfPropertyChange();
            OnActivationProcessed(_activeItem, true);
        }

        protected Task ChangeActiveItemAsync(T newItem, bool closePrevious) => ChangeActiveItemAsync(newItem, closePrevious, CancellationToken.None);
    }
}
