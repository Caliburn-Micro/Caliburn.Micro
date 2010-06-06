namespace Caliburn.Micro
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public static class ViewModelBinder
    {
        public static bool ApplyConventionsByDefault { get; set; }

        static ViewModelBinder()
        {
            ApplyConventionsByDefault = true;
        }

        public static void Bind(object viewModel, DependencyObject view, object context = null)
        {
            Action.SetTarget(view, viewModel);

            var viewAware = viewModel as IViewAware;
            if (viewAware != null)
                viewAware.AttachView(view, context);

            var element = WindowManager.GetSignificantView(view) as FrameworkElement;
            if(element == null)
                return;

            if (!ShouldApplyConventions(element))
                return;

            var viewType = viewModel.GetType();
            var properties = viewType.GetProperties();
            var methods = viewType.GetMethods();

            BindActions(element, methods);
            BindProperties(element, properties);
        }

        static bool ShouldApplyConventions(FrameworkElement view)
        {
            var overriden = View.GetApplyConventions(view);
            return overriden.GetValueOrDefault(ApplyConventionsByDefault);
        }

        static void BindProperties(FrameworkElement view, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var foundControl = view.FindName(property.Name) as DependencyObject;
                if(foundControl == null)
                    continue;

                var convention = ConventionManager.GetElementConvention(foundControl.GetType());
                if (convention == null)
                    continue;

                if (((FrameworkElement)foundControl).GetBindingExpression(convention.BindableProperty) != null)
                    continue;

                var binding = new Binding(property.Name) { Mode = ConventionManager.DetermineBindingMode(property) };
                ConventionManager.ApplyValueConverter(binding, convention, property);
                ConventionManager.ApplyStringFormat(binding, convention, property);
                ConventionManager.ApplyValidation(binding, property);

                BindingOperations.SetBinding(foundControl, convention.BindableProperty, binding);

                var textBox = foundControl as TextBox;
                if (textBox != null && convention.BindableProperty == TextBox.TextProperty)
                {
                    textBox.TextChanged += delegate { textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource(); };
                    continue;
                }

                var itemsControl = foundControl as ItemsControl;
                if(itemsControl != null)
                {
                    if (string.IsNullOrEmpty(itemsControl.DisplayMemberPath) && itemsControl.ItemTemplate == null)
                        itemsControl.ItemTemplate = ConventionManager.DefaultDataTemplate;

                    var selector = itemsControl as Selector;
                    if(selector != null)
                    {
                        var selectionBinding = new Binding("Active" + ConventionManager.Singularize(property.Name)) {
                            Mode = BindingMode.TwoWay
                        };

                        BindingOperations.SetBinding(foundControl, Selector.SelectedItemProperty, selectionBinding);
                    }
                }
            }
        }

        static void BindActions(FrameworkElement view, IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                var found = view.FindName(method.Name) as DependencyObject;
                if (found == null)
                    continue;

                var message = method.Name;
                var parameters = method.GetParameters();

                if (parameters.Length > 0)
                {
                    message += "(";

                    foreach (var parameter in parameters)
                    {
                        var paramName = parameter.Name;
                        var specialValue = "$" + paramName.ToLower();

                        if (MessageBinder.SpecialValues.Contains(specialValue))
                            paramName = specialValue;

                        message += paramName + ",";
                    }

                    message = message.Remove(message.Length - 1, 1);
                    message += ")";
                }

                Message.SetAttach(found, message);
            }
        }
    }
}