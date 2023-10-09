using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caliburn.Micro;

/// <summary>
/// A base implementation of <see cref = "IViewAware" /> which is capable of caching views by context.
/// </summary>
public class ViewAware : PropertyChangedBase, IViewAware {
    /// <summary>
    /// The default view context.
    /// </summary>
    public static readonly object DefaultContext
        = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewAware"/> class.
    /// </summary>
    public ViewAware()
        => Views = new WeakValueDictionary<object, object>();

    /// <summary>
    /// Raised when a view is attached.
    /// </summary>
    public event EventHandler<ViewAttachedEventArgs> ViewAttached
        = (sender, e) => { };

    /// <summary>
    /// Gets the view chache for this instance.
    /// </summary>
    protected IDictionary<object, object> Views { get; }

    /// <summary>
    /// Gets a view previously attached to this instance.
    /// </summary>
    /// <param name = "context">The context denoting which view to retrieve.</param>
    /// <returns>The view.</returns>
    public virtual object GetView(object context = null) {
        Views.TryGetValue(context ?? DefaultContext, out object view);

        return view;
    }

    void IViewAware.AttachView(object view, object context) {
        Views[context ?? DefaultContext] = view;

        object nonGeneratedView = PlatformProvider.Current.GetFirstNonGeneratedView(view);
        PlatformProvider.Current.ExecuteOnFirstLoad(nonGeneratedView, OnViewLoaded);
        OnViewAttached(nonGeneratedView, context);
        ViewAttached(this, new ViewAttachedEventArgs { View = nonGeneratedView, Context = context });

        if (this is IActivate activatable && !activatable.IsActive) {
            AttachViewReadyOnActivated(activatable, nonGeneratedView);

            return;
        }

        PlatformProvider.Current.ExecuteOnLayoutUpdated(nonGeneratedView, OnViewReady);
    }

    /// <summary>
    /// Called when a view is attached.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="context">The context in which the view appears.</param>
    protected virtual void OnViewAttached(object view, object context) {
    }

    /// <summary>
    /// Called when an attached view's Loaded event fires.
    /// </summary>
    /// <param name="view">The loaded view.</param>
    protected virtual void OnViewLoaded(object view) {
    }

    /// <summary>
    /// Called the first time the page's LayoutUpdated event fires after it is navigated to.
    /// </summary>
    protected virtual void OnViewReady(object view) {
    }

    private static void AttachViewReadyOnActivated(IActivate activatable, object nonGeneratedView) {
        var viewReference = new WeakReference(nonGeneratedView);
        Task OnActivated(object s, ActivationEventArgs e) {
            ((IActivate)s).Activated -= OnActivated;
            object view = viewReference.Target;
            if (view == null) {
                return Task.CompletedTask;
            }

            PlatformProvider.Current.ExecuteOnLayoutUpdated(view, ((ViewAware)s).OnViewReady);

            return Task.CompletedTask;
        }

        activatable.Activated += OnActivated;
    }
}
