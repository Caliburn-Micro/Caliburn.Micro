namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Expression = System.Linq.Expressions.Expression;

    /// <summary>
    /// Generic extension methods used by the framework.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets all the attributes of a particular type.
        /// </summary>
        /// <typeparam name="T">The type of attributes to get.</typeparam>
        /// <param name="member">The member to inspect for attributes.</param>
        /// <param name="inherit">Whether or not to search for inherited attributes.</param>
        /// <returns>The list of attributes found.</returns>
        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit)
        {
            return Attribute.GetCustomAttributes(member, inherit).OfType<T>();
        }

        /// <summary>
        /// Gets a property by name, ignoring case and searching all interfaces.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="propertyName">The property to search for.</param>
        /// <returns>The property or null if not found.</returns>
        public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName) {
            var typeList = new List<Type> { type };

            if (type.IsInterface)
                typeList.AddRange(type.GetInterfaces());

            return typeList
                .Select(interfaceType => interfaceType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance))
                .FirstOrDefault(property => property != null);
        }

        /// <summary>
        /// Applies the action to each element in the list.
        /// </summary>
        /// <typeparam name="T">The enumerable item's type.</typeparam>
        /// <param name="enumerable">The elements to enumerate.</param>
        /// <param name="action">The action to apply to each item in the list.</param>
        public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);
        }

        /// <summary>
        /// Converts an expression into a <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>The member info.</returns>
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member;
        }

        /// <summary>
        /// Searches through the list of named elements looking for a case-insensitive match.
        /// </summary>
        /// <param name="elementsToSearch">The named elements to search through.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>The named element or null if not found.</returns>
        public static FrameworkElement FindName(this IEnumerable<FrameworkElement> elementsToSearch, string name)
        {
            return elementsToSearch.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets all the <see cref="FrameworkElement"/> instances with names in the scope.
        /// </summary>
        /// <returns>Named <see cref="FrameworkElement"/> instances in the provided scope.</returns>
        /// <remarks>Pass in a <see cref="DependencyObject"/> and receive a list of named <see cref="FrameworkElement"/> instances in the same scope.</remarks>
        public static Func<DependencyObject, IEnumerable<FrameworkElement>> GetNamedElementsInScope = elementInScope =>{
            var root = elementInScope;
            var previous = elementInScope;

            while(true)
            {
                if(root == null)
                {
                    root = previous;
                    break;
                }
                if(root is UserControl)
                    break;

                previous = root;
                root = VisualTreeHelper.GetParent(previous);
            }

            var descendants = new List<FrameworkElement>();
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                var currentElement = current as FrameworkElement;

                if(currentElement != null && !string.IsNullOrEmpty(currentElement.Name))
                    descendants.Add(currentElement);

                var childCount = VisualTreeHelper.GetChildrenCount(current);
                if(childCount > 0)
                {
                    for(var i = 0; i < childCount; i++)
                    {
                        var childDo = VisualTreeHelper.GetChild(current, i);

                        if(childDo is UserControl)
                            continue;

                        queue.Enqueue(childDo);
                    }
                }
                else
                {
                    var contentControl = current as ContentControl;
                    if(contentControl != null)
                    {
                        if(contentControl.Content != null
                            && contentControl.Content is DependencyObject
                                && !(contentControl.Content is UserControl))
                            queue.Enqueue(contentControl.Content as DependencyObject);
                    }
                    else
                    {
                        var itemsControl = current as ItemsControl;
                        if(itemsControl != null)
                        {
                            itemsControl.Items.OfType<DependencyObject>()
                                .Where(item => !(item is UserControl))
                                .Apply(queue.Enqueue);
                        }
                    }
                }
            }

            return descendants;
        };
    }
}