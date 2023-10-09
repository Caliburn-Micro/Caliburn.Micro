using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

#if WINDOWS_UWP
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

using EventTrigger = Microsoft.Xaml.Interactions.Core.EventTriggerBehavior;
using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;

#else
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;

using Microsoft.Xaml.Behaviors;

using EventTrigger = Microsoft.Xaml.Behaviors.EventTrigger;
#endif

namespace Caliburn.Micro {
    /// <summary>
    /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
    /// </summary>
#if WINDOWS_UWP
    [ContentProperty(Name = "Parameters")]
#else
    [ContentProperty("Parameters")]
    [DefaultTrigger(typeof(FrameworkElement), typeof(EventTrigger), "MouseLeftButtonDown")]
    [DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]
    [TypeConstraint(typeof(FrameworkElement))]
#endif
    public class ActionMessage : TriggerAction<FrameworkElement>, IHaveParameters {
        /// <summary>
        /// Represents the method name of an action message.
        /// </summary>
        public static readonly DependencyProperty MethodNameProperty
            = DependencyProperty.Register(
                "MethodName",
                typeof(string),
                typeof(ActionMessage),
                null);

        /// <summary>
        /// Represents the parameters of an action message.
        /// </summary>
        public static readonly DependencyProperty ParametersProperty
            = DependencyProperty.Register(
                "Parameters",
                typeof(AttachedCollection<Parameter>),
                typeof(ActionMessage),
                null);

        internal static readonly DependencyProperty HandlerProperty
            = DependencyProperty.RegisterAttached(
                "Handler",
                typeof(object),
                typeof(ActionMessage),
                new PropertyMetadata(null, HandlerPropertyChanged));

        private static readonly ILog Log = LogManager.GetLog(typeof(ActionMessage));

        private ActionExecutionContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMessage"/> class.
        /// </summary>
        public ActionMessage()
            => SetValue(ParametersProperty, new AttachedCollection<Parameter>());

        /// <summary>
        /// Occurs before the message detaches from the associated object.
        /// </summary>
        public event EventHandler Detaching
            = (sender, e) => { };

        /// <summary>
        /// Gets or sets a value indicating whether the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.
        /// </summary>
        /// <remarks>This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.</remarks>
        public static bool EnforceGuardsDuringInvocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the action throw if it cannot locate the target or the method at invocation time.
        /// </summary>
        /// <remarks>True by default.</remarks>
        public static bool ThrowsExceptions { get; set; }
            = true;

        /// <summary>
        /// Gets or sets func to  return the list of possible names of guard methods / properties for the given method.
        /// </summary>
        public static Func<MethodInfo, IEnumerable<string>> BuildPossibleGuardNames { get; set; }
            = method
            => {
                var guardNames = new List<string>();

                const string GuardPrefix = "Can";

                string methodName = method.Name;

                guardNames.Add(GuardPrefix + methodName);

                const string AsyncMethodSuffix = "Async";

                if (!methodName.EndsWith(AsyncMethodSuffix, StringComparison.OrdinalIgnoreCase)) {
                    return guardNames;
                }

                guardNames.Add(GuardPrefix + methodName.Substring(0, methodName.Length - AsyncMethodSuffix.Length));

                return guardNames;
            };

        /// <summary>
        /// Gets or sets action to invoke the action using the specified <see cref="ActionExecutionContext"/>.
        /// </summary>
        public static Action<ActionExecutionContext> InvokeAction { get; set; }
            = context
                => {
                    object[] values = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
                    object returnValue = context.Method.Invoke(context.Target, values);

                    if (returnValue is System.Threading.Tasks.Task task) {
                        returnValue = task.AsResult();
                    }

                    if (returnValue is IResult result) {
                        returnValue = new[] { result };
                    }

                    if (returnValue is IEnumerable<IResult> enumerable) {
                        returnValue = enumerable.GetEnumerator();
                    }

                    if (returnValue is IEnumerator<IResult> enumerator) {
                        Coroutine.BeginExecute(
                            enumerator,
                            new CoroutineExecutionContext {
                                Source = context.Source,
                                View = context.View,
                                Target = context.Target,
                            });
                    }
                };

        /// <summary>
        /// Gets or sets func to apply an availability effect, such as IsEnabled, to an element.
        /// </summary>
        /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect { get; set; }
            = context
                => {
#if WINDOWS_UWP
                    if (context.Source is not Control source) {
                        return true;
                    }
#else
                    FrameworkElement source = context.Source;
                    if (source == null) {
                        return true;
                    }
#endif

#if WINDOWS_UWP
                    bool hasBinding = ConventionManager.HasBinding(source, Control.IsEnabledProperty);
#else
                    bool hasBinding = ConventionManager.HasBinding(source, UIElement.IsEnabledProperty);
#endif
                    if (!hasBinding && context.CanExecute != null) {
                        source.IsEnabled = context.CanExecute();
                    }

                    return source.IsEnabled;
                };

        /// <summary>
        /// Gets or sets func to find the method on the target matching the specified message.
        /// </summary>
        /// <returns>The matching method, if available.</returns>
        public static Func<ActionMessage, object, MethodInfo> GetTargetMethod { get; set; }
            = (message, target)
                =>
#if WINDOWS_UWP
                    (from method in target.GetType().GetRuntimeMethods()
                     where method.Name == message.MethodName
                     let methodParameters = method.GetParameters()
                     where message.Parameters.Count == methodParameters.Length
                     select method).FirstOrDefault();
#else
                    (from method in target.GetType().GetMethods()
                     where method.Name == message.MethodName
                     let methodParameters = method.GetParameters()
                     where message.Parameters.Count == methodParameters.Length
                     select method).FirstOrDefault();
#endif

        /// <summary>
        /// Gets or sets action to Set the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding { get; set; }
            = context
                => {
                    FrameworkElement source = context.Source;

                    DependencyObject currentElement = source;
                    while (currentElement != null) {
                        if (Action.HasTargetSet(currentElement)) {
                            object target = Message.GetHandler(currentElement);
                            if (target != null) {
                                MethodInfo method = GetTargetMethod(context.Message, target);
                                if (method != null) {
                                    context.Method = method;
                                    context.Target = target;
                                    context.View = currentElement;
                                    return;
                                }
                            } else {
                                context.View = currentElement;
                                return;
                            }
                        }

                        currentElement = BindingScope.GetVisualParent(currentElement);
                    }

                    if (source != null && source.DataContext != null) {
                        object target = source.DataContext;
                        MethodInfo method = GetTargetMethod(context.Message, target);

                        if (method != null) {
                            context.Target = target;
                            context.Method = method;
                            context.View = source;
                        }
                    }
                };

        /// <summary>
        /// Gets or sets action to prepare the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext { get; set; }
            = context
                => {
                    SetMethodBinding(context);
                    if (context.Target == null || context.Method == null) {
                        return;
                    }

                    var possibleGuardNames = BuildPossibleGuardNames(context.Method).ToList();

                    MethodInfo guard = TryFindGuardMethod(context, possibleGuardNames);

                    if (guard != null) {
                        context.CanExecute = () => (bool)guard.Invoke(context.Target, MessageBinder.DetermineParameters(context, guard.GetParameters()));

                        return;
                    }

                    if (context.Target is not INotifyPropertyChanged inpc) {
                        return;
                    }

                    Type targetType = context.Target.GetType();
                    string matchingGuardName = null;
                    foreach (string possibleGuardName in possibleGuardNames) {
                        matchingGuardName = possibleGuardName;
                        guard = GetMethodInfo(targetType, "get_" + matchingGuardName);
                        if (guard != null) {
                            break;
                        }
                    }

                    if (guard == null) {
                        return;
                    }

                    void OnPropertyChanged(object s, PropertyChangedEventArgs e) {
                        if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == matchingGuardName) {
                            Caliburn.Micro.Execute.OnUIThread(() => {
                                ActionMessage message = context.Message;
                                if (message == null) {
                                    inpc.PropertyChanged -= OnPropertyChanged;
                                    return;
                                }

                                message.UpdateAvailability();
                            });
                        }
                    }

                    inpc.PropertyChanged += OnPropertyChanged;
                    context.Disposing += (sender, e) => inpc.PropertyChanged -= OnPropertyChanged;
                    context.Message.Detaching += (sender, e) => inpc.PropertyChanged -= OnPropertyChanged;
                    context.CanExecute = () => (bool)guard.Invoke(context.Target, MessageBinder.DetermineParameters(context, guard.GetParameters()));
                };

        /// <summary>
        /// Gets or sets the name of the method to be invoked on the presentation model class.
        /// </summary>
        /// <value>The name of the method.</value>
#if !WINDOWS_UWP
        [Category("Common Properties")]
#endif
        public string MethodName {
            get => (string)GetValue(MethodNameProperty);
            set => SetValue(MethodNameProperty, value);
        }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
#if !WINDOWS_UWP
        [Category("Common Properties")]
#endif
        public AttachedCollection<Parameter> Parameters
            => (AttachedCollection<Parameter>)GetValue(ParametersProperty);

        /// <summary>
        /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
        /// </summary>
        public virtual void UpdateAvailability() {
            if (context == null) {
                return;
            }

            if (context.Target == null || context.View == null) {
                PrepareContext(context);
            }

            UpdateAvailabilityCore();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current <see cref="object"/>.
        /// </returns>
        public override string ToString()
            => "Action: " + MethodName;

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching() {
            if (!View.InDesignMode) {
                Detaching(this, EventArgs.Empty);
                AssociatedObject.Loaded -= ElementLoaded;
                Parameters.Detach();
            }

            base.OnDetaching();
        }

        /*
         * Change in the public API of Microsoft.Xaml.Behaviors causing CA1725 issue.
         * The parameter name changed from 'parameter' to 'parmeter' (without second 'a')
         * This if statement to reslove
         * CA1725: Parameter names should match base declaration.
        */
#if UAP10_0_19041
        /// <summary>Invokes the action.</summary>
        /// <param name="parmeter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parmeter) {
#else
        /// <summary>Invokes the action.</summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter) {
#endif
            Log.Info("Invoking {0}.", this);

            if (context == null) {
                UpdateContext();
            }

            if (context.Target == null || context.View == null) {
                PrepareContext(context);
                if (context.Target == null) {
                    var ex = new Exception(string.Format(CultureInfo.InvariantCulture, "No target found for method {0}.", context.Message.MethodName));
                    Log.Error(ex);

                    if (!ThrowsExceptions) {
                        return;
                    }

                    throw ex;
                }

                if (!UpdateAvailabilityCore()) {
                    return;
                }
            }

            if (context.Method == null) {
                var ex = new Exception(string.Format(CultureInfo.InvariantCulture, "Method {0} not found on target of type {1}.", context.Message.MethodName, context.Target.GetType()));
                Log.Error(ex);

                if (!ThrowsExceptions) {
                    return;
                }

                throw ex;
            }

#if UAP10_0_19041
            context.EventArgs = parmeter;
#else
            context.EventArgs = parameter;
#endif

            if (EnforceGuardsDuringInvocation && context.CanExecute != null && !context.CanExecute()) {
                return;
            }

            InvokeAction(context);
            context.EventArgs = null;
        }

        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
#if WINDOWS_UWP
        protected override void OnAttached() {
            if (!View.InDesignMode) {
                Parameters.Attach(AssociatedObject);
                Parameters.OfType<Parameter>().Apply(x => x.MakeAwareOf(this));

                if (View.ExecuteOnLoad(AssociatedObject, ElementLoaded)) {
                    // Not yet sure if this will be needed
                    // var trigger = Interaction.GetTriggers(AssociatedObject)
                    //    .FirstOrDefault(t => t.Actions.Contains(this)) as EventTrigger;
                    // if (trigger != null && trigger.EventName == "Loaded")
                    //    Invoke(new RoutedEventArgs());
                }

                View.ExecuteOnUnload(AssociatedObject, (s, e) => OnDetaching());
            }

            base.OnAttached();
        }
#else
        protected override void OnAttached() {
            if (!View.InDesignMode) {
                Parameters.Attach(AssociatedObject);
                Parameters.Apply(x => x.MakeAwareOf(this));

                if (View.ExecuteOnLoad(AssociatedObject, ElementLoaded)) {
                    if (Interaction.GetTriggers(AssociatedObject)
                        .FirstOrDefault(t => t.Actions.Contains(this)) is EventTrigger trigger && trigger.EventName == "Loaded") {
                        Invoke(new RoutedEventArgs());
                    }
                }
            }

            base.OnAttached();
        }
#endif

        /// <summary>
        /// Try to find a candidate for guard function, having:
        ///    - a name matching any of <paramref name="possibleGuardNames"/>
        ///    - no generic parameters
        ///    - a bool return type
        ///    - no parameters or a set of parameters corresponding to the action method.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="possibleGuardNames">Method names to look for.</param>
        /// <returns>A MethodInfo, if found; null otherwise.</returns>
        private static MethodInfo TryFindGuardMethod(ActionExecutionContext context, IEnumerable<string> possibleGuardNames) {
            Type targetType = context.Target.GetType();
            MethodInfo guard = null;
            foreach (string possibleGuardName in possibleGuardNames) {
                guard = GetMethodInfo(targetType, possibleGuardName);
                if (guard != null) {
                    break;
                }
            }

            if (guard == null) {
                return null;
            }

            if (guard.ContainsGenericParameters) {
                return null;
            }

            if (!typeof(bool).Equals(guard.ReturnType)) {
                return null;
            }

            ParameterInfo[] guardPars = guard.GetParameters();
            ParameterInfo[] actionPars = context.Method.GetParameters();
            if (guardPars.Length == 0) {
                return guard;
            }

            if (guardPars.Length != actionPars.Length) {
                return null;
            }

            IEnumerable<bool> comparisons = guardPars.Zip(
                context.Method.GetParameters(),
                (x, y) => x.ParameterType == y.ParameterType);

            return comparisons.Any(x => !x) ? null : guard;
        }

        private static void HandlerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ActionMessage)d).UpdateContext();

        private static MethodInfo GetMethodInfo(Type t, string methodName) =>
#if WINDOWS_UWP
            t.GetRuntimeMethods().SingleOrDefault(m => m.Name == methodName);
#else
            t.GetMethod(methodName);
#endif

        private bool UpdateAvailabilityCore() {
            Log.Info("{0} availability update.", this);

            return ApplyAvailabilityEffect(context);
        }

        private void UpdateContext() {
            context?.Dispose();

            context = new ActionExecutionContext {
                Message = this,
                Source = AssociatedObject,
            };

            PrepareContext(context);
            UpdateAvailabilityCore();
        }

        private void ElementLoaded(object sender, RoutedEventArgs e) {
            UpdateContext();

            DependencyObject currentElement;
            if (context.View == null) {
                currentElement = AssociatedObject;
                while (currentElement != null) {
                    if (Action.HasTargetSet(currentElement)) {
                        break;
                    }

                    currentElement = BindingScope.GetVisualParent(currentElement);
                }
            } else {
                currentElement = context.View;
            }

#if NET || CAL_NETCORE
            var binding = new Binding {
                Source = currentElement,
                Path = new PropertyPath(Message.HandlerProperty),
            };
#elif WINDOWS_UWP
            var binding = new Binding {
                Source = currentElement,
            };
#else
            const string bindingText = "<Binding xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation\' xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro.Platform' Path='(cal:Message.Handler)' />";

            var binding = (Binding)XamlReader.Load(bindingText);
            binding.Source = currentElement;
#endif
            BindingOperations.SetBinding(this, HandlerProperty, binding);
        }
    }
}
