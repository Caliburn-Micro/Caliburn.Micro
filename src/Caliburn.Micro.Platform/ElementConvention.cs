using System;
using System.Reflection;

#if WINDOWS_UWP
using Windows.UI.Xaml;

using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif XFORMS
using Xamarin.Forms;

using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
using FrameworkElement = Xamarin.Forms.VisualElement;
#elif MAUI
using Microsoft.Maui.Controls;

using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;
using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
#else
using System.Windows;

using TriggerBase = Microsoft.Xaml.Behaviors.TriggerBase;
#endif

#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    /// Represents the conventions for a particular element type.
    /// </summary>
    public class ElementConvention {
        /// <summary>
        /// Gets or sets the type of element to which the conventions apply.
        /// </summary>
        public Type ElementType { get; set; }

        /// <summary>
        /// Gets or sets the default property to be used in binding conventions.
        /// </summary>
        public Func<DependencyObject, DependencyProperty> GetBindableProperty { get; set; }

        /// <summary>
        /// Gets or sets the default trigger to be used when wiring actions on this element.
        /// </summary>
        public Func<TriggerBase> CreateTrigger { get; set; }

        /// <summary>
        /// Gets or sets the default property to be used for parameters of this type in actions.
        /// </summary>
        public string ParameterProperty { get; set; }

        /// <summary>
        /// Gets or sets func to applies custom conventions for elements of this type.
        /// </summary>
        /// <remarks>Pass the view model type, property path, property instance, framework element and its convention.</remarks>
        public Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool> ApplyBinding { get; set; }
            = (viewModelType, path, property, element, convention)
                => ConventionManager.SetBindingWithoutBindingOverwrite(
                    viewModelType,
                    path,
                    property,
                    element,
                    convention,
                    convention.GetBindableProperty(element));
    }
}
