namespace Caliburn.Micro.Xamarin.Forms {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xamarin.Forms;
    using UIElement = global::Xamarin.Forms.Element;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using DependencyObject = global::Xamarin.Forms.BindableObject;

    /// <summary>
    /// Used to configure the conventions used by the framework to apply bindings and create actions.
    /// </summary>
    public static class ConventionManager {
        static readonly ILog Log = LogManager.GetLog(typeof(ConventionManager));

        /// <summary>
        /// Indicates whether or not static properties should be included during convention name matching.
        /// </summary>
        /// <remarks>False by default.</remarks>
        public static bool IncludeStaticProperties = false;

        /// <summary>
        /// Gets a property by name, ignoring case and searching all interfaces.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="propertyName">The property to search for.</param>
        /// <returns>The property or null if not found.</returns>
        public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName) {
            var typeInfo = type.GetTypeInfo();
            var typeList = new List<Type> { type };

            if (typeInfo.IsInterface) {
                typeList.AddRange(typeInfo.ImplementedInterfaces);
            }

            return typeList
                .Select(interfaceType => interfaceType.GetRuntimeProperty(propertyName))
                .FirstOrDefault(property => property != null);
        }
    }
}
