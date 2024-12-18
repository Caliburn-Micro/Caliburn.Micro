#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    using System;
    using System.Reflection;
#if WINDOWS_UWP
    using Windows.UI.Xaml;
    using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif WinUI3
    using Microsoft.UI.Xaml;
    using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif XFORMS
    using global::Xamarin.Forms;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
#elif AVALONIA
    using DependencyProperty = Avalonia.AvaloniaProperty;
    using DependencyObject = Avalonia.AvaloniaObject;
    using FrameworkElement = Avalonia.Controls.Control;
    using EventTrigger = Avalonia.Xaml.Interactions.Core.EventTriggerBehavior;
    using TriggerBase = Avalonia.Xaml.Interactivity.Trigger;
#elif MAUI
    using global::Microsoft.Maui.Controls;
    using DependencyObject = global::Microsoft.Maui.Controls.BindableObject;
    using DependencyProperty = global::Microsoft.Maui.Controls.BindableProperty;
    using FrameworkElement = global::Microsoft.Maui.Controls.VisualElement;
#else
    using System.Windows;
    using TriggerBase = Microsoft.Xaml.Behaviors.TriggerBase;
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
#if AVALONIA
        /// <summary>
        /// The default trigger to be used when wiring actions on this element.
        /// </summary>
        public Func<EventTrigger> CreateTrigger;
#else
        /// <summary>
        /// The default trigger to be used when wiring actions on this element.
        /// </summary>
        public Func<TriggerBase> CreateTrigger;
#endif
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
