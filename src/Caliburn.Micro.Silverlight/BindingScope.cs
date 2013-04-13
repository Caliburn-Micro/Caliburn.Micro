namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
#if WinRT
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
#else
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
#endif

    /// <summary>
    /// Provides methods for searching a given scope for named elements.
    /// </summary>
    public static class BindingScope
    {
        private static readonly List<Func<Type, Boolean>> _childResolverFilters
            = new List<Func<Type, Boolean>>();
        private static readonly List<Func<DependencyObject, IEnumerable<DependencyObject>>> _childResolvers
            = new List<Func<DependencyObject, IEnumerable<DependencyObject>>>();
        private static readonly Dictionary<Type, Object> _nonResolvableChildTypes = new Dictionary<Type, Object>();

        /// <summary>
        /// Searches through the list of named elements looking for a case-insensitive match.
        /// </summary>
        /// <param name="elementsToSearch">The named elements to search through.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>The named element or null if not found.</returns>
#if WinRT
        public static FrameworkElement FindName(this IEnumerable<FrameworkElement> elementsToSearch, string name)
        {
            return elementsToSearch.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
#else
        public static FrameworkElement FindName(this IEnumerable<FrameworkElement> elementsToSearch, string name)
        {
            return elementsToSearch.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
#endif

        public static void AddChildResolver(Func<Type, Boolean> filter, Func<DependencyObject, IEnumerable<DependencyObject>> resolver)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            _nonResolvableChildTypes.Clear();
            _childResolverFilters.Add(filter);
            _childResolvers.Add(resolver);
        }

        public static Boolean RemoveChildResolver(Func<DependencyObject, IEnumerable<DependencyObject>> resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            var index = _childResolvers.IndexOf(resolver);

            if (index >= 0)
            {
                _childResolverFilters.RemoveAt(index);
                _childResolvers.RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets all the <see cref="FrameworkElement"/> instances with names in the scope.
        /// </summary>
        /// <returns>Named <see cref="FrameworkElement"/> instances in the provided scope.</returns>
        /// <remarks>Pass in a <see cref="DependencyObject"/> and receive a list of named <see cref="FrameworkElement"/> instances in the same scope.</remarks>
        public static Func<DependencyObject, IEnumerable<FrameworkElement>> GetNamedElements = elementInScope =>
            {
                var routeHops = FindScopeNamingRoute(elementInScope);

                return FindNamedDescendants(routeHops);
            };

        /// <summary>
        /// Finds a set of named <see cref="FrameworkElement"/> instances in each hop in a <see cref="ScopeNamingRoute"/>.
        /// </summary>
        /// <remarks>
        /// Searches all the elements in the <see cref="ScopeNamingRoute"/> parameter as well as the visual children of 
        /// each of these elements, the <see cref="ContentControl.Content"/>, the <see cref="HeaderedContentControl.Header"/>,
        /// the <see cref="ItemsControl.Items"/>, or the <see cref="HeaderedItemsControl.Header"/>, if any are found.
        /// </remarks>
        public static Func<ScopeNamingRoute, IEnumerable<FrameworkElement>> FindNamedDescendants = routeHops =>
            {
                if (routeHops == null)
                {
                    throw new ArgumentNullException("routeHops");
                }

                if (routeHops.Root == null)
                {
                    throw new ArgumentException(String.Format("Root is null on the given {0}", typeof(ScopeNamingRoute)));
                }

                var descendants = new List<FrameworkElement>();
                var queue = new Queue<DependencyObject>();
                queue.Enqueue(routeHops.Root);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    var currentElement = current as FrameworkElement;

                    if (currentElement != null && !string.IsNullOrEmpty(currentElement.Name))
                        descendants.Add(currentElement);

                    if (current is UserControl && !ReferenceEquals(current, routeHops.Root))
                        continue;

                    DependencyObject hopTarget;
                    if (routeHops.TryGetHop(current, out hopTarget))
                    {
                        queue.Enqueue(hopTarget);
                        continue;
                    }

#if NET
                    var childCount = (current is UIElement || current is UIElement3D || current is ContainerVisual
                                          ? VisualTreeHelper.GetChildrenCount(current)
                                          : 0);
#else
                    var childCount = VisualTreeHelper.GetChildrenCount(current);
#endif
                    if (childCount > 0)
                    {
                        for (var i = 0; i < childCount; i++)
                        {
                            var childDo = VisualTreeHelper.GetChild(current, i);
                            queue.Enqueue(childDo);
                        }

#if WinRT
                        var page = current as Page;

                        if (page != null)
                        {
                            if (page.BottomAppBar != null)
                                queue.Enqueue(page.BottomAppBar);

                            if (page.TopAppBar != null)
                                queue.Enqueue(page.TopAppBar);
                        }
#endif
                    }
                    else
                    {
                        var contentControl = current as ContentControl;
                        if (contentControl != null)
                        {
                            if (contentControl.Content is DependencyObject)
                                queue.Enqueue(contentControl.Content as DependencyObject);
#if !SILVERLIGHT && !WinRT
                            var headeredControl = contentControl as HeaderedContentControl;
                            if (headeredControl != null && headeredControl.Header is DependencyObject)
                                queue.Enqueue(headeredControl.Header as DependencyObject);
#endif
                        }

                        var itemsControl = current as ItemsControl;
                        if (itemsControl != null)
                        {
                            itemsControl.Items.OfType<DependencyObject>()
                                .Apply(queue.Enqueue);
#if !SILVERLIGHT && !WinRT
                            var headeredControl = itemsControl as HeaderedItemsControl;
                            if (headeredControl != null && headeredControl.Header is DependencyObject)
                                queue.Enqueue(headeredControl.Header as DependencyObject);
#endif
                        }
#if WinRT
                        var semanticZoom = current as SemanticZoom;
                        if (semanticZoom != null) 
                        {
                            if (semanticZoom.ZoomedInView is DependencyObject)
                                queue.Enqueue(semanticZoom.ZoomedInView as DependencyObject);

                            if (semanticZoom.ZoomedOutView is DependencyObject)
                                queue.Enqueue(semanticZoom.ZoomedOutView as DependencyObject);
                        }
#endif
                        else
                        {
                            var currentType = current.GetType();

                            if (!_nonResolvableChildTypes.ContainsKey(currentType))
                            {
                                var canResolve = _childResolverFilters.Any(f => f(currentType));

                                if (!canResolve)
                                {
                                    _nonResolvableChildTypes[currentType] = null;
                                }
                                else
                                {
                                    _childResolvers.SelectMany(r => r(current) ?? Enumerable.Empty<DependencyObject>())
                                                   .Where(c => c != null)
                                                   .Apply(queue.Enqueue);
                                }
                            }
                        }
                    }
                }
                
                return descendants;
            };

        /// <summary>
        /// Finds a path of dependency objects which traces through visual anscestry until a root which is <see langword="null"/>,
        /// a <see cref="UserControl"/>, a <see cref="Page"/> with a dependency object <see cref="Page.ContentProperty"/> value, 
        /// a dependency object with <see cref="View.IsScopeRootProperty"/> set to <see langword="true"/>. <see cref="ContentPresenter"/>
        /// and <see cref="ItemsPresenter"/> are included in the resulting <see cref="ScopeNamingRoute"/> in order to track which item
        /// in an items control we are scoped to.
        /// </summary>
        public static Func<DependencyObject, ScopeNamingRoute> FindScopeNamingRoute = elementInScope =>
            {
                var root = elementInScope;
                var previous = elementInScope;
                DependencyObject contentPresenter = null;
                var routeHops = new ScopeNamingRoute();

                while (true)
                {
                    if (root == null)
                    {
                        root = previous;
                        break;
                    }

                    if (root is UserControl)
                        break;
#if !SILVERLIGHT
                    if (root is Page)
                    {
                        root = ((Page)root).Content as DependencyObject ?? root;
                        break;
                    }
#endif
                    if ((bool)root.GetValue(View.IsScopeRootProperty))
                        break;

#if WinRT
                    if (root is AppBar) 
                    {
                        var frame = Window.Current.Content as Frame;
                        var page = (frame != null) ? frame.Content as Page : null;
                        if (page != null && (root == page.TopAppBar || root == page.BottomAppBar)) 
                        {
                            root = page;
                            break;
                        }
                    }
#endif

                    if (root is ContentPresenter)
                        contentPresenter = root;
                    else if (root is ItemsPresenter && contentPresenter != null)
                    {
                        routeHops.AddHop(root, contentPresenter);
                        contentPresenter = null;
                    }

                    previous = root;
                    root = VisualTreeHelper.GetParent(previous);
                }

                routeHops.Root = root;
                return routeHops;
            };

        /// <summary>
        /// Maintains a connection in the visual tree of dependency objects in order to record a route through it.
        /// </summary>
        public class ScopeNamingRoute
        {
            private readonly Dictionary<DependencyObject, DependencyObject> _path
                = new Dictionary<DependencyObject, DependencyObject>();
            private DependencyObject _root;

            /// <summary>
            /// Gets or sets the starting point of the route.
            /// </summary>
            public DependencyObject Root
            {
                get
                {
                    return _root;
                }

                set
                {
                    if (_path.Count > 0 && !_path.ContainsKey(value))
                    {
                        throw new ArgumentException("Value is not a hop source in the route.");
                    }

                    if (_path.ContainsValue(value))
                    {
                        throw new ArgumentException("Value is a target of some route hop; cannot be a root.");
                    }

                    _root = value;
                }
            }

            /// <summary>
            /// Adds a segment to the route.
            /// </summary>
            /// <param name="from">The source dependency object.</param>
            /// <param name="to">The target dependency object.</param>
            public void AddHop(DependencyObject from, DependencyObject to)
            {
                if (@from == null)
                {
                    throw new ArgumentNullException("from");
                }

                if (to == null)
                {
                    throw new ArgumentNullException("to");
                }

                if (_path.Count > 0 &&
                    !_path.ContainsKey(from) &&
                    !_path.ContainsKey(to) &&
                    !_path.ContainsValue(from) &&
                    !_path.ContainsValue(from))
                {
                    throw new ArgumentException("Hop pair not part of existing route.");
                }

                if (_path.ContainsKey(to))
                {
                    throw new ArgumentException("Cycle detected when adding hop.");
                }

                _path[from] = to;
            }

            /// <summary>
            /// Tries to get a target dependency object given a source.
            /// </summary>
            /// <param name="hopSource">The possible beginning of a route segment (hop).</param>
            /// <param name="hopTarget">The target of a route segment (hop).</param>
            /// <returns><see langword="true"/> if <paramref name="hopSource"/> had a target recorded; <see langword="false"/> otherwise.</returns>
            public Boolean TryGetHop(DependencyObject hopSource, out DependencyObject hopTarget)
            {
                return _path.TryGetValue(hopSource, out hopTarget);
            }
        }
    }
}