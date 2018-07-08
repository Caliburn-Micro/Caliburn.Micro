using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// A base class for various implementations of <see cref="IConductor"/>.
    /// </summary>
    /// <typeparam name="T">The type that is being conducted.</typeparam>
    public abstract class ConductorBase<T> : Screen, IConductor, IParent<T> where T : class
    {
        private ICloseStrategy<T> _closeStrategy;

        /// <summary>
        /// Gets or sets the close strategy.
        /// </summary>
        /// <value>The close strategy.</value>
        public ICloseStrategy<T> CloseStrategy
        {
            get => _closeStrategy ?? (_closeStrategy = new DefaultCloseStrategy<T>());
            set => _closeStrategy = value;
        }

        Task IConductor.DeactivateItemAsync(object item, bool close, CancellationToken cancellationToken)
        {
            return DeactivateItemAsync((T)item, close, cancellationToken);
        }

        IEnumerable IParent.GetChildren()
        {
            return GetChildren();
        }

        /// <summary>
        /// Occurs when an activation request is processed.
        /// </summary>
        public virtual event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = delegate { };

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>The collection of children.</returns>
        public abstract IEnumerable<T> GetChildren();

        Task IConductor.ActivateItemAsync(object item, CancellationToken cancellationToken)
        {
            return ActivateItemAsync((T)item, cancellationToken);
        }

        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public abstract Task ActivateItemAsync(T item, CancellationToken cancellationToken);

        /// <summary>
        /// Deactivates the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        public abstract Task DeactivateItemAsync(T item, bool close, CancellationToken cancellationToken);

        /// <summary>
        /// Called by a subclass when an activation needs processing.
        /// </summary>
        /// <param name="item">The item on which activation was attempted.</param>
        /// <param name="success">if set to <c>true</c> activation was successful.</param>
        protected virtual void OnActivationProcessed(T item, bool success)
        {
            if (item == null)
                return;

            ActivationProcessed?.Invoke(this, new ActivationProcessedEventArgs
            {
                Item = item,
                Success = success
            });
        }

        /// <summary>
        /// Ensures that an item is ready to be activated.
        /// </summary>
        /// <param name="newItem">The item that is about to be activated.</param>
        /// <returns>The item to be activated.</returns>
        protected virtual T EnsureItem(T newItem)
        {
            if (newItem is IChild node && node.Parent != this)
                node.Parent = this;

            return newItem;
        }
    }
}
