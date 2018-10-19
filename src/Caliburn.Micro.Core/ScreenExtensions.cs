using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// Hosts extension methods for <see cref="IScreen"/> classes.
    /// </summary>
    public static class ScreenExtensions
    {
        /// <summary>
        /// Activates the item if it implements <see cref="IActivate"/>, otherwise does nothing.
        /// </summary>
        /// <param name="potentialActivatable">The potential activatable.</param>
        public static Task TryActivateAsync(object potentialActivatable)
        {
            return potentialActivatable is IActivate activator ? activator.ActivateAsync(CancellationToken.None) : Task.FromResult(true);
        }

        /// <summary>
        /// Activates the item if it implements <see cref="IActivate"/>, otherwise does nothing.
        /// </summary>
        /// <param name="potentialActivatable">The potential activatable.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task TryActivateAsync(object potentialActivatable, CancellationToken cancellationToken)
        {
            return potentialActivatable is IActivate activator ? activator.ActivateAsync(cancellationToken) : Task.FromResult(true);
        }

        /// <summary>
        /// Deactivates the item if it implements <see cref="IDeactivate"/>, otherwise does nothing.
        /// </summary>
        /// <param name="potentialDeactivatable">The potential deactivatable.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task TryDeactivateAsync(object potentialDeactivatable, bool close, CancellationToken cancellationToken)
        {
            return potentialDeactivatable is IDeactivate deactivator ? deactivator.DeactivateAsync(close, cancellationToken): Task.FromResult(true);
        }

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="conductor">The conductor.</param>
        /// <param name="item">The item to close.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task CloseItemAsync(this IConductor conductor, object item, CancellationToken cancellationToken)
        {
            return conductor.DeactivateItemAsync(item, true, cancellationToken);
        }

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="conductor">The conductor.</param>
        /// <param name="item">The item to close.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static Task CloseItemAsync<T>(this ConductorBase<T> conductor, T item, CancellationToken cancellationToken) where T : class
        {
            return conductor.DeactivateItemAsync(item, true, cancellationToken);
        }

        ///<summary>
        /// Activates a child whenever the specified parent is activated.
        ///</summary>
        ///<param name="child">The child to activate.</param>
        ///<param name="parent">The parent whose activation triggers the child's activation.</param>
        public static void ActivateWith(this IActivate child, IActivate parent)
        {
            var childReference = new WeakReference(child);

            void OnParentActivated(object s, ActivationEventArgs e)
            {
                var activatable = (IActivate)childReference.Target;
                if (activatable == null)
                    ((IActivate)s).Activated -= OnParentActivated;
                else
                    activatable.ActivateAsync(CancellationToken.None);
            }

            parent.Activated += OnParentActivated;
        }

        ///<summary>
        /// Deactivates a child whenever the specified parent is deactivated.
        ///</summary>
        ///<param name="child">The child to deactivate.</param>
        ///<param name="parent">The parent whose deactivation triggers the child's deactivation.</param>
        public static void DeactivateWith(this IDeactivate child, IDeactivate parent)
        {
            var childReference = new WeakReference(child);
            EventHandler<DeactivationEventArgs> handler = null;
            handler = (s, e) =>
            {
                var deactivatable = (IDeactivate)childReference.Target;
                if (deactivatable == null)
                    ((IDeactivate)s).Deactivated -= handler;
                else
                    deactivatable.DeactivateAsync(e.WasClosed, CancellationToken.None);
            };
            parent.Deactivated += handler;
        }

        ///<summary>
        /// Activates and Deactivates a child whenever the specified parent is Activated or Deactivated.
        ///</summary>
        ///<param name="child">The child to activate/deactivate.</param>
        ///<param name="parent">The parent whose activation/deactivation triggers the child's activation/deactivation.</param>
        public static void ConductWith<TChild, TParent>(this TChild child, TParent parent)
            where TChild : IActivate, IDeactivate
            where TParent : IActivate, IDeactivate
        {
            child.ActivateWith(parent);
            child.DeactivateWith(parent);
        }
    }
}
