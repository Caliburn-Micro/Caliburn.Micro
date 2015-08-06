#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#else
namespace Caliburn.Micro
#endif
{
    using System;
    using System.Reflection;
#if WinRT81
    using Windows.UI.Xaml;
    using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif XFORMS
    using global::Xamarin.Forms;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
#else
    using System.Windows;
    using TriggerBase = System.Windows.Interactivity.TriggerBase;
#endif

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
        public Func<TriggerBase> CreateTrigger;

        /// <summary>
        /// The default property to be used for parameters of this type in actions.
        /// </summary>
        public string ParameterProperty;

        /// <summary>
        /// Applies custom conventions for elements of this type.
        /// </summary>
        /// <remarks>Pass the view model type, property path, property instance, framework element and its convention.</remarks>
        public Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool> ApplyBinding =
            (viewModelType, path, property, element, convention) => ConventionManager.SetBindingWithoutBindingOverwrite(viewModelType, path, property, element, convention, convention.GetBindableProperty(element));
    }
}