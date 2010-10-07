namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Interactivity;
#if WP7
    using Microsoft.Phone.Controls;
#endif

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
            foreach(var element in namedElements) {
                var cleanName = element.Name.Trim('_');
                var parts = cleanName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                var property = viewModelType.GetPropertyCaseInsensitive(parts[0]);

                for (int i = 1; i < parts.Length && property != null; i++) {
                    property = property.PropertyType.GetPropertyCaseInsensitive(parts[i]);
                }

                if (property == null) {
                    Log.Info("No convention applied to {0}.", element.Name);
                    continue;
                }

                var convention = ConventionManager.GetElementConvention(element.GetType());
                if (convention == null) {
                    Log.Warn("No conventions configured for {0}.", element.GetType());
                    continue;
                }

                convention.ApplyBinding(
                    parts.Length == 1 ? viewModelType : property.PropertyType,
                    cleanName.Replace('_', '.'),
                    property,
                    element,
                    convention
                    );
                
                Log.Info("Added convention binding for {0}.", element.Name);
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
            Log.Info("Binding {0} and {1}.", view, viewModel);
            Action.SetTarget(view, viewModel);

            var viewAware = viewModel as IViewAware;
            if(viewAware != null)
            {
                Log.Info("Attaching {0} to {1}.", view, viewAware);
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
                Log.Info("Skipping conventions {0} and {1}.", element, viewModel);
                return;
            }

            var viewModelType = viewModel.GetType();
            var namedElements = ExtensionMethods.GetNamedElementsInScope(element);
            var isLoaded = element.GetValue(View.IsLoadedProperty);

            namedElements.Apply(x => x.SetValue(View.IsLoadedProperty, isLoaded));

            BindActions(namedElements, viewModelType);
            BindProperties(namedElements, viewModelType);
#if WP7
            BindAppBar(view);
#endif

            view.SetValue(ConventionsAppliedProperty, true);
        };

#if WP7
        static void BindAppBar(DependencyObject view) {
            var page = view as PhoneApplicationPage;
            if (page == null || page.ApplicationBar == null)
                return;

            var triggers = Interaction.GetTriggers(view);

            foreach(var item in page.ApplicationBar.Buttons) {
                var button = item as AppBarButton;
                if (button == null)
                    return;

                var parsedTrigger = Parser.Parse(view, button.Message).First();
                var trigger = new AppBarButtonTrigger(button);
                var actionMessages = parsedTrigger.Actions.OfType<ActionMessage>().ToList();
                actionMessages.Apply(x => {
                    x.buttonSource = button;
                    parsedTrigger.Actions.Remove(x);
                    trigger.Actions.Add(x);
                });
                
                triggers.Add(trigger);
            }

            foreach (var item in page.ApplicationBar.MenuItems) {
                var menuItem = item as AppBarMenuItem;
                if (menuItem == null)
                    return;

                var parsedTrigger = Parser.Parse(view, menuItem.Message).First();
                var trigger = new AppBarMenuItemTrigger(menuItem);
                var actionMessages = parsedTrigger.Actions.OfType<ActionMessage>().ToList();
                actionMessages.Apply(x => {
                    x.menuItemSource = menuItem;
                    parsedTrigger.Actions.Remove(x);
                    trigger.Actions.Add(x);
                });

                triggers.Add(trigger);
            }
        }
#endif
    }
}