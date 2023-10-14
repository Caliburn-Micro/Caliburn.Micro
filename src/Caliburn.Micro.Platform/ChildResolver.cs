using System;
using System.Collections.Generic;

#if WINDOWS_UWP
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace Caliburn.Micro {
    /// <summary>
    /// Represents a resolver that takes a control and returns it's children.
    /// </summary>
    public class ChildResolver {
        private readonly Func<Type, bool> _filter;
        private readonly Func<DependencyObject, IEnumerable<DependencyObject>> _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildResolver"/> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="resolver">The resolver.</param>
        public ChildResolver(Func<Type, bool> filter, Func<DependencyObject, IEnumerable<DependencyObject>> resolver) {
            _filter = filter;
            _resolver = resolver;
        }

        /// <summary>
        /// Can this resolve appy to the given type.
        /// </summary>
        /// <param name="type">The visual tree type.</param>
        /// <returns>Returns true if this resolver applies.</returns>
        public bool CanResolve(Type type)
            => _filter(type);

        /// <summary>
        /// The element from the visual tree for the children to resolve.
        /// </summary>
        /// <param name="obj">The Dependency Object.</param>
        public IEnumerable<DependencyObject> Resolve(DependencyObject obj)
            => _resolver(obj);
    }
}
