namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Shapes;

#if !SILVERLIGHT
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

#if SILVERLIGHT
        /// <summary>
        /// The default DataTemplate used for ItemsControls when required.
        /// </summary>
        public static DataTemplate DefaultDataTemplate = (DataTemplate)XamlReader.Load(
#else
        /// <summary>
        /// The default DataTemplate used for ItemsControls when required.
        /// </summary>
        public static DataTemplate DefaultDataTemplate = (DataTemplate)XamlReader.Parse(
#endif
            "<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
                          "xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro'> " +
                "<ContentControl cal:View.Model=\"{Binding}\" VerticalContentAlignment=\"Stretch\" HorizontalContentAlignment=\"Stretch\" />" +
            "</DataTemplate>"
            );

        static readonly Dictionary<Type, ElementConvention> ElementConventions = new Dictionary<Type, ElementConvention>();

        /// <summary>
        /// Changes the provided word from a plural form to a singular form.
        /// </summary>
        public static Func<string, string> Singularize = original =>{
            return original.TrimEnd('s');
        };

        /// <summary>
        /// Derives the SelectedItem property name.
        /// </summary>
        public static Func<string, IEnumerable<string>> DerivePotentialSelectionNames = name =>{
            var singular = Singularize(name);
            return new[] {
                "Active" + singular,
                "Selected" + singular,
                "Current" + singular
            };
        };

        /// <summary>
        /// Applies the appropriate binding mode to the binding.
        /// </summary>
        public static Action<Binding, PropertyInfo> ApplyBindingMode = (binding, property) =>{
            var setMethod = property.GetSetMethod();
            binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
        };

        /// <summary>
        /// Determines whether or not and what type of validation to enable on the binding.
        /// </summary>
        public static Action<Binding, Type, PropertyInfo> ApplyValidation = (binding, viewModelType, property) => {
#if SILVERLIGHT && !WP7
            if(typeof(INotifyDataErrorInfo).IsAssignableFrom(viewModelType))
                binding.ValidatesOnNotifyDataErrors = true;
#endif
#if !WP7
            if(typeof(IDataErrorInfo).IsAssignableFrom(viewModelType))
                binding.ValidatesOnDataErrors = true;
#endif
        };

        /// <summary>
        /// Determines whether a value converter is is needed and applies one to the binding.
        /// </summary>
        public static Action<Binding, DependencyProperty, PropertyInfo> ApplyValueConverter = (binding, bindableProperty, property) =>{
            if (bindableProperty == UIElement.VisibilityProperty && typeof(bool).IsAssignableFrom(property.PropertyType))
                binding.Converter = BooleanToVisibilityConverter;
        };

        /// <summary>
        /// Determines whether a custom string format is needed and applies it to the binding.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (binding, convention, property) =>{
#if !WP7
            if(typeof(DateTime).IsAssignableFrom(property.PropertyType))
                binding.StringFormat = "{0:MM/dd/yyyy}";
#endif
        };

        /// <summary>
        /// Determines whether a custom update source trigger should be applied to the binding.
        /// </summary>
        public static Action<DependencyProperty, DependencyObject, Binding> ApplyUpdateSourceTrigger = (bindableProperty, element, binding) =>{
#if SILVERLIGHT
            ApplySilverlightTriggers(element, bindableProperty, x => x.GetBindingExpression(bindableProperty));
#else
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
#endif
        };

        /// <summary>
        /// Determines whether a particular dependency property already has a binding on the provided element.
        /// </summary>
        public static Func<FrameworkElement, DependencyProperty, bool> HasBinding = (element, property) =>
        {
            var exists = element.GetBindingExpression(property) != null;

            if (exists)
                Log.Info("Binding exists on {0}.", element.Name);

            return exists;
        };

        /// <summary>
        /// Creates a binding and sets it on the element.
        /// </summary>
        public static Func<ElementConvention, Type, PropertyInfo, FrameworkElement, bool> SetBinding =
            (convention, viewModelType, property, element) => {
                var bindableProperty = convention.GetBindableProperty(element);
                if(HasBinding(element, bindableProperty))
                    return false;

                var binding = new Binding(property.Name);

                ApplyBindingMode(binding, property);
                ApplyValueConverter(binding, bindableProperty, property);
                ApplyStringFormat(binding, convention, property);
                ApplyValidation(binding, viewModelType, property);
                ApplyUpdateSourceTrigger(bindableProperty, element, binding);

                BindingOperations.SetBinding(element, bindableProperty, binding);

                return true;
            };

        static ConventionManager()
        {
#if SILVERLIGHT
            AddElementConvention<HyperlinkButton>(HyperlinkButton.ContentProperty, "DataContext", "Click");
            AddElementConvention<PasswordBox>(PasswordBox.PasswordProperty, "Password", "PasswordChanged");
#else
            AddElementConvention<PasswordBox>(PasswordBox.DataContextProperty, "DataContext", "PasswordChanged");
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
                .ApplyBinding = (convention, viewModelType, property, element) => {
                    if(!SetBinding(convention, viewModelType, property, element))
                        return;

                    var tabControl = (TabControl)element;
                    if(tabControl.ContentTemplate == null && property.PropertyType.IsGenericType) {
                        var itemType = property.PropertyType.GetGenericArguments().First();
                        if(!itemType.IsValueType && !typeof(string).IsAssignableFrom(itemType))
                            tabControl.ContentTemplate = DefaultDataTemplate;
                    }

                    ConfigureSelector((Selector)element, viewModelType, property);
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
            AddElementConvention<Selector>(Selector.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                .ApplyBinding = (convention, viewModelType, property, element) => {
                    if (!SetBinding(convention, viewModelType, property, element))
                        return;

                    ConfigureSelector((Selector)element, viewModelType, property);
                    ConfigureItemsControl((ItemsControl)element, property);
                };
            AddElementConvention<ItemsControl>(ItemsControl.ItemsSourceProperty, "DataContext", "Loaded")
                .ApplyBinding = (convention, viewModelType, property, element) => {
                    if (!SetBinding(convention, viewModelType, property, element))
                        return;

                    ConfigureItemsControl((ItemsControl)element, property);
                };
            AddElementConvention<ContentControl>(ContentControl.ContentProperty, "DataContext", "Loaded").GetBindableProperty =
                delegate(DependencyObject foundControl) {
                    var element = (ContentControl)foundControl;
#if SILVERLIGHT
                    return element.ContentTemplate == null && !(element.Content is DependencyObject)
                        ? View.ModelProperty
                        : ContentControl.ContentProperty;
#else
                    return element.ContentTemplate == null && element.ContentTemplateSelector == null && !(element.Content is DependencyObject)
                        ? View.ModelProperty
                        : ContentControl.ContentProperty;
#endif
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
        public static ElementConvention AddElementConvention<T>(DependencyProperty bindableProperty, string parameterProperty, string eventName) {
            return AddElementConvention(new ElementConvention {
                ElementType = typeof(T),
                GetBindableProperty = element => bindableProperty,
                ParameterProperty = parameterProperty,
                CreateTrigger = () => new System.Windows.Interactivity.EventTrigger { EventName = eventName }
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
            return propertyConvention ?? GetElementConvention(elementType.BaseType);
        }

        private static void ConfigureItemsControl(ItemsControl itemsControl, PropertyInfo property) {
            if(string.IsNullOrEmpty(itemsControl.DisplayMemberPath)
                && !HasBinding(itemsControl, ItemsControl.DisplayMemberPathProperty)
                    && itemsControl.ItemTemplate == null
                        && property.PropertyType.IsGenericType) {
#if !WP7
                var itemType = property.PropertyType.GetGenericArguments().First();
                if(!itemType.IsValueType && !typeof(string).IsAssignableFrom(itemType))
#endif
                    itemsControl.ItemTemplate = DefaultDataTemplate;
            }
        }

        private static void ConfigureSelector(Selector selector, Type viewModelType, PropertyInfo property) {
            if(HasBinding(selector, Selector.SelectedItemProperty))
                return;

            foreach(var potentialName in DerivePotentialSelectionNames(property.Name)) {
                if(viewModelType.GetProperty(potentialName) != null) {
                    BindingOperations.SetBinding(selector, Selector.SelectedItemProperty, new Binding(potentialName) { Mode = BindingMode.TwoWay });
                    return;
                }
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Accounts for the lack of UpdateSourceTrigger in silverlight.
        /// </summary>
        /// <param name="element">The element to wire for change events on.</param>
        /// <param name="dependencyProperty">The rproperty that is being bound.</param>
        /// <param name="expressionSource">The source of the binding expression tht needs to be updated.</param>
        public static void ApplySilverlightTriggers(DependencyObject element, DependencyProperty dependencyProperty, Func<FrameworkElement, BindingExpression> expressionSource)
        {
            var textBox = element as TextBox;
            if (textBox != null && dependencyProperty == TextBox.TextProperty)
            {
                textBox.TextChanged += delegate { expressionSource(textBox).UpdateSource(); };
                return;
            }

            var passwordBox = element as PasswordBox;
            if (passwordBox != null && dependencyProperty == PasswordBox.PasswordProperty)
            {
                passwordBox.PasswordChanged += delegate { expressionSource(passwordBox).UpdateSource(); };
                return;
            }
        }
#endif
    }
}