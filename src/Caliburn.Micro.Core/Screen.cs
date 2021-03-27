using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// A base implementation of <see cref = "IScreen" />.
    /// </summary>
    public class Screen : ViewAware, IScreen, IChild
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(Screen));
        private string _displayName;

        private bool _isActive;
        private bool _isInitialized;
        private object _parent;

        /// <summary>
        /// Creates an instance of the screen.
        /// </summary>
        public Screen()
        {
            _displayName = GetType().FullName;
        }

        /// <summary>
        /// Indicates whether or not this instance is currently initialized.
        /// Virtualized in order to help with document oriented view models.
        /// </summary>
        public virtual bool IsInitialized
        {
            get => _isInitialized;
            private set
            {
                _isInitialized = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or Sets the Parent <see cref = "IConductor" />
        /// </summary>
        public virtual object Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or Sets the Display Name
        /// </summary>
        public virtual string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Indicates whether or not this instance is currently active.
        /// Virtualized in order to help with document oriented view models.
        /// </summary>
        public virtual bool IsActive
        {
            get => _isActive;
            private set
            {
                _isActive = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Raised after activation occurs.
        /// </summary>
        public virtual event EventHandler<ActivationEventArgs> Activated = delegate { };

        /// <summary>
        /// Raised before deactivation.
        /// </summary>
        public virtual event EventHandler<DeactivationEventArgs> AttemptingDeactivation = delegate { };

        /// <summary>
        /// Raised after deactivation.
        /// </summary>
        public virtual event AsyncEventHandler<DeactivationEventArgs> Deactivated = delegate { return Task.FromResult(true); };

        async Task IActivate.ActivateAsync(CancellationToken cancellationToken)
        {
            if (IsActive)
                return;

            var initialized = false;

            if (!IsInitialized)
            {
                await OnInitializeAsync(cancellationToken);
                IsInitialized = initialized = true;
            }

            Log.Info("Activating {0}.", this);
            await OnActivateAsync(cancellationToken);
            IsActive = true;

            Activated?.Invoke(this, new ActivationEventArgs
            {
                WasInitialized = initialized
            });
        }

        async Task IDeactivate.DeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            if (IsActive || IsInitialized && close)
            {
                AttemptingDeactivation?.Invoke(this, new DeactivationEventArgs
                {
                    WasClosed = close
                });

                Log.Info("Deactivating {0}.", this);
                await OnDeactivateAsync(close, cancellationToken);
                IsActive = false;

                await (Deactivated?.InvokeAllAsync(this, new DeactivationEventArgs
                {
                    WasClosed = close
                }) ?? Task.FromResult(true));

                if (close)
                {
                    Views.Clear();
                    Log.Info("Closed {0}.", this);
                }
            }
        }

        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation and holds the value of the close check..</returns>
        public virtual Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
        /// Also provides an opportunity to pass a dialog result to it's corresponding view.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual async Task TryCloseAsync(bool? dialogResult = null)
        {
            if (Parent is IConductor conductor)
            {
                await conductor.CloseItemAsync(this, CancellationToken.None);
            }

            var closeAction = PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult);

            await Execute.OnUIThreadAsync(async () => await closeAction(CancellationToken.None));
        }

        /// <summary>
        /// Called when initializing.
        /// </summary>
        protected virtual Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected virtual Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name = "close">Indicates whether this instance will be closed.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected virtual Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
