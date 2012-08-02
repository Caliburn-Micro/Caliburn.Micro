namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
#if WinRT
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Markup;
    using EventTrigger = Windows.UI.Interactivity.EventTrigger;
    using Windows.UI.Xaml.Shapes;
#else
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Shapes;    
    using EventTrigger = System.Windows.Interactivity.EventTrigger;
#endif
#if !SILVERLIGHT && !WinRT
    using System.Windows.Documents;
#endif

    /// <summary>
    /// Used to configure the conventions used by the framework to apply bindings and create actions.
    /// </summary>
    public static class ConventionManager
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ConventionManager));

        /// <summary>
        /// Converters <see cref="bool"/> to/from <see cref="Visibility"/>.
        /// </summary>
        public static IValueConverter BooleanToVisibilityConverter = new BooleanToVisibilityConverter();

        /// <summary>
        /// Indicates whether or not static properties should be included during convention name matching.
        /// </summary>
        /// <remarks>False by default.</remarks>
        public static bool IncludeStaticProperties = false;

        /// <summary>
        /// Indicates whether or not the Content of ContentControls should be overwritten by conventional bindings.
        /// </summary>
        /// <remarks>False by default.</remarks>
        public static bool OverwriteContent = false;

        /// <summary>
        /// The default DataTemplate used for ItemsControls when required.
        /// </summary>
        public static DataTemplate DefaultItemTemplate = (DataTemplate)
#if SILVERLIGHT || WinRT
XamlReader.Load(
#else
        XamlReader.Parse(
#endif
#if WinRT
"<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:cal='using:Caliburn.Micro'>" +
                "<ContentControl cal:View.Model=\"{Binding}\" VerticalContentAlignment=\"Stretch\" HorizontalContentAlignment=\"Stretch\" IsTabStop=\"False\" />" +
            "</DataTemplate>"
#else
             "<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
                          "xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro'> " +
                "<ContentControl cal:View.Model=\"{Binding}\" VerticalContentAlignment=\"Stretch\" HorizontalContentAlignment=\"Stretch\" IsTabStop=\"False\" />" +
            "</DataTemplate>"
#endif
);

        /// <summary>
        /// The default DataTemplate used for Headered controls when required.
        /// </summary>
        public static DataTemplate DefaultHeaderTemplate = (DataTemplate)
#if SILVERLIGHT || WinRT
XamlReader.Load(
#else
        XamlReader.Parse(
#endif
"<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><TextBlock Text=\"{Binding DisplayName, Mode=TwoWay}\" /></DataTemplate>"
        );

        static readonly Dictionary<Type, ElementConvention> ElementConventions = new Dictionary<Type, ElementConvention>();

        /// <summary>
        /// Changes the provided word from a plural form to a singular form.
        /// </summary>
        public static Func<string, string> Singularize = original =>
        {
            return original.EndsWith("ies")
                ? original.TrimEnd('s').TrimEnd('e').TrimEnd('i') + "y"
                : original.TrimEnd('s');
        };

        /// <summary>
        /// Derives the SelectedItem property name.
        /// </summary>
        public static Func<string, IEnumerable<string>> DerivePotentialSelectionNames = name =>
        {
            var singular = Singularize(name);
            return new[] {
                "Active" + singular,
                "Selected" + singular,
                "Current" + singular
            };
        };

        /// <summary>
        /// Creates a binding and sets it on the element, applying the appropriate conventions.
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <param name="element"></param>
        /// <param name="convention"></param>
        /// <param name="bindableProperty"></param>
        public static Action<Type, string, PropertyInfo, FrameworkElement, ElementConvention, DependencyProperty> SetBinding =
            (viewModelType, path, property, element, convention, bindableProperty) =>
            {
#if WinRT
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

#if WinRT
        /// <summary>
        /// Applies the appropriate binding mode to the binding.
        /// </summary>
        public static Action<Binding, PropertyInfo> ApplyBindingMode = (binding, property) =>
        {
            var setMethod = property.SetMethod;
            binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
        };
#else
          /// <summary>
        /// Applies the appropriate binding mode to the binding.
        /// </summary>
        public static Action<Binding, PropertyInfo> ApplyBindingMode = (binding, property) =>{
            var setMethod = property.GetSetMethod();
            binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
        };
#endif

        /// <summary>
        /// Determines whether or not and what type of validation to enable on the binding.
        /// </summary>
        public static Action<Binding, Type, PropertyInfo> ApplyValidation = (binding, viewModelType, property) =>
        {
#if SILVERLIGHT
            if (typeof(INotifyDataErrorInfo).IsAssignableFrom(viewModelType)) {
                binding.ValidatesOnNotifyDataErrors = true;
                binding.ValidatesOnExceptions = true;
            }
#endif
#if !WinRT
            if (typeof(IDataErrorInfo).IsAssignableFrom(viewModelType)) {
                binding.ValidatesOnDataErrors = true;
                binding.ValidatesOnExceptions = true;
            }
#endif
        };

        /// <summary>
        /// Determines whether a value converter is is needed and applies one to the binding.
        /// </summary>
        public static Action<Binding, DependencyProperty, PropertyInfo> ApplyValueConverter = (binding, bindableProperty, property) =>
        {
            if (bindableProperty == UIElement.VisibilityProperty && typeof(bool).IsAssignableFrom(property.PropertyType))
                binding.Converter = BooleanToVisibilityConverter;
        };

        /// <summary>
        /// Determines whether a custom string format is needed and applies it to the binding.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (binding, convention, property) =>
        {
#if !WinRT
            if(typeof(DateTime).IsAssignableFrom(property.PropertyType))
                binding.StringFormat = "{0:d}";
#endif
        };

        /// <summary>
        /// Determines whether a custom update source trigger should be applied to the binding.
        /// </summary>
        public static Action<DependencyProperty, DependencyObject, Binding, PropertyInfo> ApplyUpdateSourceTrigger = (bindableProperty, element, binding, info) =>
        {
#if SILVERLIGHT && !SL5
            ApplySilverlightTriggers(
                element, 
                bindableProperty, 
                x => x.GetBindingExpression(bindableProperty),
                info,
                binding
                );
#elif !WinRT
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
#endif
        };

        static ConventionManager()
        {
#if !WP71 && !WinRT
            AddElementConvention<DatePicker>(DatePicker.SelectedDateProperty, "SelectedDate", "SelectedDateChanged");
#endif
#if SILVERLIGHT || WinRT
            AddElementConvention<HyperlinkButton>(HyperlinkButton.ContentProperty, "DataContext", "Click");
            AddElementConvention<PasswordBox>(PasswordBox.PasswordProperty, "Password", "PasswordChanged");
#else
            AddElementConvention<PasswordBox>(null, "Password", "PasswordChanged");
            AddElementConvention<Hyperlink>(Hyperlink.DataContextProperty, "DataContext", "Click");
            AddElementConvention<RichTextBox>(RichTextBox.DataContextProperty, "DataContext", "TextChanged");
            AddElementConvention<Menu>(Menu.ItemsSourceProperty,"DataContext", "Click");
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
                    var bindableProperty = convention.GetBindableProperty(element);
                    if(!SetBindingWithoutBindingOverwrite(viewModelType, path, property, element, convention, bindableProperty))
                        return false;

                    var tabControl = (TabControl)element;
                    if(tabControl.ContentTemplate == null 
                        && tabControl.ContentTemplateSelector == null 
                        && property.PropertyType.IsGenericType) {
                        var itemType = property.PropertyType.GetGenericArguments().First();
                        if(!itemType.IsValueType && !typeof(string).IsAssignableFrom(itemType)){
                            tabControl.ContentTemplate = DefaultItemTemplate;
                            Log.Info("ContentTemplate applied to {0}.", element.Name);
                        }
                    }

                    ConfigureSelectedItem(element, Selector.SelectedItemProperty, viewModelType, path);

                    if(string.IsNullOrEmpty(tabControl.DisplayMemberPath))
                        ApplyHeaderTemplate(tabControl, TabControl.ItemTemplateProperty, TabControl.ItemTemplateSelectorProperty, viewModelType);

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
                .ApplyBinding = (viewModelType, path, property, element, convention) =>
                {
                    if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty))
                    {
                        return false;
                    }

                    ConfigureSelectedItem(element, Selector.SelectedItemProperty, viewModelType, path);
                    ApplyItemTemplate((ItemsControl)element, property);

                    return true;
                };
            AddElementConvention<ItemsControl>(ItemsControl.ItemsSourceProperty, "DataContext", "Loaded")
                .ApplyBinding = (viewModelType, path, property, element, convention) =>
                {
                    if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty))
                    {
                        return false;
                    }

                    ApplyItemTemplate((ItemsControl)element, property);

                    return true;
                };
            AddElementConvention<ContentControl>(ContentControl.ContentProperty, "DataContext", "Loaded").GetBindableProperty =
                delegate(DependencyObject foundControl)
                {
                    var element = (ContentControl)foundControl;

                    if (element.Content is DependencyObject && !OverwriteContent)
                        return null;
#if SILVERLIGHT
                    var useViewModel = element.ContentTemplate == null;
#else
                    var useViewModel = element.ContentTemplate == null && element.ContentTemplateSelector == null;
#endif
                    if (useViewModel)
                    {
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
        /// Adds an element convention.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="bindableProperty">The default property for binding conventions.</param>
        /// <param name="parameterProperty">The default property for action parameters.</param>
        /// <param name="eventName">The default event to trigger actions.</param>
        public static ElementConvention AddElementConvention<T>(DependencyProperty bindableProperty, string parameterProperty, string eventName)
        {
            return AddElementConvention(new ElementConvention
            {
                ElementType = typeof(T),
                GetBindableProperty = element => bindableProperty,
                ParameterProperty = parameterProperty,
                CreateTrigger = () => new EventTrigger { EventName = eventName }
            });
        }

        /// <summary>
        /// Adds an element convention.
        /// </summary>
        /// <param name="convention"></param>
        public static ElementConvention AddElementConvention(ElementConvention convention)
        {
            return ElementConventions[convention.ElementType] = convention;
        }

        /// <summary>
        /// Gets an element convention for the provided element type.
        /// </summary>
        /// <param name="elementType">The type of element to locate the convention for.</param>
        /// <returns>The convention if found, null otherwise.</returns>
        /// <remarks>Searches the class hierarchy for conventions.</remarks>
        public static ElementConvention GetElementConvention(Type elementType)
        {
            if (elementType == null)
                return null;

            ElementConvention propertyConvention;
            ElementConventions.TryGetValue(elementType, out propertyConvention);
#if WinRT
            return propertyConvention ?? GetElementConvention(elementType.GetTypeInfo().BaseType);
#else
            return propertyConvention ?? GetElementConvention(elementType.BaseType);
#endif
        }

        /// <summary>
        /// Determines whether a particular dependency property already has a binding on the provided element.
        /// </summary>
        public static bool HasBinding(FrameworkElement element, DependencyProperty property)
        {
#if !WinRT
            return element.GetBindingExpression(property) != null;
#else
            var localValue = element.ReadLocalValue(property);

            if (localValue == DependencyProperty.UnsetValue)
                return false;

            return localValue.GetType().FullName == "System.__ComObject";
#endif
        }

        /// <summary>
        /// Creates a binding and sets it on the element, guarding against pre-existing bindings.
        /// </summary>
        public static bool SetBindingWithoutBindingOverwrite(Type viewModelType, string path, PropertyInfo property, FrameworkElement element, ElementConvention convention, DependencyProperty bindableProperty)
        {
            if (bindableProperty == null || HasBinding(element, bindableProperty))
            {
                return false;
            }

            SetBinding(viewModelType, path, property, element, convention, bindableProperty);
            return true;
        }

        /// <summary>
        /// Creates a binding and set it on the element, guarding against pre-existing bindings and pre-existing values.
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <param name="element"></param>
        /// <param name="convention"></param>
        /// <param name="bindableProperty"> </param>
        /// <returns></returns>
        public static bool SetBindingWithoutBindingOrValueOverwrite(Type viewModelType, string path, PropertyInfo property, FrameworkElement element, ElementConvention convention, DependencyProperty bindableProperty)
        {
            if (bindableProperty == null || HasBinding(element, bindableProperty))
            {
                return false;
            }

            if (element.GetValue(bindableProperty) != null)
            {
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
        public static void ApplyItemTemplate(ItemsControl itemsControl, PropertyInfo property)
        {

            if (!string.IsNullOrEmpty(itemsControl.DisplayMemberPath)
                || HasBinding(itemsControl, ItemsControl.DisplayMemberPathProperty)
                    || itemsControl.ItemTemplate != null)
            {
                return;
            }
#if !WinRT
            if (!property.PropertyType.IsGenericType)
                return;
#endif

#if !WP71 && !WinRT
            var itemType = property.PropertyType.GetGenericArguments().First();
            if (itemType.IsValueType || typeof(string).IsAssignableFrom(itemType)) {
                return;
            }
#endif

#if NET
            if (itemsControl.ItemTemplateSelector == null){
                itemsControl.ItemTemplate = DefaultItemTemplate;
                Log.Info("ItemTemplate applied to {0}.", itemsControl.Name);
            }
#else
            itemsControl.ItemTemplate = DefaultItemTemplate;
            Log.Info("ItemTemplate applied to {0}.", itemsControl.Name);
#endif
        }

        /// <summary>
        /// Configures the selected item convention.
        /// </summary>
        /// <param name="selector">The element that has a SelectedItem property.</param>
        /// <param name="selectedItemProperty">The SelectedItem property.</param>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="path">The property path.</param>
        public static Action<FrameworkElement, DependencyProperty, Type, string> ConfigureSelectedItem =
            (selector, selectedItemProperty, viewModelType, path) =>
            {
                if (HasBinding(selector, selectedItemProperty))
                {
                    return;
                }

                var index = path.LastIndexOf('.');
                index = index == -1 ? 0 : index + 1;
                var baseName = path.Substring(index);

                foreach (var potentialName in DerivePotentialSelectionNames(baseName))
                {
                    if (viewModelType.GetPropertyCaseInsensitive(potentialName) != null)
                    {
                        var selectionPath = path.Replace(baseName, potentialName);
#if WinRT
                        var binding = new Binding { Mode = BindingMode.TwoWay, Path = new PropertyPath(selectionPath) };
#else
                        var binding = new Binding(selectionPath) { Mode = BindingMode.TwoWay };
#endif
                        var shouldApplyBinding = ConfigureSelectedItemBinding(selector, selectedItemProperty, viewModelType, selectionPath, binding);
                        if (shouldApplyBinding)
                        {
                            BindingOperations.SetBinding(selector, selectedItemProperty, binding);
                            Log.Info("SelectedItem binding applied to {0}.", selector.Name);
                            return;
                        }

                        Log.Info("SelectedItem binding not applied to {0} due to 'ConfigureSelectedItemBinding' customization.", selector.Name);
                    }
                }
            };

        /// <summary>
        /// Configures the SelectedItem binding for matched selection path.
        /// </summary>
        /// <param name="selector">The element that has a SelectedItem property.</param>
        /// <param name="selectedItemProperty">The SelectedItem property.</param>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="selectionPath">The property path.</param>
        /// <param name="binding">The binding to configure.</param>
        /// <returns>A bool indicating whether to apply binding</returns>
        public static Func<FrameworkElement, DependencyProperty, Type, string, Binding, bool> ConfigureSelectedItemBinding =
            (selector, selectedItemProperty, viewModelType, selectionPath, binding) =>
            {
                return true;
            };

        /// <summary>
        /// Applies a header template based on <see cref="IHaveDisplayName"/>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="headerTemplateProperty"></param>
        /// <param name="headerTemplateSelectorProperty"> </param>
        /// <param name="viewModelType"></param>
        public static void ApplyHeaderTemplate(FrameworkElement element, DependencyProperty headerTemplateProperty, DependencyProperty headerTemplateSelectorProperty, Type viewModelType)
        {
            var template = element.GetValue(headerTemplateProperty);
            var selector = headerTemplateSelectorProperty != null
                               ? element.GetValue(headerTemplateSelectorProperty)
                               : null;

            if (template != null || selector != null || !typeof(IHaveDisplayName).IsAssignableFrom(viewModelType))
            {
                return;
            }

            element.SetValue(headerTemplateProperty, DefaultHeaderTemplate);
            Log.Info("Header template applied to {0}.", element.Name);
        }
#if WinRT
        /// <summary>
        /// Gets a property by name, ignoring case and searching all interfaces.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="propertyName">The property to search for.</param>
        /// <returns>The property or null if not found.</returns>
        public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName)
        {
            var typeInfo = type.GetTypeInfo();
            var typeList = new List<Type> { type };

            if (typeInfo.IsInterface)
            {
                typeList.AddRange(typeInfo.ImplementedInterfaces);
            }

            return typeList
                .Select(interfaceType => interfaceType.GetRuntimeProperty(propertyName))
                .FirstOrDefault(property => property != null);
        }
#else
        /// <summary>
        /// Gets a property by name, ignoring case and searching all interfaces.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="propertyName">The property to search for.</param>
        /// <returns>The property or null if not found.</returns>
        public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName) {
            var typeList = new List<Type> { type };

            if (type.IsInterface) {
                typeList.AddRange(type.GetInterfaces());
            }

            var flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            if (IncludeStaticProperties) {
                flags = flags | BindingFlags.Static;
            }

            return typeList
                .Select(interfaceType => interfaceType.GetProperty(propertyName, flags))
                .FirstOrDefault(property => property != null);
        }
#endif
#if (SILVERLIGHT && !SL5)
        /// <summary>
        /// Accounts for the lack of UpdateSourceTrigger in silverlight.
        /// </summary>
        /// <param name="element">The element to wire for change events on.</param>
        /// <param name="dependencyProperty">The property that is being bound.</param>
        /// <param name="expressionSource">Gets the the binding expression that needs to be updated.</param>
        /// <param name="property">The property being bound to if available.</param>
        /// <param name="binding">The binding if available.</param>
        public static void ApplySilverlightTriggers(DependencyObject element, DependencyProperty dependencyProperty, Func<FrameworkElement, BindingExpression> expressionSource, PropertyInfo property, Binding binding){
            var textBox = element as TextBox;
            if (textBox != null && dependencyProperty == TextBox.TextProperty) {
                if (property != null) {
                    var typeCode = Type.GetTypeCode(property.PropertyType);
                    if (typeCode == TypeCode.Single || typeCode == TypeCode.Double || typeCode == TypeCode.Decimal) {
                        binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;
                        textBox.KeyUp += delegate {
                            var start = textBox.SelectionStart;
                            var text = textBox.Text;

                            expressionSource(textBox).UpdateSource();

                            textBox.Text = text;
                            textBox.SelectionStart = start;
                        };
                        return;
                    }
                }

                textBox.TextChanged += delegate { expressionSource(textBox).UpdateSource(); };
                return;
            }

            var passwordBox = element as PasswordBox;
            if (passwordBox != null && dependencyProperty == PasswordBox.PasswordProperty) {
                passwordBox.PasswordChanged += delegate { expressionSource(passwordBox).UpdateSource(); };
                return;
            }
        }
#endif
    }
}
