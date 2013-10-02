namespace Caliburn.Micro {
    using System;
    using System.Reflection;

    /// <summary>
    ///   A base implementation of <see cref = "IScreen" />.
    /// </summary>
    public class Screen : ViewAware, IScreen, IChild {
        static readonly ILog Log = LogManager.GetLog(typeof(Screen));

        bool isActive;
        bool isInitialized;
        object parent;
        string displayName;

        /// <summary>
        ///   Creates an instance of the screen.
        /// </summary>
        public Screen() {
            displayName = GetType().FullName;
        }

        /// <summary>
        ///   Gets or Sets the Parent <see cref = "IConductor" />
        /// </summary>
        public object Parent {
            get { return parent; }
            set {
                parent = value;
                NotifyOfPropertyChange("Parent");
            }
        }

        /// <summary>
        ///   Gets or Sets the Display Name
        /// </summary>
        public string DisplayName {
            get { return displayName; }
            set {
                displayName = value;
                NotifyOfPropertyChange("DisplayName");
            }
        }

        /// <summary>
        ///   Indicates whether or not this instance is currently active.
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            private set {
                isActive = value;
                NotifyOfPropertyChange("IsActive");
            }
        }

        /// <summary>
        ///   Indicates whether or not this instance is currently initialized.
        /// </summary>
        public bool IsInitialized {
            get { return isInitialized; }
            private set {
                isInitialized = value;
                NotifyOfPropertyChange("IsInitialized");
            }
        }

        /// <summary>
        ///   Raised after activation occurs.
        /// </summary>
        public event EventHandler<ActivationEventArgs> Activated = delegate { };

        /// <summary>
        ///   Raised before deactivation.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation = delegate { };

        /// <summary>
        ///   Raised after deactivation.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> Deactivated = delegate { };

        void IActivate.Activate() {
            if (IsActive) {
                return;
            }

            var initialized = false;

            if (!IsInitialized) {
                IsInitialized = initialized = true;
                OnInitialize();
            }

            IsActive = true;
            Log.Info("Activating {0}.", this);
            OnActivate();

            Activated(this, new ActivationEventArgs {
                WasInitialized = initialized
            });
        }

        /// <summary>
        ///   Called when initializing.
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        ///   Called when activating.
        /// </summary>
        protected virtual void OnActivate() { }

        void IDeactivate.Deactivate(bool close) {
            if(IsActive || (IsInitialized && close)) {
                AttemptingDeactivation(this, new DeactivationEventArgs {
                    WasClosed = close
                });

                IsActive = false;
                Log.Info("Deactivating {0}.", this);
                OnDeactivate(close);

                Deactivated(this, new DeactivationEventArgs {
                    WasClosed = close
                });

                if(close) {
                    Views.Clear();
                    Log.Info("Closed {0}.", this);
                }
            }
        }

        /// <summary>
        ///   Called when deactivating.
        /// </summary>
        /// <param name = "close">Inidicates whether this instance will be closed.</param>
        protected virtual void OnDeactivate(bool close) { }

        /// <summary>
        ///   Called to check whether or not this instance can close.
        /// </summary>
        /// <param name = "callback">The implementor calls this action with the result of the close check.</param>
        public virtual void CanClose(Action<bool> callback) {
            callback(true);
        }

        System.Action GetViewCloseAction(bool? dialogResult) {
            var conductor = Parent as IConductor;
            if (conductor != null) {
                return () => conductor.CloseItem(this);
            }

            foreach (var contextualView in Views.Values) {
                var viewType = contextualView.GetType();
#if WinRT
                var closeMethod = viewType.GetRuntimeMethod("Close", new Type[0]);
#else
                var closeMethod = viewType.GetMethod("Close");
#endif
                if(closeMethod != null)
                    return () => {
#if !SILVERLIGHT && !WinRT
                        var isClosed = false;
                        if(dialogResult != null) {
                            var resultProperty = contextualView.GetType().GetProperty("DialogResult");
                            if (resultProperty != null) {
                                resultProperty.SetValue(contextualView, dialogResult, null);
                                isClosed = true;
                            }
                        }

                        if (!isClosed) {
                            closeMethod.Invoke(contextualView, null);
                        }
#else
                        closeMethod.Invoke(contextualView, null);
#endif
                    };

#if WinRT
                var isOpenProperty = viewType.GetRuntimeProperty("IsOpen");
#else
                var isOpenProperty = viewType.GetProperty("IsOpen");
#endif
                if (isOpenProperty != null) {
                    return () => isOpenProperty.SetValue(contextualView, false, null);
                }
            }

            return () => Log.Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
        }

        /// <summary>
        ///   Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
        /// </summary>
        public virtual void TryClose() {
            GetViewCloseAction(null).OnUIThread();
        }

#if !SILVERLIGHT
        /// <summary>
        /// Closes this instance by asking its Parent to initiate shutdown or by asking it's corresponding view to close.
        /// This overload also provides an opportunity to pass a dialog result to it's corresponding view.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual void TryClose(bool? dialogResult) {
            GetViewCloseAction(dialogResult).OnUIThread();
        }
#endif
    }
}
