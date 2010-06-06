namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Markup;

    public static class ConventionManager
    {
        static readonly BooleanToVisibilityConverter BooleanToVisibilityConverter = new BooleanToVisibilityConverter();

        public static DataTemplate DefaultDataTemplate = (DataTemplate)XamlReader.Load(
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

        public static Action<Binding, PropertyInfo> ApplyValidation = (binding, property) =>{
#if SILVERLIGHT
            if(typeof(INotifyDataErrorInfo).IsAssignableFrom(property.DeclaringType))
                binding.ValidatesOnNotifyDataErrors = true;
            else 
#endif
            if(typeof(IDataErrorInfo).IsAssignableFrom(property.DeclaringType))
                binding.ValidatesOnDataErrors = true;
        };

        public static Action<Binding, ElementConvention, PropertyInfo> ApplyValueConverter = (binding, convention, property) =>{
            if(convention.BindableProperty == UIElement.VisibilityProperty && typeof(bool).IsAssignableFrom(property.PropertyType))
                binding.Converter = BooleanToVisibilityConverter;
        };

        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (binding, convention, property) =>{
            if (typeof(DateTime).IsAssignableFrom(property.PropertyType))
                binding.Source = "{0:MM/dd/yyyy}";
        };

        static ConventionManager()
        {
            AddElementConvention<HyperlinkButton>(HyperlinkButton.ContentProperty, "DataContext", "Click");
            AddElementConvention<UserControl>(UserControl.VisibilityProperty, "DataContext", "Loaded");
            AddElementConvention<ListBox>(ListBox.ItemsSourceProperty, "SelectedItem", "SelectionChanged");
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
            AddElementConvention<ContentControl>(View.ModelProperty, "DataContext", "Loaded"); 
        }

        public static void AddElementConvention<T>(DependencyProperty bindableProperty, string parameterProperty, string eventName)
        {
            AddElementConvention(new ElementConvention(typeof(T), bindableProperty, parameterProperty, () => new System.Windows.Interactivity.EventTrigger{ EventName = eventName }));
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

        public static BindingMode DetermineBindingMode(PropertyInfo property)
        {
            var setMethod = property.GetSetMethod();
            var canWriteToProperty = property.CanWrite && setMethod != null && setMethod.IsPublic;

            return canWriteToProperty ? BindingMode.TwoWay : BindingMode.OneWay;
        }
    }
}