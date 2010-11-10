namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// A base implementation of <see cref="IScreen"/>.
    /// </summary>
    public class Screen : PropertyChangedBase, IScreen, IChild<IConductor>, IViewAware
    {
        protected static readonly ILog Log = LogManager.GetLog(typeof(Screen));

        bool isActive;
        bool isInitialized;
        IConductor parent;
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
        public IConductor Parent
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
        public string DisplayName
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

            if(close)
                Log.Info("Closed {0}.", this);

            Deactivated(this, new DeactivationEventArgs {
                WasClosed = close
            });
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
            views[context ?? ViewLocator.DefaultContext] = view;

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
            views.TryGetValue(context ?? ViewLocator.DefaultContext, out view);
            return view;
        }

        /// <summary>
        /// Raised when a view is attached.
        /// </summary>
        public event EventHandler<ViewAttachedEventArgs> ViewAttached = delegate { };

        /// <summary>
        /// Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding default view to close.
        /// </summary>
        public void TryClose() {
            Execute.OnUIThread(() => {
                if(Parent != null)
                    Parent.CloseItem(this);
                else {
                    var view = GetView(null);

                    if(view == null) {
                        var ex = new NotSupportedException("A Parent or default view is required.");
                        Log.Error(ex);
                        throw ex;
                    }

                    var method = view.GetType().GetMethod("Close");
                    if(method != null) {
                        method.Invoke(view, null);
                        return;
                    }

                    var property = view.GetType().GetProperty("IsOpen");
                    if(property != null) {
                        property.SetValue(view, false, new object[] {});
                        return;
                    }

                    var ex2 = new NotSupportedException("The default view does not support Close/IsOpen.");
                    Log.Error(ex2);
                    throw ex2;
                }
            });
        }

#if !SILVERLIGHT

        /// <summary>
        /// Closes this instance by asking its Parent to initiate shutdown or by asking it's corresponding default view to close.
        /// This overload also provides an opportunity to pass a dialog result to it's corresponding default view.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual void TryClose(bool? dialogResult)
        {
            Execute.OnUIThread(() => {
                var view = GetView(null);

                if(view != null)
                {
                    var property = view.GetType().GetProperty("DialogResult");
                    if(property != null)
                        property.SetValue(view, dialogResult, null);
                }

                TryClose();
            });
        }

#endif
    }
}