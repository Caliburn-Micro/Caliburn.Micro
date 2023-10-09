using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
#endif

namespace Caliburn.Micro {
    /// <summary>
    /// Provides methods for searching a given scope for named elements.
    /// </summary>
    public static class BindingScope {
        private static readonly List<ChildResolver> ChildResolvers = new List<ChildResolver>();
        private static readonly Dictionary<Type, object> NonResolvableChildTypes = new Dictionary<Type, object>();

        static BindingScope() {
            AddChildResolver<ContentControl>(e => new[] { e.Content as DependencyObject });
            AddChildResolver<ItemsControl>(e => e.Items.OfType<DependencyObject>().ToArray());
#if WINDOWS_UWP
            AddChildResolver<SemanticZoom>(e => new[] { e.ZoomedInView as DependencyObject, e.ZoomedOutView as DependencyObject });
            AddChildResolver<ListViewBase>(e => new[] { e.Header as DependencyObject });
            AddChildResolver<ListViewBase>(e => new[] { e.Footer as DependencyObject });
            AddChildResolver<Hub>(ResolveHub);
            AddChildResolver<HubSection>(e => new[] { e.Header as DependencyObject });
            AddChildResolver<CommandBar>(ResolveCommandBar);
            AddChildResolver<Button>(e => ResolveFlyoutBase(e.Flyout));
            AddChildResolver<FrameworkElement>(e => ResolveFlyoutBase(FlyoutBase.GetAttachedFlyout(e)));
            AddChildResolver<SplitView>(e => new[] { e.Pane, e.Content as DependencyObject });
#else
            AddChildResolver<HeaderedContentControl>(e => new[] { e.Header as DependencyObject });
            AddChildResolver<HeaderedItemsControl>(e => new[] { e.Header as DependencyObject });
#endif
        }

        /// <summary>
        /// Gets or sets func to get the parent of the given object in the Visual Tree.
        /// </summary>
        /// <returns>The parent of the given object in the Visual Tree.</returns>
        public static Func<DependencyObject, DependencyObject> GetVisualParent { get; set; }
            = e
                => VisualTreeHelper.GetParent(e);

        /// <summary>
        /// Gets or sets func to get all the <see cref="FrameworkElement"/> instances with names in the scope.
        /// </summary>
        /// <returns>Named <see cref="FrameworkElement"/> instances in the provided scope.</returns>
        /// <remarks>Pass in a <see cref="DependencyObject"/> and receive a list of named <see cref="FrameworkElement"/> instances in the same scope.</remarks>
        public static Func<DependencyObject, IEnumerable<FrameworkElement>> GetNamedElements { get; set; }
            = elementInScope
                => {
                    ScopeNamingRoute routeHops = FindScopeNamingRoute(elementInScope);

                    return FindNamedDescendants(routeHops);
                };

        /// <summary>
        /// Gets or sets func to find a path of dependency objects which traces through visual anscestry until a root which is <see langword="null"/>,
        /// a <see cref="UserControl"/>, a <c>Page</c> with a dependency object <c>Page.ContentProperty</c> value,
        /// a dependency object with <see cref="View.IsScopeRootProperty"/> set to <see langword="true"/>. <see cref="ContentPresenter"/>
        /// and <see cref="ItemsPresenter"/> are included in the resulting <see cref="ScopeNamingRoute"/> in order to track which item
        /// in an items control we are scoped to.
        /// </summary>
        public static Func<DependencyObject, ScopeNamingRoute> FindScopeNamingRoute { get; set; }
            = elementInScope
                => {
                    DependencyObject root = elementInScope;
                    DependencyObject previous = elementInScope;
                    DependencyObject contentPresenter = null;
                    var routeHops = new ScopeNamingRoute();

                    while (true) {
                        if (root == null) {
                            root = previous;
                            break;
                        }

                        if (root is UserControl) {
                            break;
                        }

                        if (root is Page page) {
                            root = page.Content as DependencyObject ?? root;
                            break;
                        }

                        if ((bool)root.GetValue(View.IsScopeRootProperty)) {
                            break;
                        }

#if WINDOWS_UWP
                        if (root is AppBar) {
                            if (Window.Current.Content is Frame frame1 &&
                                frame1.Content is Page page1 &&
                                (page1.TopAppBar == root || page1.BottomAppBar == root)) {
                                root = page1;

                                break;
                            }
                        }
#endif
                        if (root is ContentPresenter) {
                            contentPresenter = root;
                        } else if (root is ItemsPresenter && contentPresenter != null) {
                            routeHops.AddHop(root, contentPresenter);
                            contentPresenter = null;
                        }

                        previous = root;
                        root = GetVisualParent(previous);
                    }

                    routeHops.Root = root;
                    return routeHops;
                };

        /// <summary>
        /// Gets or sets func to find a set of named <see cref="FrameworkElement"/> instances in each hop in a <see cref="ScopeNamingRoute"/>.
        /// </summary>
        /// <remarks>
        /// Searches all the elements in the <see cref="ScopeNamingRoute"/> parameter as well as the visual children of
        /// each of these elements, the <see cref="ContentControl.Content"/>, the <c>HeaderedContentControl.Header</c>,
        /// the <see cref="ItemsControl.Items"/>, or the <c>HeaderedItemsControl.Header</c>, if any are found.
        /// </remarks>
        public static Func<ScopeNamingRoute, IEnumerable<FrameworkElement>> FindNamedDescendants { get; set; }
            = routeHops
                => {
                    if (routeHops == null) {
                        throw new ArgumentNullException(nameof(routeHops));
                    }

                    if (routeHops.Root == null) {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Root is null on the given {0}", typeof(ScopeNamingRoute)));
                    }

                    var descendants = new List<FrameworkElement>();
                    var queue = new Queue<DependencyObject>();
                    queue.Enqueue(routeHops.Root);

                    while (queue.Count > 0) {
                        DependencyObject current = queue.Dequeue();
                        if (current == null) {
                            continue;
                        }

                        if (current is FrameworkElement currentElement && !string.IsNullOrEmpty(currentElement.Name)) {
                            descendants.Add(currentElement);
                        }

                        if (current is UserControl && !ReferenceEquals(current, routeHops.Root)) {
                            continue;
                        }

                        if (routeHops.TryGetHop(current, out DependencyObject hopTarget)) {
                            queue.Enqueue(hopTarget);
                            continue;
                        }

#if NET || CAL_NETCORE
                        int childCount = (current is Visual or Visual3D)
                            ? VisualTreeHelper.GetChildrenCount(current)
                            : 0;
#else
                        int childCount = (current is UIElement)
                            ? VisualTreeHelper.GetChildrenCount(current) : 0;
#endif
                        if (childCount > 0) {
                            for (int i = 0; i < childCount; i++) {
                                DependencyObject childDo = VisualTreeHelper.GetChild(current, i);
                                queue.Enqueue(childDo);
                            }

#if WINDOWS_UWP
                            if (current is Page page) {
                                if (page.BottomAppBar != null) {
                                    queue.Enqueue(page.BottomAppBar);
                                }

                                if (page.TopAppBar != null) {
                                    queue.Enqueue(page.TopAppBar);
                                }
                            }
#endif
                        } else {
                            Type currentType = current.GetType();

                            if (!NonResolvableChildTypes.ContainsKey(currentType)) {
                                ChildResolver[] resolvers = ChildResolvers.Where(r => r.CanResolve(currentType)).ToArray();

                                if (!resolvers.Any()) {
                                    NonResolvableChildTypes[currentType] = null;
                                } else {
                                    resolvers
                                        .SelectMany(r => r.Resolve(current) ?? Enumerable.Empty<DependencyObject>())
                                        .Where(c => c != null)
                                        .Apply(queue.Enqueue);
                                }
                            }
                        }
                    }

                    return descendants;
                };

        /// <summary>
        /// Searches through the list of named elements looking for a case-insensitive match.
        /// </summary>
        /// <param name="elementsToSearch">The named elements to search through.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>The named element or null if not found.</returns>
        public static FrameworkElement FindName(this IEnumerable<FrameworkElement> elementsToSearch, string name)
            =>
#if WINDOWS_UWP
                elementsToSearch.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
#else
                elementsToSearch.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
#endif

        /// <summary>
        /// Adds a child resolver.
        /// </summary>
        /// <param name="filter">The type filter.</param>
        /// <param name="resolver">The resolver.</param>
        public static ChildResolver AddChildResolver(Func<Type, bool> filter, Func<DependencyObject, IEnumerable<DependencyObject>> resolver) {
            if (filter == null) {
                throw new ArgumentNullException(nameof(filter));
            }

            if (resolver == null) {
                throw new ArgumentNullException(nameof(resolver));
            }

            NonResolvableChildTypes.Clear();

            var childResolver = new ChildResolver(filter, resolver);

            ChildResolvers.Add(childResolver);

            return childResolver;
        }

        /// <summary>
        /// Adds a child resolver.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public static ChildResolver AddChildResolver<T>(Func<T, IEnumerable<DependencyObject>> resolver)
            where T : DependencyObject {
            if (resolver == null) {
                throw new ArgumentNullException(nameof(resolver));
            }

            NonResolvableChildTypes.Clear();

            var childResolver = new ChildResolver<T>(resolver);

            ChildResolvers.Add(childResolver);

            return childResolver;
        }

        /// <summary>
        /// Removes a child resolver.
        /// </summary>
        /// <param name="resolver">The resolver to remove.</param>
        /// <returns>true, when the resolver was (found and) removed.</returns>
        public static bool RemoveChildResolver(ChildResolver resolver)
            => resolver == null ? throw new ArgumentNullException(nameof(resolver)) : ChildResolvers.Remove(resolver);

#if WINDOWS_UWP
        private static IEnumerable<DependencyObject> ResolveFlyoutBase(FlyoutBase flyoutBase) {
            if (flyoutBase == null) {
                yield break;
            }

            if (flyoutBase is Flyout flyout &&
                flyout.Content != null) {
                yield return flyout.Content;
            }

            if (flyoutBase is not MenuFlyout menuFlyout ||
                menuFlyout.Items == null) {
                yield break;
            }

            foreach (MenuFlyoutItemBase item in menuFlyout.Items) {
                foreach (DependencyObject subItem in ResolveMenuFlyoutItems(item)) {
                    yield return subItem;
                }
            }
        }

        private static IEnumerable<DependencyObject> ResolveMenuFlyoutItems(MenuFlyoutItemBase item) {
            yield return item;

            if (item is not MenuFlyoutSubItem subItem || subItem.Items == null) {
                yield break;
            }

            foreach (MenuFlyoutItemBase subSubItem in subItem.Items) {
                yield return subSubItem;
            }
        }

        private static IEnumerable<DependencyObject> ResolveCommandBar(CommandBar commandBar) {
            foreach (DependencyObject child in commandBar.PrimaryCommands.OfType<DependencyObject>()) {
                yield return child;
            }

            foreach (DependencyObject child in commandBar.SecondaryCommands.OfType<DependencyObject>()) {
                yield return child;
            }
        }

        private static IEnumerable<DependencyObject> ResolveHub(Hub hub) {
            yield return hub.Header as DependencyObject;

            foreach (HubSection section in hub.Sections) {
                yield return section;
            }
        }
#endif

        /// <summary>
        /// Maintains a connection in the visual tree of dependency objects in order to record a route through it.
        /// </summary>
        public class ScopeNamingRoute {
            private readonly Dictionary<DependencyObject, DependencyObject> path
                = new Dictionary<DependencyObject, DependencyObject>();

            private DependencyObject root;

            /// <summary>
            /// Gets or sets the starting point of the route.
            /// </summary>
            public DependencyObject Root {
                get => root;

                set {
                    if (path.ContainsValue(value)) {
                        throw new ArgumentException("Value is a target of some route hop; cannot be a root.");
                    }

                    root = value;
                }
            }

            /// <summary>
            /// Tries to get a target dependency object given a source.
            /// </summary>
            /// <param name="hopSource">The possible beginning of a route segment (hop).</param>
            /// <param name="hopTarget">The target of a route segment (hop).</param>
            /// <returns><see langword="true"/> if <paramref name="hopSource"/> had a target recorded; <see langword="false"/> otherwise.</returns>
            public bool TryGetHop(DependencyObject hopSource, out DependencyObject hopTarget)
                => path.TryGetValue(hopSource, out hopTarget);

            /// <summary>
            /// Adds a segment to the route.
            /// </summary>
            /// <param name="from">The source dependency object.</param>
            /// <param name="to">The target dependency object.</param>
            public void AddHop(DependencyObject from, DependencyObject to) {
                if (@from == null) {
                    throw new ArgumentNullException(nameof(from));
                }

                if (to == null) {
                    throw new ArgumentNullException(nameof(to));
                }

                if (path.Count > 0 &&
                    !path.ContainsKey(from) &&
                    !path.ContainsKey(to) &&
                    !path.ContainsValue(from) &&
                    !path.ContainsValue(from)) {
                    throw new ArgumentException("Hop pair not part of existing route.");
                }

                if (path.ContainsKey(to)) {
                    throw new ArgumentException("Cycle detected when adding hop.");
                }

                path[from] = to;
            }
        }
    }
}
