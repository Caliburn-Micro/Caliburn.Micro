namespace Caliburn.Micro
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Binds a view to a view model.
    /// </summary>
    public static class ViewModelBinder
    {
        /// <summary>
        /// Gets or sets a value indicating whether to apply conventions by default.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if conventions should be applied by default; otherwise, <c>false</c>.
        /// </value>
        public static bool ApplyConventionsByDefault = true;
        static readonly ILog Log = LogManager.GetLog(typeof(ViewModelBinder));

        /// <summary>
        /// Binds the specified viewModel to the view.
        /// </summary>
        ///<remarks>Passes the the view model, view and creation context (or null for default) to use in applying binding.</remarks>
        public static System.Action<object, DependencyObject, object> Bind = (viewModel, view, context) =>{
            Log.Info("Binding {0}+{1}.", view, viewModel);
            Action.SetTarget(view, viewModel);

            var viewAware = viewModel as IViewAware;
            if(viewAware != null)
            {
                Log.Info("Attaching {0}+{1}.", view, viewAware);
                viewAware.AttachView(view, context);
            }

#if WP7
            var element = view as FrameworkElement;
#else
            var element = WindowManager.GetSignificantView(view) as FrameworkElement;
#endif
            if(element == null)
                return;

            if(!ShouldApplyConventions(element))
            {
                Log.Info("Skipping conventions {0}+{1}.", element, viewModel);
                return;
            }

            var viewType = viewModel.GetType();
            var properties = viewType.GetProperties();
            var methods = viewType.GetMethods();

            BindActions(element, methods);
            BindProperties(element, properties);
        };

        /// <summary>
        /// Determines whether a view should have conventions applied to it.
        /// </summary>
        /// <param name="view">The view to check.</param>
        /// <returns>Whether or not conventions should be applied to the view.</returns>
        public static bool ShouldApplyConventions(FrameworkElement view)
        {
            var overriden = View.GetApplyConventions(view);
            return overriden.GetValueOrDefault(ApplyConventionsByDefault);
        }

        /// <summary>
        /// Creates data bindings on the view's controls based on the provided properties.
        /// </summary>
        /// <param name="view">The view to search for bindable controls.</param>
        /// <param name="properties">The properties to create bindings for.</param>
        public static void BindProperties(FrameworkElement view, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var foundControl = view.FindName(property.Name) as DependencyObject;
                if (foundControl == null)
                {
                    Log.Info("No bindable control for property {0}.", property.Name);
                    continue;
                }

                var convention = ConventionManager.GetElementConvention(foundControl.GetType());
                if (convention == null)
                {
                    Log.Warn("No conventions for {0}.", foundControl.GetType());
                    continue;
                }

                var bindableProperty = ConventionManager.CheckBindablePropertyExceptions(convention, foundControl);
                if (ConventionManager.HasBinding((FrameworkElement)foundControl, bindableProperty))
                {
                    Log.Warn("Binding exists on {0}.", property.Name);
                    continue;
                }

                var binding = new Binding(property.Name);

                ConventionManager.ApplyBindingMode(binding, convention, property);
                ConventionManager.ApplyValueConverter(binding, convention, property);
                ConventionManager.ApplyStringFormat(binding, convention, property);
                ConventionManager.ApplyValidation(binding, convention, property);
                ConventionManager.ApplyUpdateSourceTrigger(bindableProperty, foundControl, binding);

                BindingOperations.SetBinding(foundControl, bindableProperty, binding);
                Log.Info("Added convention binding for {0}.", property.Name);
                ConventionManager.AddCustomBindingBehavior(convention, property, foundControl, binding);
            }
        }

        /// <summary>
        /// Attaches instances of <see cref="ActionMessage"/> to the view's controls based on the provided methods.
        /// </summary>
        /// <param name="view">The view to search for actionable controls.</param>
        /// <param name="methods">The methods to create instances of <see cref="ActionMessage"/> for.</param>
        public static void BindActions(FrameworkElement view, IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                var found = view.FindName(method.Name) as DependencyObject;
                if (found == null)
                {
                    Log.Info("No bindable control for action {0}.", method.Name);
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

                Log.Info("Added convention action for {0} as {1}.", method.Name, message);
                Message.SetAttach(found, message);
            }
        }
    }
}