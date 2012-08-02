namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
#if WinRT
    using Windows.UI.Xaml;
#else
    using System.Windows;
#endif

    ///<summary>
    ///  A base implementation of <see cref = "IViewAware" /> which is capable of caching views by context.
    ///</summary>
    public class ViewAware : PropertyChangedBase, IViewAware
    {
        bool cacheViews;

        static readonly DependencyProperty PreviouslyAttachedProperty = DependencyProperty.RegisterAttached(
            "PreviouslyAttached",
            typeof(bool),
            typeof(ViewAware),
            null
            );

        /// <summary>
        /// Indicates whether or not implementors of <see cref="IViewAware"/> should cache their views by default.
        /// </summary>
        public static bool CacheViewsByDefault = true;

        /// <summary>
        ///   The view chache for this instance.
        /// </summary>
        protected readonly Dictionary<object, object> Views = new Dictionary<object, object>();

        ///<summary>
        /// Creates an instance of <see cref="ViewAware"/>.
        ///</summary>
        public ViewAware()
            : this(CacheViewsByDefault) { }

        ///<summary>
        /// Creates an instance of <see cref="ViewAware"/>.
        ///</summary>
        ///<param name="cacheViews">Indicates whether or not this instance maintains a view cache.</param>
        public ViewAware(bool cacheViews)
        {
            CacheViews = cacheViews;
        }

        /// <summary>
        ///   Raised when a view is attached.
        /// </summary>
        public event EventHandler<ViewAttachedEventArgs> ViewAttached = delegate { };

        ///<summary>
        ///  Indicates whether or not this instance maintains a view cache.
        ///</summary>
        protected bool CacheViews
        {
            get { return cacheViews; }
            set
            {
                cacheViews = value;
                if (!cacheViews)
                    Views.Clear();
            }
        }

        void IViewAware.AttachView(object view, object context)
        {
            if (CacheViews)
            {
                Views[context ?? View.DefaultContext] = view;
            }

            var nonGeneratedView = View.GetFirstNonGeneratedView(view);

            var element = nonGeneratedView as FrameworkElement;
            if (element != null && !(bool)element.GetValue(PreviouslyAttachedProperty))
            {
                element.SetValue(PreviouslyAttachedProperty, true);
                View.ExecuteOnLoad(element, (s, e) => OnViewLoaded(s));
            }

            OnViewAttached(nonGeneratedView, context);
            ViewAttached(this, new ViewAttachedEventArgs { View = nonGeneratedView, Context = context });
        }

        /// <summary>
        /// Called when a view is attached.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="context">The context in which the view appears.</param>
        protected internal virtual void OnViewAttached(object view, object context) { }

        /// <summary>
        ///   Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name = "view"></param>
        protected internal virtual void OnViewLoaded(object view) { }

#if WP71 || WinRT
        /// <summary>
        ///   Called the first time the page's LayoutUpdated event fires after it is navigated to.
        /// </summary>
        /// <param name = "view"></param>
        protected internal virtual void OnViewReady(object view) { }
#endif

        /// <summary>
        ///   Gets a view previously attached to this instance.
        /// </summary>
        /// <param name = "context">The context denoting which view to retrieve.</param>
        /// <returns>The view.</returns>
        public virtual object GetView(object context = null)
        {
            object view;
            Views.TryGetValue(context ?? View.DefaultContext, out view);
            return view;
        }
    }
}