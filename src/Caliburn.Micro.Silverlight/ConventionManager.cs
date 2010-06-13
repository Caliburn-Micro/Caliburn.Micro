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

    public static class ConventionManager
    {
        static readonly BooleanToVisibilityConverter BooleanToVisibilityConverter = new BooleanToVisibilityConverter();

#if SILVERLIGHT
        public static DataTemplate DefaultDataTemplate = (DataTemplate)XamlReader.Load(
#else
        public static DataTemplate DefaultDataTemplate = (DataTemplate)XamlReader.Parse(
#endif
            "<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
                           "xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' " +
                           "xmlns:cal='http://www.caliburnproject.org'> " +
                "<ContentControl cal:View.Model=\"{Binding}\" />" +
            "</DataTemplate>"
            );

        static readonly Dictionary<Type, ElementConvention> ElementConventions = new Dictionary<Type, ElementConvention>();

        public static Func<string, string> Singularize = original =>{
            return original.TrimEnd('s');
        };

        public static Action<Binding, ElementConvention, PropertyInfo> ApplyBindingMode = (binding, convention, property) =>{
            var setMethod = property.GetSetMethod();
            binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
        };

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

        public static Action<Binding, ElementConvention, PropertyInfo> ApplyValueConverter = (binding, convention, property) =>{
            if(convention.BindableProperty == UIElement.VisibilityProperty && typeof(bool).IsAssignableFrom(property.PropertyType))
                binding.Converter = BooleanToVisibilityConverter;
        };

        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (binding, convention, property) =>{
#if !WP7
            if(typeof(DateTime).IsAssignableFrom(property.PropertyType))
                binding.StringFormat = "{0:MM/dd/yyyy}";
#endif
        };

        public static Func<ElementConvention, DependencyObject, DependencyProperty> CheckBindablePropertyExceptions = (convention, foundControl) =>{
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

        public static Func<FrameworkElement, DependencyProperty, bool> HasBinding = (element, property) =>{
            return element.GetBindingExpression(property) != null;
        };

        public static Action<DependencyProperty, DependencyObject, Binding> ApplyUpdateSourceTrigger = (bindableProperty, foundControl, binding) =>{
#if !SILVERLIGHT
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            return;
#else
            var textBox = foundControl as TextBox;
            if (textBox != null && bindableProperty == TextBox.TextProperty)
            {
                textBox.TextChanged += delegate { textBox.GetBindingExpression(bindableProperty).UpdateSource(); };
                return;
            }

            var passwordBox = foundControl as PasswordBox;
            if (passwordBox != null && bindableProperty == PasswordBox.PasswordProperty)
            {
                passwordBox.PasswordChanged += delegate { passwordBox.GetBindingExpression(bindableProperty).UpdateSource(); };
                return;
            }
#endif
        };

        public static Action<ElementConvention, PropertyInfo, DependencyObject, Binding> AddCustomBindingBehavior = (convention, property, foundControl, binding) =>{
            var itemsControl = foundControl as ItemsControl;
            if(itemsControl == null)
                return;

            if (string.IsNullOrEmpty(itemsControl.DisplayMemberPath) && itemsControl.ItemTemplate == null && property.PropertyType.IsGenericType)
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

            var potentialNames = new[] {
                "Active" + Singularize(property.Name),
                "Selected" + Singularize(property.Name)
            };

            foreach(var potentialName in potentialNames)
            {
                if(property.DeclaringType.GetProperty(potentialName) != null)
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
            AddElementConvention<DockPanel>(DockPanel.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<UniformGrid>(UniformGrid.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<WrapPanel>(WrapPanel.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Viewbox>(Viewbox.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<BulletDecorator>(BulletDecorator.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Slider>(Slider.ValueProperty, "Value", "ValueChanged");
            AddElementConvention<Expander>(Expander.IsExpandedProperty, "IsExpanded", "Expanded");
            AddElementConvention<Window>(Window.DataContextProperty, "DataContext", "Loaded");
            AddElementConvention<StatusBar>(StatusBar.ItemsSourceProperty, "DataContext", "Loaded");
            AddElementConvention<ToolBar>(ToolBar.ItemsSourceProperty, "DataContext", "Loaded");
            AddElementConvention<ToolBarTray>(ToolBarTray.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<TreeView>(TreeView.ItemsSourceProperty, "SelectedItem", "SelectedItemChanged");
            AddElementConvention<TabControl>(TabControl.ItemsSourceProperty, "ItemsSource", "SelectionChanged");
            AddElementConvention<TabItem>(TabItem.ContentProperty, "DataContext", "DataContextChanged");
            AddElementConvention<ListView>(ListView.ItemsSourceProperty, "SelectedItem", "SelectionChanged");
#endif
            AddElementConvention<ListBox>(ListBox.ItemsSourceProperty, "SelectedItem", "SelectionChanged");
            AddElementConvention<UserControl>(UserControl.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<ComboBox>(ComboBox.ItemsSourceProperty, "SelectedItem", "SelectionChanged");
            AddElementConvention<Image>(Image.SourceProperty, "Source", "Loaded");
            AddElementConvention<ButtonBase>(ButtonBase.ContentProperty, "DataContext", "Click");
            AddElementConvention<Button>(Button.ContentProperty, "DataContext", "Click");
            AddElementConvention<ToggleButton>(ToggleButton.IsCheckedProperty, "IsChecked", "Click");
            AddElementConvention<RadioButton>(RadioButton.IsCheckedProperty, "IsChecked", "Click");
            AddElementConvention<CheckBox>(CheckBox.IsCheckedProperty, "IsChecked", "Click");
            AddElementConvention<TextBox>(TextBox.TextProperty, "Text", "TextChanged");
            AddElementConvention<TextBlock>(TextBlock.TextProperty, "Text", "DataContextChanged");
            AddElementConvention<StackPanel>(StackPanel.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Grid>(Grid.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<Border>(Border.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<ItemsControl>(ItemsControl.ItemsSourceProperty, "DataContext", "Loaded");
            AddElementConvention<ContentControl>(ContentControl.ContentProperty, "DataContext", "Loaded"); 
        }

        public static void AddElementConvention<T>(DependencyProperty bindableProperty, string parameterProperty, string eventName)
        {
            AddElementConvention(new ElementConvention {
                ElementType = typeof(T),
                BindableProperty = bindableProperty,
                ParameterProperty = parameterProperty,
                CreateTrigger = () => new System.Windows.Interactivity.EventTrigger { EventName = eventName }
            });
        }

        public static void AddElementConvention(ElementConvention convention)
        {
            ElementConventions[convention.ElementType] = convention;
        }

        public static ElementConvention GetElementConvention(Type elementType)
        {
            if (elementType == null)
                return null;

            ElementConvention propertyConvention;
            ElementConventions.TryGetValue(elementType, out propertyConvention);
            return propertyConvention ?? GetElementConvention(elementType.BaseType);
        }
    }
}