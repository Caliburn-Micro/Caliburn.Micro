using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

#if XFORMS
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
using FrameworkElement = Xamarin.Forms.VisualElement;
using UIElement = Xamarin.Forms.Element;

#elif MAUI
using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;
using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
using UIElement = Microsoft.Maui.Controls.Element;

#elif WINDOWS_UWP
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;

#else
using System.Windows;

using Microsoft.Xaml.Behaviors;
#endif

#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    /// Binds a view to a view model.
    /// </summary>
    public static class ViewModelBinder {
        /// <summary>
        /// Indicates whether or not the conventions have already been applied to the view.
        /// </summary>
        public static readonly DependencyProperty ConventionsAppliedProperty =
            DependencyPropertyHelper.RegisterAttached(
                "ConventionsApplied",
                typeof(bool),
                typeof(ViewModelBinder),
                false);

#if !XFORMS && !MAUI
        private const string AsyncSuffix = "Async";
#endif

        private static readonly ILog Log = LogManager.GetLog(typeof(ViewModelBinder));

        /// <summary>
        /// Gets or sets a value indicating whether to apply conventions by default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if conventions should be applied by default; otherwise, <c>false</c>.
        /// </value>
        public static bool ApplyConventionsByDefault { get; set; }
            = true;

        /// <summary>
        /// Gets or sets func to attaches instances of <see cref="ActionMessage"/> to the view's controls based on the provided methods.
        /// </summary>
        /// <remarks>Parameters include the named elements to search through and the type of view model to determine conventions for. Returns unmatched elements.</remarks>
        public static Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> BindActions { get; set; }
            = (namedElements, viewModelType) => {
                var unmatchedElements = namedElements.ToList();
#if !XFORMS && !MAUI
#if WINDOWS_UWP
                IEnumerable<MethodInfo> methods = viewModelType.GetRuntimeMethods();
#else
                MethodInfo[] methods = viewModelType.GetMethods();
#endif
                foreach (MethodInfo method in methods) {
                    FrameworkElement foundControl = unmatchedElements.FindName(method.Name);
                    if (foundControl == null && IsAsyncMethod(method)) {
                        string methodNameWithoutAsyncSuffix = method.Name.Substring(0, method.Name.Length - AsyncSuffix.Length);
                        foundControl = unmatchedElements.FindName(methodNameWithoutAsyncSuffix);
                    }

                    if (foundControl == null) {
                        Log.Info("Action Convention Not Applied: No actionable element for {0}.", method.Name);
                        continue;
                    }

                    unmatchedElements.Remove(foundControl);

#if WINDOWS_UWP
                    BehaviorCollection triggers = Interaction.GetBehaviors(foundControl);
                    if (triggers != null && triggers.Count > 0) {
                        Log.Info("Action Convention Not Applied: Interaction.Triggers already set on {0}.", foundControl.Name);
                        continue;
                    }
#endif

                    string message = method.Name;
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length > 0) {
                        message += "(";

                        foreach (ParameterInfo parameter in parameters) {
                            string paramName = parameter.Name;
                            string specialValue = "$" + paramName.ToLower(CultureInfo.InvariantCulture);

                            if (MessageBinder.SpecialValues.ContainsKey(specialValue)) {
                                paramName = specialValue;
                            }

                            message += paramName + ",";
                        }

                        message = message.Remove(message.Length - 1, 1);
                        message += ")";
                    }

                    Log.Info("Action Convention Applied: Action {0} on element {1}.", method.Name, message);
                    Message.SetAttach(foundControl, message);
                }
#endif
                return unmatchedElements;
            };

        /// <summary>
        /// Gets or sets func to binds the specified viewModel to the view.
        /// </summary>
        /// <remarks>Passes the the view model, view and creation context (or null for default) to use in applying binding.</remarks>
        public static Action<object, DependencyObject, object> Bind { get; set; }
            = (viewModel, view, context) => {
#if !WINDOWS_UWP && !XFORMS && !MAUI
                // when using d:DesignInstance, Blend tries to assign the DesignInstanceExtension class as the DataContext,
                // so here we get the actual ViewModel which is in the Instance property of DesignInstanceExtension
                if (View.InDesignMode) {
                    Type vmType = viewModel.GetType();
                    if (vmType.FullName == "Microsoft.Expression.DesignModel.InstanceBuilders.DesignInstanceExtension") {
                        PropertyInfo propInfo = vmType.GetProperty("Instance", BindingFlags.Instance | BindingFlags.NonPublic);
                        viewModel = propInfo.GetValue(viewModel, null);
                    }
                }
#endif

                Log.Info("Binding {0} and {1}.", view, viewModel);

#if XFORMS
                DependencyProperty noContext = Caliburn.Micro.Xamarin.Forms.Bind.NoContextProperty;
#elif MAUI
                DependencyProperty noContext = Maui.Bind.NoContextProperty;
#else
                DependencyProperty noContext = Caliburn.Micro.Bind.NoContextProperty;
#endif

                if ((bool)view.GetValue(noContext)) {
                    Action.SetTargetWithoutContext(view, viewModel);
                } else {
                    Action.SetTarget(view, viewModel);
                }

                if (viewModel is IViewAware viewAware) {
                    Log.Info("Attaching {0} to {1}.", view, viewAware);
                    viewAware.AttachView(view, context);
                }

                if ((bool)view.GetValue(ConventionsAppliedProperty)) {
                    return;
                }

                if (!(View.GetFirstNonGeneratedView(view) is FrameworkElement element)) {
                    return;
                }

                if (!ShouldApplyConventions(element)) {
                    Log.Info("Skipping conventions for {0} and {1}.", element, viewModel);
                    return;
                }

                Type viewModelType = viewModel.GetType();
#if NET45
            var viewModelTypeProvider = viewModel as ICustomTypeProvider;
            if (viewModelTypeProvider != null) {
                viewModelType = viewModelTypeProvider.GetCustomType();
            }
#endif
#if XFORMS || MAUI
                IEnumerable<FrameworkElement> namedElements = new List<FrameworkElement>();
#else
                IEnumerable<FrameworkElement> namedElements = BindingScope.GetNamedElements(element);
#endif
                namedElements = BindActions(namedElements, viewModelType);
                namedElements = BindProperties(namedElements, viewModelType);
                HandleUnmatchedElements(namedElements, viewModelType);

                view.SetValue(ConventionsAppliedProperty, true);
            };

        /// <summary>
        /// Gets or sets func to creates data bindings on the view's controls based on the provided properties.
        /// </summary>
        /// <remarks>Parameters include named Elements to search through and the type of view model to determine conventions for. Returns unmatched elements.</remarks>
        public static Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> BindProperties { get; set; }
            = (namedElements, viewModelType) => {
                var unmatchedElements = new List<FrameworkElement>();
#if !XFORMS && !MAUI
                foreach (FrameworkElement element in namedElements) {
                    string cleanName = element.Name.Trim('_');
                    string[] parts = cleanName.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                    PropertyInfo property = viewModelType.GetPropertyCaseInsensitive(parts[0]);
                    Type interpretedViewModelType = viewModelType;

                    for (int i = 1; i < parts.Length && property != null; i++) {
                        interpretedViewModelType = property.PropertyType;
                        property = interpretedViewModelType.GetPropertyCaseInsensitive(parts[i]);
                    }

                    if (property == null) {
                        unmatchedElements.Add(element);
                        Log.Info("Binding Convention Not Applied: Element {0} did not match a property.", element.Name);
                        continue;
                    }

                    ElementConvention convention = ConventionManager.GetElementConvention(element.GetType());
                    if (convention == null) {
                        unmatchedElements.Add(element);
                        Log.Warn("Binding Convention Not Applied: No conventions configured for {0}.", element.GetType());
                        continue;
                    }

                    bool applied = convention.ApplyBinding(
                        interpretedViewModelType,
                        cleanName.Replace('_', '.'),
                        property,
                        element,
                        convention);

                    if (applied) {
                        Log.Info("Binding Convention Applied: Element {0}.", element.Name);
                    } else {
                        Log.Info("Binding Convention Not Applied: Element {0} has existing binding.", element.Name);
                        unmatchedElements.Add(element);
                    }
                }
#endif
                return unmatchedElements;
            };

        /// <summary>
        /// Gets or sets action that allows the developer to add custom handling of named elements which were not matched by any default conventions.
        /// </summary>
        public static Action<IEnumerable<FrameworkElement>, Type> HandleUnmatchedElements { get; set; }
            = (elements, viewModelType) => { };

        /// <summary>
        /// Determines whether a view should have conventions applied to it.
        /// </summary>
        /// <param name="view">The view to check.</param>
        /// <returns>Whether or not conventions should be applied to the view.</returns>
        public static bool ShouldApplyConventions(FrameworkElement view) {
            bool? overriden = View.GetApplyConventions(view);
            return overriden.GetValueOrDefault(ApplyConventionsByDefault);
        }

#if !XFORMS && !MAUI
        private static bool IsAsyncMethod(MethodInfo method)
            => typeof(Task).GetTypeInfo().IsAssignableFrom(method.ReturnType.GetTypeInfo()) &&
               method.Name.EndsWith(AsyncSuffix, StringComparison.OrdinalIgnoreCase);
#endif
    }
}
