namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;

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
        /// Indicates whether or not the conventions have already been applied to the view.
        /// </summary>
        public static readonly DependencyProperty ConventionsAppliedProperty =
            DependencyProperty.RegisterAttached(
                "ConventionsApplied",
                typeof(bool),
                typeof(ViewModelBinder),
                null
                );

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
        /// <remarks>Parameters include named Elements to search through and the type of view model to determine conventions for.</remarks>
        public static Action<IEnumerable<FrameworkElement>, Type> BindProperties = (namedElements, viewModelType) =>{
            var properties = viewModelType.GetProperties();
            foreach(var property in properties)
            {
                var foundControl = namedElements.FindName(property.Name);
                if(foundControl == null)
                {
                    Log.Info("No bindable control for property {0}.", property.Name);
                    continue;
                }

                var convention = ConventionManager.GetElementConvention(foundControl.GetType());
                if(convention == null)
                {
                    Log.Warn("No conventions for {0}.", foundControl.GetType());
                    continue;
                }

                var bindableProperty = ConventionManager.EnsureDependencyProperty(convention, foundControl);
                if(ConventionManager.HasBinding(foundControl, bindableProperty))
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
                ConventionManager.AddCustomBindingBehavior(binding, convention, viewModelType, property, foundControl);
            }
        };

        /// <summary>
        /// Attaches instances of <see cref="ActionMessage"/> to the view's controls based on the provided methods.
        /// </summary>
        /// <remarks>Parameters include the named elements to search through and the type of view model to determine conventions for.</remarks>
        public static Action<IEnumerable<FrameworkElement>, Type> BindActions = (namedElements, viewModelType) =>{
            var methods = viewModelType.GetMethods();
            foreach(var method in methods)
            {
                var foundControl = namedElements.FindName(method.Name);
                if(foundControl == null)
                {
                    Log.Info("No bindable control for action {0}.", method.Name);
                    continue;
                }

                var triggers = Interaction.GetTriggers(foundControl);
                if (triggers != null && triggers.Count > 0) {
                    Log.Info("Interaction.Triggers already set on control {0}.", foundControl.Name);
                    continue;
                }

                var message = method.Name;
                var parameters = method.GetParameters();

                if(parameters.Length > 0)
                {
                    message += "(";

                    foreach(var parameter in parameters)
                    {
                        var paramName = parameter.Name;
                        var specialValue = "$" + paramName.ToLower();

                        if(MessageBinder.SpecialValues.Contains(specialValue))
                            paramName = specialValue;

                        message += paramName + ",";
                    }

                    message = message.Remove(message.Length - 1, 1);
                    message += ")";
                }

                Log.Info("Added convention action for {0} as {1}.", method.Name, message);
                Message.SetAttach(foundControl, message);
            }
        };

        /// <summary>
        /// Binds the specified viewModel to the view.
        /// </summary>
        ///<remarks>Passes the the view model, view and creation context (or null for default) to use in applying binding.</remarks>
        public static Action<object, DependencyObject, object> Bind = (viewModel, view, context) =>{
            Log.Info("Binding {0}+{1}.", view, viewModel);
            Action.SetTarget(view, viewModel);

            var viewAware = viewModel as IViewAware;
            if(viewAware != null)
            {
                Log.Info("Attaching {0}+{1}.", view, viewAware);
                viewAware.AttachView(view, context);
            }

            if ((bool)view.GetValue(ConventionsAppliedProperty))
                return;

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

            var viewModelType = viewModel.GetType();
            var namedElements = ExtensionMethods.GetNamedElementsInScope(element);
            var isLoaded = element.GetValue(View.IsLoadedProperty);

            namedElements.Apply(x => x.SetValue(View.IsLoadedProperty, isLoaded));

            BindActions(namedElements, viewModelType);
            BindProperties(namedElements, viewModelType);

            view.SetValue(ConventionsAppliedProperty, true);
        };
    }
}