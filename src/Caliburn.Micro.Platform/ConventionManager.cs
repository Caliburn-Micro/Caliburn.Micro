using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;

using EventTrigger = Microsoft.Xaml.Interactions.Core.EventTriggerBehavior;
#else
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Shapes;

using EventTrigger = Microsoft.Xaml.Behaviors.EventTrigger;
#endif

namespace Caliburn.Micro {
    /// <summary>
    /// Used to configure the conventions used by the framework to apply bindings and create actions.
    /// </summary>
    public static class ConventionManager {
        private const string DefaultHeaderTemplateXamlText = @"
<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <TextBlock Text=""{Binding DisplayName, Mode=TwoWay}"" />
</DataTemplate>";

        private static readonly ILog Log = LogManager.GetLog(typeof(ConventionManager));
        private static readonly Dictionary<Type, ElementConvention> ElementConventions = new Dictionary<Type, ElementConvention>();

        static ConventionManager() {
#if WINDOWS_UWP
            AddElementConvention<SplitView>(SplitView.ContentProperty, "IsPaneOpen", "PaneClosing").GetBindableProperty
                = foundControl
                    => {
                        var element = (SplitView)foundControl;
                        if (!OverwriteContent) {
                            return null;
                        }

                        Log.Info("ViewModel bound on {0}.", element.Name);

                        return View.ModelProperty;
                    };
            AddElementConvention<DatePicker>(DatePicker.DateProperty, "Date", "DateChanged");
            AddElementConvention<TimePicker>(TimePicker.TimeProperty, "Time", "TimeChanged");
            AddElementConvention<Hub>(Hub.HeaderProperty, "Header", "Loaded");
            AddElementConvention<HubSection>(HubSection.HeaderProperty, "Header", "SectionsInViewChanged");
            AddElementConvention<MenuFlyoutItem>(MenuFlyoutItem.TextProperty, "Text", "Click");
            AddElementConvention<ToggleMenuFlyoutItem>(ToggleMenuFlyoutItem.IsCheckedProperty, "IsChecked", "Click");
            AddElementConvention<SearchBox>(SearchBox.QueryTextProperty, "QueryText", "QuerySubmitted");
            AddElementConvention<ToggleSwitch>(ToggleSwitch.IsOnProperty, "IsOn", "Toggled");
            AddElementConvention<ProgressRing>(ProgressRing.IsActiveProperty, "IsActive", "Loaded");
            AddElementConvention<Slider>(Slider.ValueProperty, "Value", "ValueChanged");
            AddElementConvention<RichEditBox>(RichEditBox.DataContextProperty, "DataContext", "TextChanged");
            AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                .ApplyBinding = (viewModelType, path, property, element, convention) => {
                    if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty)) {
                        return false;
                    }

                    ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                    ApplyItemTemplate((ItemsControl)element, property);

                    return true;
                };
            AddElementConvention<HyperlinkButton>(HyperlinkButton.ContentProperty, "DataContext", "Click");
            AddElementConvention<PasswordBox>(PasswordBox.PasswordProperty, "Password", "PasswordChanged");
#else
            AddElementConvention<DatePicker>(DatePicker.SelectedDateProperty, "SelectedDate", "SelectedDateChanged");
            AddElementConvention<DocumentViewer>(DocumentViewer.DocumentProperty, "DataContext", "Loaded");
            AddElementConvention<PasswordBox>(null, "Password", "PasswordChanged");
            AddElementConvention<Hyperlink>(Hyperlink.DataContextProperty, "DataContext", "Click");
            AddElementConvention<RichTextBox>(RichTextBox.DataContextProperty, "DataContext", "TextChanged");
            AddElementConvention<Menu>(Menu.ItemsSourceProperty, "DataContext", "Click");
            AddElementConvention<MenuItem>(MenuItem.ItemsSourceProperty, "DataContext", "Click");
            AddElementConvention<Label>(Label.ContentProperty, "Content", "DataContextChanged");
            AddElementConvention<Slider>(Slider.ValueProperty, "Value", "ValueChanged");
            AddElementConvention<Expander>(Expander.IsExpandedProperty, "IsExpanded", "Expanded");
            AddElementConvention<StatusBar>(StatusBar.ItemsSourceProperty, "DataContext", "Loaded");
            AddElementConvention<ToolBar>(ToolBar.ItemsSourceProperty, "DataContext", "Loaded");
            AddElementConvention<ToolBarTray>(ToolBarTray.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<TreeView>(TreeView.ItemsSourceProperty, "SelectedItem", "SelectedItemChanged");
            AddElementConvention<TabControl>(TabControl.ItemsSourceProperty, "ItemsSource", "SelectionChanged")
                .ApplyBinding = (viewModelType, path, property, element, convention) => {
                    DependencyProperty bindableProperty = convention.GetBindableProperty(element);
                    if (!SetBindingWithoutBindingOverwrite(viewModelType, path, property, element, convention, bindableProperty)) {
                        return false;
                    }

                    var tabControl = (TabControl)element;
                    if (tabControl.ContentTemplate == null
                        && tabControl.ContentTemplateSelector == null
                        && property.PropertyType.IsGenericType) {
                        Type itemType = property.PropertyType.GetGenericArguments().First();
                        if (!itemType.IsValueType && !typeof(string).IsAssignableFrom(itemType)) {
                            tabControl.ContentTemplate = DefaultItemTemplate;
                            Log.Info("ContentTemplate applied to {0}.", element.Name);
                        }
                    }

                    ConfigureSelectedItem(element, Selector.SelectedItemProperty, viewModelType, path);

                    if (string.IsNullOrEmpty(tabControl.DisplayMemberPath)) {
                        ApplyHeaderTemplate(tabControl, TabControl.ItemTemplateProperty, TabControl.ItemTemplateSelectorProperty, viewModelType);
                    }

                    return true;
                };
            AddElementConvention<TabItem>(TabItem.ContentProperty, "DataContext", "DataContextChanged");
            AddElementConvention<Window>(Window.DataContextProperty, "DataContext", "Loaded");
#endif
            AddElementConvention<UserControl>(UserControl.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Image>(Image.SourceProperty, "Source", "Loaded");
            AddElementConvention<ToggleButton>(ToggleButton.IsCheckedProperty, "IsChecked", "Click");
            AddElementConvention<ButtonBase>(ButtonBase.ContentProperty, "DataContext", "Click");
            AddElementConvention<TextBox>(TextBox.TextProperty, "Text", "TextChanged");
            AddElementConvention<TextBlock>(TextBlock.TextProperty, "Text", "DataContextChanged");
            AddElementConvention<ProgressBar>(ProgressBar.ValueProperty, "Value", "ValueChanged");
            AddElementConvention<Selector>(Selector.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                .ApplyBinding = (viewModelType, path, property, element, convention) => {
                    if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty)) {
                        return false;
                    }

                    ConfigureSelectedItem(element, Selector.SelectedItemProperty, viewModelType, path);
                    ApplyItemTemplate((ItemsControl)element, property);

                    return true;
                };
            AddElementConvention<ItemsControl>(ItemsControl.ItemsSourceProperty, "DataContext", "Loaded")
                .ApplyBinding = (viewModelType, path, property, element, convention) => {
                    if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty)) {
                        return false;
                    }

                    ApplyItemTemplate((ItemsControl)element, property);

                    return true;
                };
            AddElementConvention<ContentControl>(ContentControl.ContentProperty, "DataContext", "Loaded").GetBindableProperty
                = foundControl => {
                    var element = (ContentControl)foundControl;

                    if (element.Content is DependencyObject && !OverwriteContent) {
                        return null;
                    }

                    bool useViewModel = element.ContentTemplate == null && element.ContentTemplateSelector == null;

                    if (useViewModel) {
                        Log.Info("ViewModel bound on {0}.", element.Name);
                        return View.ModelProperty;
                    }

                    Log.Info("Content bound on {0}. Template or content was present.", element.Name);
                    return ContentControl.ContentProperty;
                };
            AddElementConvention<Shape>(Shape.VisibilityProperty, "DataContext", "MouseLeftButtonUp");
            AddElementConvention<FrameworkElement>(FrameworkElement.VisibilityProperty, "DataContext", "Loaded");
        }

        /// <summary>
        /// Gets or sets converter to convert <see cref="bool"/> to/from <see cref="Visibility"/>.
        /// </summary>
        public static IValueConverter BooleanToVisibilityConverter { get; set; }
            = new BooleanToVisibilityConverter();

        /// <summary>
        /// Gets or sets a value indicating whether or not static properties should be included during convention name matching.
        /// </summary>
        /// <remarks>False by default.</remarks>
        public static bool IncludeStaticProperties { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the Content of ContentControls should be overwritten by conventional bindings.
        /// </summary>
        /// <remarks>False by default.</remarks>
        public static bool OverwriteContent { get; set; }

        /// <summary>
        /// Gets or sets the default DataTemplate used for ItemsControls when required.
        /// </summary>
        public static DataTemplate DefaultItemTemplate { get; set; }
            =
#if WINDOWS_UWP
            (DataTemplate)XamlReader.Load("<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:cal='using:Caliburn.Micro'>" +
                "<ContentControl cal:View.Model=\"{Binding}\" VerticalContentAlignment=\"Stretch\" HorizontalContentAlignment=\"Stretch\" IsTabStop=\"False\" />" +
            "</DataTemplate>");
#else
             (DataTemplate)XamlReader.Parse("<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
                           "xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro.Platform'> " +
                "<ContentControl cal:View.Model=\"{Binding}\" VerticalContentAlignment=\"Stretch\" HorizontalContentAlignment=\"Stretch\" IsTabStop=\"False\" />" +
            "</DataTemplate>");
#endif

        /// <summary>
        /// Gets or sets the default DataTemplate used for Headered controls when required.
        /// </summary>
        public static DataTemplate DefaultHeaderTemplate { get; set; }
            =
#if WINDOWS_UWP
        (DataTemplate)XamlReader.Load(DefaultHeaderTemplateXamlText);
#else
        (DataTemplate)XamlReader.Parse(DefaultHeaderTemplateXamlText);
#endif

        /// <summary>
        /// Gets or sets func to change the provided word from a plural form to a singular form.
        /// </summary>
        public static Func<string, string> Singularize { get; set; }
            = original
                => original.EndsWith("ies", StringComparison.OrdinalIgnoreCase)
                    ? original.TrimEnd('s').TrimEnd('e').TrimEnd('i') + "y"
                    : original.TrimEnd('s');

        /// <summary>
        /// Gets or sets func to derive the SelectedItem property name.
        /// </summary>
        public static Func<string, IEnumerable<string>> DerivePotentialSelectionNames { get; set; }
            = name => {
                string singular = Singularize(name);
                return new[] {
                    "Active" + singular,
                    "Selected" + singular,
                    "Current" + singular,
                };
            };

        /// <summary>
        /// Gets or sets action to create a binding and sets it on the element, applying the appropriate conventions.
        /// </summary>
        public static Action<Type, string, PropertyInfo, FrameworkElement, ElementConvention, DependencyProperty> SetBinding { get; set; }
            = (viewModelType, path, property, element, convention, bindableProperty)
                => {
#if WINDOWS_UWP
                    var binding = new Binding { Path = new PropertyPath(path) };
#else
                    var binding = new Binding(path);
#endif
                    ApplyBindingMode(binding, property);
                    ApplyValueConverter(binding, bindableProperty, property);
                    ApplyStringFormat(binding, convention, property);
                    ApplyValidation(binding, viewModelType, property);
                    ApplyUpdateSourceTrigger(bindableProperty, element, binding, property);

                    BindingOperations.SetBinding(element, bindableProperty, binding);
                };

        /// <summary>
        /// Gets or sets action to apply the appropriate binding mode to the binding.
        /// </summary>
        public static Action<Binding, PropertyInfo> ApplyBindingMode { get; set; }
            = (binding, property)
                => {
#if WINDOWS_UWP
                    MethodInfo setMethod = property.SetMethod;
                    binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
#else
                    MethodInfo setMethod = property.GetSetMethod();
                    binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
#endif
                };

        /// <summary>
        /// Gets or sets action to determine whether or not and what type of validation to enable on the binding.
        /// </summary>
        public static Action<Binding, Type, PropertyInfo> ApplyValidation { get; set; }
            = (binding, viewModelType, property)
                => {
#if NET45
                    if (typeof(INotifyDataErrorInfo).IsAssignableFrom(viewModelType)) {
                        binding.ValidatesOnNotifyDataErrors = true;
                        binding.ValidatesOnExceptions = true;
                    }
#endif
#if !WINDOWS_UWP
                    if (typeof(IDataErrorInfo).IsAssignableFrom(viewModelType)) {
                        binding.ValidatesOnDataErrors = true;
                        binding.ValidatesOnExceptions = true;
                    }
#endif
                };

        /// <summary>
        /// Gets or sets action to determine whether a value converter is is needed and applies one to the binding.
        /// </summary>
        public static Action<Binding, DependencyProperty, PropertyInfo> ApplyValueConverter { get; set; }
            = (binding, bindableProperty, property)
                => {
                    if (bindableProperty == UIElement.VisibilityProperty && typeof(bool).IsAssignableFrom(property.PropertyType)) {
                        binding.Converter = BooleanToVisibilityConverter;
                    }
                };

        /// <summary>
        /// Gets or sets action to determine whether a custom string format is needed and applies it to the binding.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat { get; set; }
            = (binding, convention, property) => {
#if !WINDOWS_UWP
                if (typeof(DateTime).IsAssignableFrom(property.PropertyType)) {
                    binding.StringFormat = "{0:d}";
                }
#endif
            };

        /// <summary>
        /// Gets or sets action to determine whether a custom update source trigger should be applied to the binding.
        /// </summary>
        public static Action<DependencyProperty, DependencyObject, Binding, PropertyInfo> ApplyUpdateSourceTrigger { get; set; }
            = (bindableProperty, element, binding, info)
                => {
#if WINDOWS_UWP || NET || CAL_NETCORE
                    _ = string.Empty;
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
#endif
                };

        /// <summary>
        /// Gets or sets action to configure the selected item convention.
        /// </summary>
        public static Action<FrameworkElement, DependencyProperty, Type, string> ConfigureSelectedItem { get; set; } =
            (selector, selectedItemProperty, viewModelType, path)
                => {
                    if (HasBinding(selector, selectedItemProperty)) {
                        return;
                    }

                    int index = path.LastIndexOf('.');
                    index = index == -1 ? 0 : index + 1;
                    string baseName = path.Substring(index);

                    foreach (string potentialName in DerivePotentialSelectionNames(baseName)) {
                        if (viewModelType.GetPropertyCaseInsensitive(potentialName) != null) {
                            string selectionPath = path.Replace(baseName, potentialName);
#if WINDOWS_UWP
                            var binding = new Binding { Mode = BindingMode.TwoWay, Path = new PropertyPath(selectionPath) };
#else
                            var binding = new Binding(selectionPath) { Mode = BindingMode.TwoWay };
#endif
                            bool shouldApplyBinding = ConfigureSelectedItemBinding(selector, selectedItemProperty, viewModelType, selectionPath, binding);
                            if (shouldApplyBinding) {
                                BindingOperations.SetBinding(selector, selectedItemProperty, binding);
                                Log.Info("SelectedItem binding applied to {0}.", selector.Name);
                                return;
                            }

                            Log.Info("SelectedItem binding not applied to {0} due to 'ConfigureSelectedItemBinding' customization.", selector.Name);
                        }
                    }
                };

        /// <summary>
        /// Gets or sets func to configure the SelectedItem binding for matched selection path.
        /// </summary>
        public static Func<FrameworkElement, DependencyProperty, Type, string, Binding, bool> ConfigureSelectedItemBinding { get; set; }
            = (selector, selectedItemProperty, viewModelType, selectionPath, binding)
            => true;

        /// <summary>
        /// Adds an element convention.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="bindableProperty">The default property for binding conventions.</param>
        /// <param name="parameterProperty">The default property for action parameters.</param>
        /// <param name="eventName">The default event to trigger actions.</param>
        public static ElementConvention AddElementConvention<T>(DependencyProperty bindableProperty, string parameterProperty, string eventName)
            => AddElementConvention(new ElementConvention {
                ElementType = typeof(T),
                GetBindableProperty = element => bindableProperty,
                ParameterProperty = parameterProperty,
                CreateTrigger = () => new EventTrigger { EventName = eventName },
            });

        /// <summary>
        /// Adds an element convention.
        /// </summary>
        /// <param name="convention">The element convention.</param>
        public static ElementConvention AddElementConvention(ElementConvention convention)
            => ElementConventions[convention.ElementType] = convention;

        /// <summary>
        /// Gets an element convention for the provided element type.
        /// </summary>
        /// <param name="elementType">The type of element to locate the convention for.</param>
        /// <returns>The convention if found, null otherwise.</returns>
        /// <remarks>Searches the class hierarchy for conventions.</remarks>
        public static ElementConvention GetElementConvention(Type elementType) {
            if (elementType == null) {
                return null;
            }

            ElementConventions.TryGetValue(elementType, out ElementConvention propertyConvention);
#if WINDOWS_UWP
            return propertyConvention ?? GetElementConvention(elementType.GetTypeInfo().BaseType);
#else
            return propertyConvention ?? GetElementConvention(elementType.BaseType);
#endif
        }

        /// <summary>
        /// Determines whether a particular dependency property already has a binding on the provided element.
        /// </summary>
        public static bool HasBinding(FrameworkElement element, DependencyProperty property) =>
#if NET || CAL_NETCORE
            BindingOperations.GetBindingBase(element, property) != null;
#else
            element.GetBindingExpression(property) != null;
#endif

        /// <summary>
        /// Creates a binding and sets it on the element, guarding against pre-existing bindings.
        /// </summary>
        public static bool SetBindingWithoutBindingOverwrite(
            Type viewModelType,
            string path,
            PropertyInfo property,
            FrameworkElement element,
            ElementConvention convention,
            DependencyProperty bindableProperty) {
            if (bindableProperty == null || HasBinding(element, bindableProperty)) {
                return false;
            }

            SetBinding(viewModelType, path, property, element, convention, bindableProperty);
            return true;
        }

        /// <summary>
        /// Creates a binding and set it on the element, guarding against pre-existing bindings and pre-existing values.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="path">The path.</param>
        /// <param name="property">The property.</param>
        /// <param name="element">The element.</param>
        /// <param name="convention">The convention.</param>
        /// <param name="bindableProperty">The bindable property.</param>
        public static bool SetBindingWithoutBindingOrValueOverwrite(
            Type viewModelType,
            string path,
            PropertyInfo property,
            FrameworkElement element,
            ElementConvention convention,
            DependencyProperty bindableProperty) {
            if (bindableProperty == null || HasBinding(element, bindableProperty)) {
                return false;
            }

            if (element.GetValue(bindableProperty) != null) {
                return false;
            }

            SetBinding(viewModelType, path, property, element, convention, bindableProperty);
            return true;
        }

        /// <summary>
        /// Attempts to apply the default item template to the items control.
        /// </summary>
        /// <param name="itemsControl">The items control.</param>
        /// <param name="property">The collection property.</param>
        public static void ApplyItemTemplate(ItemsControl itemsControl, PropertyInfo property) {
            if (!string.IsNullOrEmpty(itemsControl.DisplayMemberPath)
                || HasBinding(itemsControl, ItemsControl.DisplayMemberPathProperty)
                || itemsControl.ItemTemplate != null) {
                return;
            }

#if !WINDOWS_UWP
            if (property.PropertyType.IsGenericType) {
                Type itemType = property.PropertyType.GetGenericArguments().First();
                if (itemType.IsValueType || typeof(string).IsAssignableFrom(itemType)) {
                    return;
                }
            }
#else
            if (property.PropertyType.GetTypeInfo().IsGenericType) {
                Type itemType = property.PropertyType.GenericTypeArguments.First();
                if (itemType.GetTypeInfo().IsValueType || typeof(string).IsAssignableFrom(itemType)) {
                    return;
                }
            }
#endif

            if (itemsControl.ItemTemplateSelector == null) {
                itemsControl.ItemTemplate = DefaultItemTemplate;
                Log.Info("ItemTemplate applied to {0}.", itemsControl.Name);
            }
        }

        /// <summary>
        /// Applies a header template based on <see cref="IHaveDisplayName"/>.
        /// </summary>
        /// <param name="element">The element to apply the header template to.</param>
        /// <param name="headerTemplateProperty">The depdendency property for the hdeader.</param>
        /// <param name="headerTemplateSelectorProperty">The selector dependency property.</param>
        /// <param name="viewModelType">The type of the view model.</param>
        public static void ApplyHeaderTemplate(FrameworkElement element, DependencyProperty headerTemplateProperty, DependencyProperty headerTemplateSelectorProperty, Type viewModelType) {
            object template = element.GetValue(headerTemplateProperty);
            object selector = headerTemplateSelectorProperty != null
                               ? element.GetValue(headerTemplateSelectorProperty)
                               : null;

            if (template != null || selector != null || !typeof(IHaveDisplayName).IsAssignableFrom(viewModelType)) {
                return;
            }

            element.SetValue(headerTemplateProperty, DefaultHeaderTemplate);
            Log.Info("Header template applied to {0}.", element.Name);
        }

        /// <summary>
        /// Gets a property by name, ignoring case and searching all interfaces.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="propertyName">The property to search for.</param>
        /// <returns>The property or null if not found.</returns>
        public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName) {
#if WINDOWS_UWP
            TypeInfo typeInfo = type.GetTypeInfo();
            var typeList = new List<Type> { type };

            if (typeInfo.IsInterface) {
                typeList.AddRange(typeInfo.ImplementedInterfaces);
            }

            return typeList
                .Select(interfaceType => interfaceType.GetRuntimeProperty(propertyName))
                .FirstOrDefault(property => property != null);
#else
            var typeList = new List<Type> { type };

            if (type.IsInterface) {
                typeList.AddRange(type.GetInterfaces());
            }

            BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            if (IncludeStaticProperties) {
                flags |= BindingFlags.Static;
            }

            return typeList
                .Select(interfaceType => interfaceType.GetProperty(propertyName, flags))
                .FirstOrDefault(property => property != null);
#endif
        }
    }
}
