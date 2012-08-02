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
        public static FrameworkElement FindName(this IEnumerable<FrameworkElement> elementsToSearch, string name) {
            return elementsToSearch.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
#endif
        /// <summary>
        /// Gets all the <see cref="FrameworkElement"/> instances with names in the scope.
        /// </summary>
        /// <returns>Named <see cref="FrameworkElement"/> instances in the provided scope.</returns>
        /// <remarks>Pass in a <see cref="DependencyObject"/> and receive a list of named <see cref="FrameworkElement"/> instances in the same scope.</remarks>
        public static Func<DependencyObject, IEnumerable<FrameworkElement>> GetNamedElements = elementInScope =>
        {
            var root = elementInScope;
            var previous = elementInScope;
            DependencyObject contentPresenter = null;
            var routeHops = new Dictionary<DependencyObject, DependencyObject>();

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

                if (root is ContentPresenter)
                    contentPresenter = root;
                else if (root is ItemsPresenter && contentPresenter != null)
                {
                    routeHops[root] = contentPresenter;
                    contentPresenter = null;
                }

                previous = root;
                root = VisualTreeHelper.GetParent(previous);
            }

            var descendants = new List<FrameworkElement>();
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var currentElement = current as FrameworkElement;

                if (currentElement != null && !string.IsNullOrEmpty(currentElement.Name))
                    descendants.Add(currentElement);

                if (current is UserControl && current != root)
                    continue;

                if (routeHops.ContainsKey(current))
                {
                    queue.Enqueue(routeHops[current]);
                    continue;
                }

#if NET
                var childCount = (current is UIElement || current is UIElement3D || current is ContainerVisual ? VisualTreeHelper.GetChildrenCount(current) : 0);
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
                    else
                    {
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
                    }
                }
            }

            return descendants;
        };
    }
}