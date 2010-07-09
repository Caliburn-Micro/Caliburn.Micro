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

#if !SILVERLIGHT
    using System.Windows.Documents;
#endif

    /// <summary>
    /// Used to configure the conventions used by the framework to apply bindings and create actions.
    /// </summary>
    public static class ConventionManager
    {
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
                          "xmlns:cal='http://www.caliburnproject.org'> " +
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
        /// Transforms a name into a guard name.
        /// </summary>
        /// <remarks>For example, the name "Save" would become "CanSave" by default.</remarks>
        public static Func<string, string> DeriveGuardName = name =>{
            return "Can" + name;
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
        /// Applies the appropriate binding mode to the binding expression.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyBindingMode = (binding, convention, property) =>{
            var setMethod = property.GetSetMethod();
            binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
        };

        /// <summary>
        /// Determines whether or not and what type of validation to enable on the binding expression.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyValidation = (binding, convention, property) => {
#if SILVERLIGHT && !WP7
            if(typeof(INotifyDataErrorInfo).IsAssignableFrom(property.DeclaringType))
                binding.ValidatesOnNotifyDataErrors = true;
#endif
#if !WP7
            if(typeof(IDataErrorInfo).IsAssignableFrom(property.DeclaringType))
                binding.ValidatesOnDataErrors = true;
#endif
        };

        /// <summary>
        /// Determine whether a value converter is is needed and applies one if available.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyValueConverter = (binding, convention, property) =>{
            if(convention.BindableProperty == UIElement.VisibilityProperty && typeof(bool).IsAssignableFrom(property.PropertyType))
                binding.Converter = BooleanToVisibilityConverter;
        };

        /// <summary>
        /// Determines whether a custom string format is needed and applies one if so.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (binding, convention, property) =>{
#if !WP7
            if(typeof(DateTime).IsAssignableFrom(property.PropertyType))
                binding.StringFormat = "{0:MM/dd/yyyy}";
#endif
        };

        /// <summary>
        /// Inspect the dependency property which will be bound by default and alter it if necessary.
        /// </summary>
        public static Func<ElementConvention, DependencyObject, DependencyProperty> EnsureDependencyProperty = (convention, foundControl) =>{
            var element = foundControl as ContentControl;
            if(element == null)
                return convention.BindableProperty;
#if SILVERLIGHT
            return element.ContentTemplate == null && !(element.Content is DependencyObject)
                ? View.ModelProperty
                : convention.BindableProperty;
#else
            return element.ContentTemplate == null && element.ContentTemplateSelector == null && !(element.Content is DependencyObject)
                ? View.ModelProperty
                : convention.BindableProperty;
#endif
        };

        /// <summary>
        /// Determines whether an particular dependency property already had a binding on the provided element.
        /// </summary>
        public static Func<FrameworkElement, DependencyProperty, bool> HasBinding = (element, property) =>{
            return element.GetBindingExpression(property) != null;
        };

        /// <summary>
        /// Determines whether a custom update source trigger should be applied to the binding expression.
        /// </summary>
        public static Action<DependencyProperty, DependencyObject, Binding> ApplyUpdateSourceTrigger = (bindableProperty, foundControl, binding) =>{
#if SILVERLIGHT
            ApplySilverlightTriggers(foundControl, bindableProperty, x => x.GetBindingExpression(bindableProperty));
#else
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
#endif
        };

        /// <summary>
        /// Adds any additional conventional behavior to the element.
        /// </summary>
        public static Action<Binding, ElementConvention, Type, PropertyInfo, DependencyObject> AddCustomBindingBehavior = (binding, convention, viewModelType, property, foundControl) =>{
            var itemsControl = foundControl as ItemsControl;
            if(itemsControl == null)
                return;

            if (string.IsNullOrEmpty(itemsControl.DisplayMemberPath)
                && !HasBinding(itemsControl, ItemsControl.DisplayMemberPathProperty)
                && itemsControl.ItemTemplate == null 
                && property.PropertyType.IsGenericType)
            {
#if !WP7
                var itemType = property.PropertyType.GetGenericArguments().First();
                if(!itemType.IsValueType && !typeof(string).IsAssignableFrom(itemType))
#endif
                    itemsControl.ItemTemplate = DefaultDataTemplate;
            }

            var selector = itemsControl as Selector;
            if(selector == null)
                return;

            if(HasBinding(selector, Selector.SelectedItemProperty))
                return;

            foreach(var potentialName in DerivePotentialSelectionNames(property.Name))
            {
                if(viewModelType.GetProperty(potentialName) != null)
                {
                    var selectionBinding = new Binding(potentialName) { Mode = BindingMode.TwoWay };
                    BindingOperations.SetBinding(foundControl, Selector.SelectedItemProperty, selectionBinding);
                    return;
                }
            }
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
            AddElementConvention<TabControl>(TabControl.ItemsSourceProperty, "ItemsSource", "SelectionChanged");
            AddElementConvention<TabItem>(TabItem.ContentProperty, "DataContext", "DataContextChanged");
            AddElementConvention<Window>(Window.DataContextProperty, "DataContext", "Loaded");
#endif
            AddElementConvention<UserControl>(UserControl.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Selector>(Selector.ItemsSourceProperty, "SelectedItem", "SelectionChanged");
            AddElementConvention<ItemsControl>(ItemsControl.ItemsSourceProperty, "DataContext", "Loaded");
            AddElementConvention<ContentControl>(ContentControl.ContentProperty, "DataContext", "Loaded");
            AddElementConvention<FrameworkElement>(FrameworkElement.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Image>(Image.SourceProperty, "Source", "Loaded");
            AddElementConvention<ButtonBase>(ButtonBase.ContentProperty, "DataContext", "Click");
            AddElementConvention<ToggleButton>(ToggleButton.IsCheckedProperty, "IsChecked", "Click");
            AddElementConvention<TextBox>(TextBox.TextProperty, "Text", "TextChanged");
            AddElementConvention<TextBlock>(TextBlock.TextProperty, "Text", "DataContextChanged");
        }

        /// <summary>
        /// Adds an element convention.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="bindableProperty">The default property for binding conventions.</param>
        /// <param name="parameterProperty">The default property for action parameters.</param>
        /// <param name="eventName">The default event to trigger actions.</param>
        public static void AddElementConvention<T>(DependencyProperty bindableProperty, string parameterProperty, string eventName)
        {
            AddElementConvention(new ElementConvention {
                ElementType = typeof(T),
                BindableProperty = bindableProperty,
                ParameterProperty = parameterProperty,
                CreateTrigger = () => new System.Windows.Interactivity.EventTrigger { EventName = eventName }
            });
        }

        /// <summary>
        /// Adds an element convention.
        /// </summary>
        /// <param name="convention"></param>
        public static void AddElementConvention(ElementConvention convention)
        {
            ElementConventions[convention.ElementType] = convention;
        }

        /// <summary>
        /// Gets an element convention for the provided element type.
        /// </summary>
        /// <param name="elementType">The type of element to locate the convention for.</param>
        /// <returns>The convention if found, null otherwise.</returns>
        /// <remarks>Searches the clas hierarchy for conventions.</remarks>
        public static ElementConvention GetElementConvention(Type elementType)
        {
            if (elementType == null)
                return null;

            ElementConvention propertyConvention;
            ElementConventions.TryGetValue(elementType, out propertyConvention);
            return propertyConvention ?? GetElementConvention(elementType.BaseType);
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