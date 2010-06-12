namespace Caliburn.Micro
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    public static class ViewModelBinder
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ViewModelBinder));
        public static bool ApplyConventionsByDefault { get; set; }

        static ViewModelBinder()
        {
            ApplyConventionsByDefault = true;
        }

        public static void Bind(object viewModel, DependencyObject view, object context = null)
        {
            Log.Info("Binding {0} to {1}.", view, viewModel);
            Action.SetTarget(view, viewModel);

            var viewAware = viewModel as IViewAware;
            if (viewAware != null)
            {
                Log.Info("Attaching view {0} to {1}.", view, viewAware);
                viewAware.AttachView(view, context);
            }

#if WP7
            var element = view as FrameworkElement;
#else
            var element = WindowManager.GetSignificantView(view) as FrameworkElement;
#endif
            if (element == null)
                return;

            if (!ShouldApplyConventions(element))
            {
                Log.Info("Skipping conventions for {0} and {1}.", element, viewModel);
                return;
            }

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
                if (foundControl == null)
                {
                    Log.Info("No bindable control located for property {0}.", property.Name);
                    continue;
                }

                var convention = ConventionManager.GetElementConvention(foundControl.GetType());
                if (convention == null)
                {
                    Log.Warn("No conventions located for type {0}.", foundControl.GetType());
                    continue;
                }

                if (ConventionManager.HasBinding((FrameworkElement)foundControl, convention.BindableProperty))
                {
                    Log.Warn("Binding already exists on {0}.", property.Name);
                    continue;
                }

                var binding = new Binding(property.Name);

                ConventionManager.ApplyBindingMode(binding, convention, property);
                ConventionManager.ApplyValueConverter(binding, convention, property);
                ConventionManager.ApplyStringFormat(binding, convention, property);
                ConventionManager.ApplyValidation(binding, convention, property);

                var bindableProperty = ConventionManager.CheckBindablePropertyExceptions(convention, foundControl);
                ConventionManager.ApplyUpdateSourceTrigger(bindableProperty, foundControl, binding);

                BindingOperations.SetBinding(foundControl, bindableProperty, binding);
                Log.Info("Added convention binding for property {0}.", property.Name);
                ConventionManager.AddCustomBindingBehavior(convention, property, foundControl, binding);
            }
        }

        static void BindActions(FrameworkElement view, IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                var found = view.FindName(method.Name) as DependencyObject;
                if (found == null)
                {
                    Log.Info("No bindable control located for action {0}.", method.Name);
                    continue;
                }

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

                Log.Info("Added convention action for method {0} as {1}.", method.Name, message);
                Message.SetAttach(found, message);
            }
        }
    }
}