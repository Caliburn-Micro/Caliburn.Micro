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
        public virtual event EventHandler<DeactivationEventArgs> Deactivated = delegate { };

        async Task IActivate.ActivateAsync(CancellationToken cancellationToken)
        {
            if (IsActive)
                return;

            var initialized = false;

            if (!IsInitialized)
            {
                IsInitialized = initialized = true;
                await OnInitializeAsync(cancellationToken);
            }

            IsActive = true;
            Log.Info("Activating {0}.", this);
            await OnActivateAsync(cancellationToken);

            Activated?.Invoke(this, new ActivationEventArgs
            {
                WasInitialized = initialized
            });
        }

        void IDeactivate.Deactivate(bool close)
        {
            if (IsActive || IsInitialized && close)
            {
                AttemptingDeactivation?.Invoke(this, new DeactivationEventArgs
                {
                    WasClosed = close
                });

                IsActive = false;
                Log.Info("Deactivating {0}.", this);
                OnDeactivate(close);

                Deactivated?.Invoke(this, new DeactivationEventArgs
                {
                    WasClosed = close
                });

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
        /// <param name = "callback">The implementor calls this action with the result of the close check.</param>
        public virtual Task<bool> CanCloseAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
        /// Also provides an opportunity to pass a dialog result to it's corresponding view.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual Task TryCloseAsync(bool? dialogResult = null)
        {
            PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();

            return Task.FromResult(true);
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
        protected virtual void OnDeactivate(bool close)
        {
        }
    }
}
