namespace Caliburn.Micro
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
#if WinRT
    using Windows.UI.Xaml;
    using Windows.UI.Interactivity;
#else
    using System.Windows;
    using System.Windows.Interactivity;
#endif

#if WP71
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
        /// <remarks>Parameters include named Elements to search through and the type of view model to determine conventions for. Returns unmatched elements.</remarks>
        public static Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> BindProperties = (namedElements, viewModelType) =>
        {
            var unmatchedElements = new List<FrameworkElement>();

            foreach (var element in namedElements)
            {
                var cleanName = element.Name.Trim('_');
                var parts = cleanName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                var property = viewModelType.GetPropertyCaseInsensitive(parts[0]);
                var interpretedViewModelType = viewModelType;

                for (int i = 1; i < parts.Length && property != null; i++)
                {
                    interpretedViewModelType = property.PropertyType;
                    property = interpretedViewModelType.GetPropertyCaseInsensitive(parts[i]);
                }

                if (property == null)
                {
                    unmatchedElements.Add(element);
                    Log.Info("Binding Convention Not Applied: Element {0} did not match a property.", element.Name);
                    continue;
                }

                var convention = ConventionManager.GetElementConvention(element.GetType());
                if (convention == null)
                {
                    unmatchedElements.Add(element);
                    Log.Warn("Binding Convention Not Applied: No conventions configured for {0}.", element.GetType());
                    continue;
                }

                var applied = convention.ApplyBinding(
                    interpretedViewModelType,
                    cleanName.Replace('_', '.'),
                    property,
                    element,
                    convention
                    );

                if (applied)
                {
                    Log.Info("Binding Convention Applied: Element {0}.", element.Name);
                }
                else
                {
                    Log.Info("Binding Convention Not Applied: Element {0} has existing binding.", element.Name);
                    unmatchedElements.Add(element);
                }
            }

            return unmatchedElements;
        };

        /// <summary>
        /// Attaches instances of <see cref="ActionMessage"/> to the view's controls based on the provided methods.
        /// </summary>
        /// <remarks>Parameters include the named elements to search through and the type of view model to determine conventions for. Returns unmatched elements.</remarks>
        public static Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> BindActions = (namedElements, viewModelType) =>
        {
#if WinRT
            var methods = viewModelType.GetRuntimeMethods();
#else
            var methods = viewModelType.GetMethods();
#endif
            var unmatchedElements = namedElements.ToList();

            foreach (var method in methods)
            {
                var foundControl = unmatchedElements.FindName(method.Name);
                if (foundControl == null)
                {
                    Log.Info("Action Convention Not Applied: No actionable element for {0}.", method.Name);
                    continue;
                }

                unmatchedElements.Remove(foundControl);

                var triggers = Interaction.GetTriggers(foundControl);
                if (triggers != null && triggers.Count > 0)
                {
                    Log.Info("Action Convention Not Applied: Interaction.Triggers already set on {0}.", foundControl.Name);
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

                        if (MessageBinder.SpecialValues.ContainsKey(specialValue))
                            paramName = specialValue;

                        message += paramName + ",";
                    }

                    message = message.Remove(message.Length - 1, 1);
                    message += ")";
                }

                Log.Info("Action Convention Applied: Action {0} on element {1}.", method.Name, message);
                Message.SetAttach(foundControl, message);
            }

            return unmatchedElements;
        };

        /// <summary>
        /// Allows the developer to add custom handling of named elements which were not matched by any default conventions.
        /// </summary>
        public static Action<IEnumerable<FrameworkElement>, Type> HandleUnmatchedElements = (elements, viewModelType) => { };

        /// <summary>
        /// Binds the specified viewModel to the view.
        /// </summary>
        ///<remarks>Passes the the view model, view and creation context (or null for default) to use in applying binding.</remarks>
        public static Action<object, DependencyObject, object> Bind = (viewModel, view, context) =>
        {
            Log.Info("Binding {0} and {1}.", view, viewModel);

            if ((bool)view.GetValue(Micro.Bind.NoContextProperty))
            {
                Action.SetTargetWithoutContext(view, viewModel);
            }
            else
            {
                Action.SetTarget(view, viewModel);
            }

            var viewAware = viewModel as IViewAware;
            if (viewAware != null)
            {
                Log.Info("Attaching {0} to {1}.", view, viewAware);
                viewAware.AttachView(view, context);
            }

            if ((bool)view.GetValue(ConventionsAppliedProperty))
            {
                return;
            }

            var element = View.GetFirstNonGeneratedView(view) as FrameworkElement;
            if (element == null)
            {
                return;
            }

            if (!ShouldApplyConventions(element))
            {
                Log.Info("Skipping conventions for {0} and {1}.", element, viewModel);
                return;
            }

            var viewModelType = viewModel.GetType();
            var namedElements = BindingScope.GetNamedElements(element);

#if SILVERLIGHT
            namedElements.Apply(x => x.SetValue(
                View.IsLoadedProperty,
                element.GetValue(View.IsLoadedProperty))
                );
#endif
            namedElements = BindActions(namedElements, viewModelType);
            namedElements = BindProperties(namedElements, viewModelType);
            HandleUnmatchedElements(namedElements, viewModelType);
#if WP71
            BindAppBar(view);
#endif

            view.SetValue(ConventionsAppliedProperty, true);
        };

#if WP71
        static void BindAppBar(DependencyObject view) {
            var page = view as PhoneApplicationPage;
            if (page == null || page.ApplicationBar == null) {
                return;
            }

            var triggers = Interaction.GetTriggers(view);

            foreach(var item in page.ApplicationBar.Buttons) {
                var button = item as AppBarButton;
                if (button == null) {
                    continue;
                }

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
                if (menuItem == null) {
					continue;
                }

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