namespace Caliburn.Micro.Xamarin.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xamarin.Forms;

    /// <summary>
    /// Used to configure the conventions used by the framework to apply bindings and create actions.
    /// </summary>
    public static class ConventionManager
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ConventionManager));

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

        private static readonly Func<object> CreateDefaultItemTemplate = () => {

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            content.SetBinding(View.ModelProperty, new Binding());

            var cell = new ViewCell
            {
                View = content
            };

            return cell;
        };

        /// <summary>
        /// The default DataTemplate used for ItemsControls when required.
        /// </summary>
        public static DataTemplate DefaultItemTemplate = new DataTemplate(CreateDefaultItemTemplate);

        private static readonly Func<object> CreateDefaultHeaderTemplate = () =>
        {
            var content = new Label();

            content.SetBinding(Label.TextProperty, new Binding("DisplayName", BindingMode.TwoWay));

            return content;
        };
        
        
        /// <summary>
        /// The default DataTemplate used for Headered controls when required.
        /// </summary>
        public static DataTemplate DefaultHeaderTemplate =new DataTemplate(CreateDefaultHeaderTemplate);

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
        public static Action<Type, string, PropertyInfo, VisualElement, ElementConvention, BindableProperty> SetBinding =
            (viewModelType, path, property, element, convention, bindableProperty) =>
            {
                var binding = new Binding(path);

                ApplyBindingMode(binding, property);
                ApplyValueConverter(binding, bindableProperty, property);
                ApplyStringFormat(binding, convention, property);
                ApplyValidation(binding, viewModelType, property);
                ApplyUpdateSourceTrigger(bindableProperty, element, binding, property);

                element.SetBinding(bindableProperty, binding);
            };

        /// <summary>
        /// Applies the appropriate binding mode to the binding.
        /// </summary>
        public static Action<Binding, PropertyInfo> ApplyBindingMode = (binding, property) =>
        {
            var setMethod = property.SetMethod;
            binding.Mode = (property.CanWrite && setMethod != null && setMethod.IsPublic) ? BindingMode.TwoWay : BindingMode.OneWay;
        };

        /// <summary>
        /// Determines whether or not and what type of validation to enable on the binding.
        /// </summary>
        public static Action<Binding, Type, PropertyInfo> ApplyValidation = (binding, viewModelType, property) =>
        {

        };

        /// <summary>
        /// Determines whether a value converter is is needed and applies one to the binding.
        /// </summary>
        public static Action<Binding, BindableProperty, PropertyInfo> ApplyValueConverter = (binding, bindableProperty, property) =>
        {
            
        };

        /// <summary>
        /// Determines whether a custom string format is needed and applies it to the binding.
        /// </summary>
        public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (binding, convention, property) =>
        {
#if !WinRT
            if (typeof(DateTime).IsAssignableFrom(property.PropertyType))
                binding.StringFormat = "{0:d}";
#endif
        };

        /// <summary>
        /// Determines whether a custom update source trigger should be applied to the binding.
        /// </summary>
        public static Action<BindableProperty, BindableObject, Binding, PropertyInfo> ApplyUpdateSourceTrigger = (bindableProperty, element, binding, info) =>
        {

        };

        static ConventionManager()
        {
            AddElementConvention<ImageCell>(ImageCell.ImageSourceProperty, "ImageSource", "Tapped");
            AddElementConvention<TextCell>(TextCell.TextProperty, "Text", "Tapped");
            AddElementConvention<SwitchCell>(SwitchCell.OnProperty, "On", "OnChanged");
            AddElementConvention<EntryCell>(EntryCell.TextProperty, "Text", "Completed");
            AddElementConvention<Cell>(Cell.IsEnabledProperty, "IsEnabled", "Tapped");
            AddElementConvention<WebView>(WebView.SourceProperty, "Source", "Navigated");
            AddElementConvention<TimePicker>(TimePicker.TimeProperty, "Time", "TimeSelected");
            AddElementConvention<Switch>(Switch.IsToggledProperty, "IsToggled", "Toggled");
            AddElementConvention<Stepper>(Stepper.ValueProperty, "Value", "ValueChanged");
            AddElementConvention<Slider>(Slider.ValueProperty, "Value", "ValueChanged");
            AddElementConvention<SearchBar>(SearchBar.TextProperty, "Text", "SearchButtonPressed");
            AddElementConvention<ProgressBar>(ProgressBar.ProgressProperty, "Progress", "Focused");
            AddElementConvention<Picker>(Picker.SelectedIndexProperty, "SelectedIndex", "SelectedIndexChanged");
            AddElementConvention<ListView>(ListView.ItemsSourceProperty, "SelectedItem", "ItemSelected")
               .ApplyBinding = (viewModelType, path, property, element, convention) =>
               {
                   if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsView<Cell>.ItemsSourceProperty))
                   {
                       return false;
                   }

                   ConfigureSelectedItem(element, ListView.SelectedItemProperty, viewModelType, path);
                   ApplyItemTemplate((ItemsView<Cell>)element, property);

                   return true;
               };
            AddElementConvention<Label>(Label.TextProperty, "Text", "Focused");
            AddElementConvention<Image>(Image.SourceProperty, "Source", "Focused");
            AddElementConvention<Entry>(Entry.TextProperty, "Text", "TextChanged");
            AddElementConvention<Editor>(Editor.TextProperty, "Text", "TextChanged");
            AddElementConvention<DatePicker>(DatePicker.DateProperty, "Date", "DateSelected");
            AddElementConvention<Button>(Button.TextProperty, "Text", "Clicked");
            AddElementConvention<ActivityIndicator>(ActivityIndicator.IsRunningProperty, "IsRunning", "Focused");
            AddElementConvention<ItemsView<Cell>>(ItemsView<Cell>.ItemsSourceProperty, "BindingContext", "Focused")
               .ApplyBinding = (viewModelType, path, property, element, convention) =>
               {
                   if (!SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsView<Cell>.ItemsSourceProperty))
                   {
                       return false;
                   }

                   ApplyItemTemplate((ItemsView<Cell>)element, property);

                   return true;
               };
            AddElementConvention<ContentView>(View.ModelProperty, "BindingContext", "Focused");
            AddElementConvention<VisualElement>(VisualElement.IsVisibleProperty, "BindingContext", "Focused");
        }

        /// <summary>
        /// Adds an element convention.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="bindableProperty">The default property for binding conventions.</param>
        /// <param name="parameterProperty">The default property for action parameters.</param>
        /// <param name="eventName">The default event to trigger actions.</param>
        public static ElementConvention AddElementConvention<T>(BindableProperty bindableProperty, string parameterProperty, string eventName)
        {
            return AddElementConvention(new ElementConvention
            {
                ElementType = typeof(T),
                GetBindableProperty = element => bindableProperty,
                ParameterProperty = parameterProperty,
                CreateTrigger = () => new EventTrigger { Event = eventName }
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

            return propertyConvention ?? GetElementConvention(elementType.GetTypeInfo().BaseType);

        }

        /// <summary>
        /// Determines whether a particular dependency property already has a binding on the provided element.
        /// </summary>
        public static bool HasBinding(VisualElement element, BindableProperty property) {
            return false; // Dman, can't be done
        }

        /// <summary>
        /// Creates a binding and sets it on the element, guarding against pre-existing bindings.
        /// </summary>
        public static bool SetBindingWithoutBindingOverwrite(Type viewModelType, string path, PropertyInfo property,
                                                             VisualElement element, ElementConvention convention,
                                                             BindableProperty bindableProperty)
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
        public static bool SetBindingWithoutBindingOrValueOverwrite(Type viewModelType, string path,
                                                                    PropertyInfo property, VisualElement element,
                                                                    ElementConvention convention,
                                                                    BindableProperty bindableProperty)
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
        public static void ApplyItemTemplate<TVisual>(ItemsView<TVisual> itemsControl, PropertyInfo property) where TVisual : BindableObject
        {
            if (property.PropertyType.GetTypeInfo().IsGenericType) {
                var itemType = property.PropertyType.GenericTypeArguments.First();
                if (itemType.GetTypeInfo().IsValueType || typeof (string).IsAssignableFrom(itemType)) {
                    return;
                }
            }


            itemsControl.ItemTemplate = DefaultItemTemplate;
            Log.Info("ItemTemplate applied to {0}.", itemsControl);
        }

        /// <summary>
        /// Configures the selected item convention.
        /// </summary>
        /// <param name="selector">The element that has a SelectedItem property.</param>
        /// <param name="selectedItemProperty">The SelectedItem property.</param>
        /// <param name="viewModelType">The view model type.</param>
        /// <param name="path">The property path.</param>
        public static Action<VisualElement, BindableProperty, Type, string> ConfigureSelectedItem =
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

                        var binding = new Binding(selectionPath) { Mode = BindingMode.TwoWay };

                        var shouldApplyBinding = ConfigureSelectedItemBinding(selector, selectedItemProperty, viewModelType, selectionPath, binding);
                        if (shouldApplyBinding)
                        {
                            selector.SetBinding(selectedItemProperty, binding);

                            Log.Info("SelectedItem binding applied to {0}.", selector);
                            return;
                        }

                        Log.Info("SelectedItem binding not applied to {0} due to 'ConfigureSelectedItemBinding' customization.", selector);
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
        public static Func<VisualElement, BindableProperty, Type, string, Binding, bool> ConfigureSelectedItemBinding =
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
        public static void ApplyHeaderTemplate(VisualElement element, BindableProperty headerTemplateProperty, BindableProperty headerTemplateSelectorProperty, Type viewModelType)
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
            Log.Info("Header template applied to {0}.", element);
        }

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

            if (typeInfo.IsInterface) {
                typeList.AddRange(typeInfo.ImplementedInterfaces);
            }

            return typeList
                .Select(interfaceType => interfaceType.GetRuntimeProperty(propertyName))
                .FirstOrDefault(property => property != null);
        }


    }
}
