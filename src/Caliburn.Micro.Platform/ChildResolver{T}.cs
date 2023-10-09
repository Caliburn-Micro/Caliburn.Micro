using System;
using System.Collections.Generic;

#if WINDOWS_UWP
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace Caliburn.Micro {
    /// <summary>
    /// Generic strongly typed child resolver.
    /// </summary>
    /// <typeparam name="T">The type to filter on.</typeparam>
    public class ChildResolver<T> : ChildResolver
        where T : DependencyObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChildResolver{T}"/> class.
        /// </summary>
        /// <param name="resolver">func to create list of dependency objects.</param>
        public ChildResolver(Func<T, IEnumerable<DependencyObject>> resolver)
            : base(
            t => typeof(T).IsAssignableFrom(t),
            o => resolver((T)o)) {
        }
    }
}
