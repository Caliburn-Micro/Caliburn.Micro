namespace Caliburn.Micro
{
    using System;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Represents the conventions for a particular element type.
    /// </summary>
    public class ElementConvention
    {
        /// <summary>
        /// The type of element to which the conventions apply.
        /// </summary>
        public Type ElementType;

        /// <summary>
        /// Gets the default property to be used in binding conventions.
        /// </summary>
        public Func<DependencyObject, DependencyProperty> GetBindableProperty;

        /// <summary>
        /// The default trigger to be used when wiring actions on this element.
        /// </summary>
        public Func<System.Windows.Interactivity.TriggerBase> CreateTrigger;

        /// <summary>
        /// The default property to be used for parameters of this type in actions.
        /// </summary>
        public string ParameterProperty;

        /// <summary>
        /// Applies custom conventions for elements of this type.
        /// </summary>
        public Action<ElementConvention, Type, PropertyInfo, FrameworkElement> ApplyBinding =
            (convention, viewModelType, property, element) => ConventionManager.SetBinding(convention, viewModelType, property, element);
    }
}