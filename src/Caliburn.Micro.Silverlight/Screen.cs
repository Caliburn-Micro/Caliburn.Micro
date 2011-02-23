namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// A base implementation of <see cref="IScreen"/>.
    /// </summary>
    public class Screen : PropertyChangedBase, IScreen, IChild, IViewAware
    {
        protected static readonly ILog Log = LogManager.GetLog(typeof(Screen));

        bool isActive;
        bool isInitialized;
        object parent;
        string displayName;
        readonly Dictionary<object, object> views = new Dictionary<object, object>();

        /// <summary>
        /// Creates an instance of the screen.
        /// </summary>
        public Screen()
        {
            DisplayName = GetType().FullName;
        }

        /// <summary>
        /// Gets or Sets the Parent <see cref="IConductor"/>
        /// </summary>
        public virtual object Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                NotifyOfPropertyChange("Parent");
            }
        }

        /// <summary>
        /// Gets or Sets the Display Name
        /// </summary>
        public virtual string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                NotifyOfPropertyChange("DisplayName");
            }
        }

        /// <summary>
        /// Indicates whether or not this instance is currently active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            private set
            {
                isActive = value;
                NotifyOfPropertyChange("IsActive");
            }
        }

        /// <summary>
        /// Indicates whether or not this instance is currently initialized.
        /// </summary>
        public bool IsInitialized
        {
            get { return isInitialized; }
            private set
            {
                isInitialized = value;
                NotifyOfPropertyChange("IsInitialized");
            }
        }

        /// <summary>
        /// Raised after activation occurs.
        /// </summary>
        public event EventHandler<ActivationEventArgs> Activated = delegate { };

        /// <summary>
        /// Raised before deactivation.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation = delegate { };

        /// <summary>
        /// Raised after deactivation.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> Deactivated = delegate { };

        void IActivate.Activate()
        {
            if(IsActive)
                return;

            var initialized = false;

            if(!IsInitialized)
            {
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
        /// Called when initializing.
        /// </summary>
        protected virtual void OnInitialize() {}

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected virtual void OnActivate() {}

        void IDeactivate.Deactivate(bool close)
        {
            if(!IsActive && !IsInitialized)
                return;

            AttemptingDeactivation(this, new DeactivationEventArgs {
                WasClosed = close
            });

            IsActive = false;
            Log.Info("Deactivating {0}.", this);
            OnDeactivate(close);

            Deactivated(this, new DeactivationEventArgs {
                WasClosed = close
            });

            if (close) {
                views.Clear();
                Log.Info("Closed {0}.", this);
            }
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected virtual void OnDeactivate(bool close) {}

        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="callback">The implementor calls this action with the result of the close check.</param>
        public virtual void CanClose(Action<bool> callback)
        {
            callback(true);
        }

        /// <summary>
        /// Attaches a view to this instance.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="context">The context in which the view appears.</param>
        public virtual void AttachView(object view, object context)
        {
            var loadWired = views.Values.Contains(view);
            views[context ?? View.DefaultContext] = view;

            var element = view as FrameworkElement;
            if(!loadWired && element != null)
                element.Loaded += delegate { OnViewLoaded(view); };

            if(!loadWired)
                ViewAttached(this, new ViewAttachedEventArgs{ View = view, Context = context });
        }

        /// <summary>
        /// Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name="view"></param>
        protected virtual void OnViewLoaded(object view) {}

        /// <summary>
        /// Gets a view previously attached to this instance.
        /// </summary>
        /// <param name="context">The context denoting which view to retrieve.</param>
        /// <returns>The view.</returns>
        public virtual object GetView(object context)
        {
            object view;
            views.TryGetValue(context ?? View.DefaultContext, out view);
            return view;
        }

        /// <summary>
        /// Raised when a view is attached.
        /// </summary>
        public event EventHandler<ViewAttachedEventArgs> ViewAttached = delegate { };

        System.Action GetViewCloseAction(bool? dialogResult) {
            var conductor = Parent as IConductor;
            if (conductor != null)
                return () => conductor.CloseItem(this);

            foreach (var contextualView in views.Values) {
                var viewType = contextualView.GetType();

                var closeMethod = viewType.GetMethod("Close");
                if (closeMethod != null)
                    return () => {
#if !SILVERLIGHT
                        if(dialogResult != null) {
                            var resultProperty = contextualView.GetType().GetProperty("DialogResult");
                            if (resultProperty != null)
                                resultProperty.SetValue(contextualView, dialogResult, null);
                        }
#endif

                        closeMethod.Invoke(contextualView, null);
                    };

                var isOpenProperty = viewType.GetProperty("IsOpen");
                if (isOpenProperty != null)
                    return () => isOpenProperty.SetValue(contextualView, false, null);
            }

            return () => {
                var ex = new NotSupportedException("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
                Log.Error(ex);
                throw ex;
            };
        }

        /// <summary>
        /// Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
        /// </summary>
        public void TryClose() {
            Execute.OnUIThread(() => {
                var closeAction = GetViewCloseAction(null);
                closeAction();
            });
        }

#if !SILVERLIGHT

        /// <summary>
        /// Closes this instance by asking its Parent to initiate shutdown or by asking it's corresponding view to close.
        /// This overload also provides an opportunity to pass a dialog result to it's corresponding view.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual void TryClose(bool? dialogResult) {
            Execute.OnUIThread(() => {
                var closeAction = GetViewCloseAction(dialogResult);
                closeAction();
            });
        }

#endif
    }
}