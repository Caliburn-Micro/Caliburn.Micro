﻿namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
#if WINDOWS_UWP
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Controls;
    using Microsoft.Xaml.Interactivity;
    using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
    using EventTrigger = Microsoft.Xaml.Interactions.Core.EventTriggerBehavior;
#elif AVALONIA
    using Avalonia;
    using Avalonia.Data;
    using Avalonia.Data.Core;
    using Avalonia.Interactivity;
    using Avalonia.Xaml.Interactivity;
    using Avalonia.VisualTree;
    using Avalonia.Xaml.Interactions.Core;
    using DependencyObject = Avalonia.AvaloniaObject;
    using XamlReader = Avalonia.Markup.Xaml.AvaloniaRuntimeXamlLoader;
    using UIElement = Avalonia.Input.InputElement;
    using DependencyPropertyChangedEventArgs = Avalonia.AvaloniaPropertyChangedEventArgs;
    using DependencyProperty = Avalonia.AvaloniaProperty;
    using EventTrigger = Avalonia.Xaml.Interactions.Core.EventTriggerBehavior;
    using FrameworkElement = Avalonia.Controls.Control;
    using Avalonia.Input;
#else
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Markup;
    using Microsoft.Xaml.Behaviors;
    using EventTrigger = Microsoft.Xaml.Behaviors.EventTrigger;
#endif

#if NET5_0_WINDOWS || NET6_0_WINDOWS
    using System.IO;
    using System.Xml;
#endif


    /// <summary>
    /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
    /// </summary>
#if WINDOWS_UWP
    [ContentProperty(Name = "Parameters")]
#elif !AVALONIA
    [ContentProperty("Parameters")]
    [DefaultTrigger(typeof(FrameworkElement), typeof(EventTrigger), "MouseLeftButtonDown")]
    [DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]
    [TypeConstraint(typeof(FrameworkElement))]
#endif
    public class ActionMessage : TriggerAction<FrameworkElement>, IHaveParameters
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ActionMessage));
        ActionExecutionContext _context;

        internal static readonly DependencyProperty HandlerProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, object>(
                "Handler", typeof(ActionMessage)
                );
#else
            DependencyProperty.RegisterAttached(
            "Handler",
            typeof(object),
            typeof(ActionMessage),
            new PropertyMetadata(null, HandlerPropertyChanged)
            );
#endif



        ///<summary>
        /// Causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.
        ///</summary>
        /// <remarks>This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.</remarks>
        public static bool EnforceGuardsDuringInvocation = false;

        ///<summary>
        /// Causes the action to throw if it cannot locate the target or the method at invocation time.
        ///</summary>
        /// <remarks>True by default.</remarks>
        public static bool ThrowsExceptions = true;

        /// <summary>
        /// Represents the method name of an action message.
        /// </summary>
        public static readonly DependencyProperty MethodNameProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, string>("MethodName", typeof(ActionMessage));
#else
            DependencyProperty.Register(
                "MethodName",
                typeof(string),
                typeof(ActionMessage),
                null
                );
#endif

        /// <summary>
        /// Represents the parameters of an action message.
        /// </summary>
        public static readonly DependencyProperty ParametersProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, AttachedCollection<Parameter>>(
                "Parameters",
                typeof(ActionMessage)
                );
#else
            DependencyProperty.Register(
            "Parameters",
            typeof(AttachedCollection<Parameter>),
            typeof(ActionMessage),
            null
            );
#endif

#if AVALONIA
        static ActionMessage()
        {
            Log.Info("ActionMessage Avalonia");
            HandlerProperty.Changed.Subscribe(args => HandlerPropertyChanged(args.Sender, args));
        }
#endif

        /// <summary>
        /// Creates an instance of <see cref="ActionMessage"/>.
        /// </summary>
        public ActionMessage()
        {
            SetValue(ParametersProperty, new AttachedCollection<Parameter>());
        }

        /// <summary>
        /// Gets or sets the name of the method to be invoked on the presentation model class.
        /// </summary>
        /// <value>The name of the method.</value>
#if !WINDOWS_UWP
        [Category("Common Properties")]
#endif
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
#if !WINDOWS_UWP
        [Category("Common Properties")]
#endif
        public AttachedCollection<Parameter> Parameters
        {
            get { return (AttachedCollection<Parameter>)GetValue(ParametersProperty); }
        }

        /// <summary>
        /// Occurs before the message detaches from the associated object.
        /// </summary>
        public event EventHandler Detaching = delegate { };

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
                    //var trigger = Interaction.GetTriggers(AssociatedObject)
                    //    .FirstOrDefault(t => t.Actions.Contains(this)) as EventTrigger;
                    //if (trigger != null && trigger.EventName == eventName)
                    //    Invoke(new RoutedEventArgs());
                }

                View.ExecuteOnUnload(AssociatedObject, ElementUnloaded);
            }

            base.OnAttached();
        }

        void ElementUnloaded(object sender, RoutedEventArgs e)
        {
            OnDetaching();
        }
#else
        protected override void OnAttached()
        {
            if (!View.InDesignMode)
            {
                Parameters.Attach(AssociatedObject);
                Parameters.Apply(x => x.MakeAwareOf(this));

                if (View.ExecuteOnLoad(AssociatedObject, ElementLoaded))
                {
#if AVALONIA
                    string eventName = "AttachedToLogicalTree";
                    var trigger = Interaction.GetBehaviors(AssociatedObject)
                        .OfType<Trigger>()
                        .FirstOrDefault(t => t.Actions.Contains(this)) as EventTriggerBehavior;
                    Log.Info($"Trigger is null {trigger == null}");
#else
                   string eventName = "Loaded";
                   var trigger = Interaction.GetTriggers(AssociatedObject)
                    .FirstOrDefault(t => t.Actions.Contains(this)) as EventTrigger;
#endif
                    if (trigger != null && trigger.EventName == eventName)
                        Invoke(new RoutedEventArgs());
                }
            }

            base.OnAttached();
        }
#endif

        static void HandlerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Log.Info($"Handler property changed {d}");
            ((ActionMessage)d).UpdateContext();
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (!View.InDesignMode)
            {
                Detaching(this, EventArgs.Empty);
#if AVALONIA
                //TODO: (Avalonia) Remove the ElementLoaded handler added in OnAttached
#else
                //TODO: Fix this: cannot remove ElementLoaded here because a wrapper handler was added instead (in View.ExecuteOnLoad() called from this.OnAttached())
                AssociatedObject.Loaded -= ElementLoaded;
#endif
                Parameters.Detach();
            }

            base.OnDetaching();
        }

#if AVALONIA
        void ElementLoaded(object sender, EventArgs e)
        {
            DependencyObject elementToUse = null;
#else
        void ElementLoaded(object sender, RoutedEventArgs e)
        {
#endif
            UpdateContext();

            DependencyObject currentElement;
            if (_context.View == null)
            {
                currentElement = AssociatedObject;
                while (currentElement != null)
                {
                    if (Action.HasTargetSet(currentElement))
                        break;
                    
#if AVALONIA
                    var currentView = ((Visual)currentElement);
                    if (elementToUse == null)
                        elementToUse = currentElement;
                    var currentParent = currentView.GetVisualParent();
                    if (currentParent?.GetVisualParent() != null)
                    {
                        currentParent = currentParent.GetVisualParent();
                    }
                    currentElement = currentParent as AvaloniaObject;
#else
                    currentElement = BindingScope.GetVisualParent(currentElement);
#endif
                }
            }
            else
                currentElement = _context.View;

#if AVALONIA
            var binding = new Binding
            {
                Path = "(cal:Message.Handler)",
                TypeResolver = (s, s1) =>
                {
                    return typeof(Message);
                },
                Source = elementToUse
            };
            Log.Info($"Binding {binding.Source}");
#elif ( NET || CAL_NETCORE && !WINDOWS_UWP)
            var binding = new Binding {
                Path = new PropertyPath(Message.HandlerProperty), 
                Source = currentElement
            };
#elif WINDOWS_UWP
            var binding = new Binding {
                Source = currentElement
            };
#else
            const string bindingText = "<Binding xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation\' xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro.Platform' Path='(cal:Message.Handler)' />";

            var binding = (Binding)XamlReader.Load(bindingText);
            binding.Source = currentElement;
#endif
#if AVALONIA
            if (elementToUse != null)
            {
                Log.Info($"GetObservable {HandlerProperty.Name}");
                var myObservable= elementToUse.GetObservable(HandlerProperty);
                 Log.Info($"myObservable is null  {myObservable == null}");
               myObservable.Subscribe(x =>
                {
                    Log.Info($"GetObservable subscribe {elementToUse}");
                    Log.Info($"GetObservable x is null {x==null}");
                    if (x != null)
                    {
                        Log.Info($"GetObservable invoke {elementToUse}");
                        Invoke(new RoutedEventArgs());
                    }
                });
                Log.Info($"Binding event {binding.Path} {binding.Source} {HandlerProperty.Name}");
                this.Bind(HandlerProperty, binding);
            }
#else
            BindingOperations.SetBinding(this, HandlerProperty, binding);
#endif
        }

        void UpdateContext()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            _context = new ActionExecutionContext
            {
                Message = this,
                Source = AssociatedObject
            };

            PrepareContext(_context);
            UpdateAvailabilityCore();
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="eventArgs">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object eventArgs)
        {
            Log.Info("Invoking {0}.", this);

            if (_context == null)
            {
                UpdateContext();
            }

            if (_context.Target == null || _context.View == null)
            {
                PrepareContext(_context);
                if (_context.Target == null)
                {
                    var ex = new Exception(string.Format("No target found for method {0}.", _context.Message.MethodName));
                    Log.Error(ex);

                    if (!ThrowsExceptions)
                        return;
                    throw ex;
                }

                if (!UpdateAvailabilityCore())
                {
                    return;
                }
            }

            if (_context.Method == null)
            {
                var ex = new Exception(string.Format("Method {0} not found on target of type {1}.", _context.Message.MethodName, _context.Target.GetType()));
                Log.Error(ex);

                if (!ThrowsExceptions)
                    return;
                throw ex;
            }

            _context.EventArgs = eventArgs;

            if (EnforceGuardsDuringInvocation && _context.CanExecute != null && !_context.CanExecute())
            {
                return;
            }

            InvokeAction(_context);
            _context.EventArgs = null;
        }

        /// <summary>
        /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
        /// </summary>
        public virtual void UpdateAvailability()
        {
            if (_context == null)
            {
                Log.Info("{0} availability update. Context is null", this);
                return;
            }
            if (_context.Target == null || _context.View == null)
                PrepareContext(_context);

            UpdateAvailabilityCore();
        }

        bool UpdateAvailabilityCore()
        {
            Log.Info("{0} availability update.", this);
            return ApplyAvailabilityEffect(_context);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return "Action: " + MethodName;
        }

        /// <summary>
        /// Invokes the action using the specified <see cref="ActionExecutionContext"/>
        /// </summary>
        public static Action<ActionExecutionContext> InvokeAction = context =>
        {
            var values = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
            var returnValue = context.Method.Invoke(context.Target, values);

            var task = returnValue as System.Threading.Tasks.Task;
            if (task != null)
            {
                returnValue = task.AsResult();
            }

            var result = returnValue as IResult;
            if (result != null)
            {
                returnValue = new[] { result };
            }

            var enumerable = returnValue as IEnumerable<IResult>;
            if (enumerable != null)
            {
                returnValue = enumerable.GetEnumerator();
            }

            var enumerator = returnValue as IEnumerator<IResult>;
            if (enumerator != null)
            {
                Coroutine.BeginExecute(enumerator,
                    new CoroutineExecutionContext
                    {
                        Source = context.Source,
                        View = context.View,
                        Target = context.Target
                    });
            }
        };

        /// <summary>
        /// Applies an availability effect, such as IsEnabled, to an element.
        /// </summary>
        /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect = context =>
        {
            Log.Info("ApplyAvailabilityEffect");

#if WINDOWS_UWP
            var source = context.Source as Control;
#else
            var source = context.Source;
#endif
            if (source == null)
            {
                Log.Info("ApplyAvailabilityEffect source is null");
                return true;
            }

#if WINDOWS_UWP
            var hasBinding = ConventionManager.HasBinding(source, Control.IsEnabledProperty);
#else
            var hasBinding = ConventionManager.HasBinding(source, UIElement.IsEnabledProperty);
#endif
            Log.Info($"ApplyAvailabilityEffect hasBinding {hasBinding}");

#if AVALONIA
            if(context.CanExecute != null)
            {
                Log.Info($"ApplyAvailabilityEffect CanExecute {context.CanExecute} - {context.Method.Name}");
                source.IsEnabled = context.CanExecute();
            }
#else
            if(!hasBinding && context.CanExecute != null)
            {
                Log.Info($"ApplyAvailabilityEffect CanExecute {context.CanExecute} - {context.Method.Name}");
                source.IsEnabled = context.CanExecute();
            }
#endif
            return source.IsEnabled;
        };

        /// <summary>
        /// Finds the method on the target matching the specified message.
        /// </summary>
        /// <returns>The matching method, if available.</returns>
        public static Func<ActionMessage, object, MethodInfo> GetTargetMethod = (message, target) =>
        {
#if WINDOWS_UWP
            return (from method in target.GetType().GetRuntimeMethods()
                    where method.Name == message.MethodName
                    let methodParameters = method.GetParameters()
                    where message.Parameters.Count == methodParameters.Length 
                    select method).FirstOrDefault();
#else
            return (from method in target.GetType().GetMethods()
                    where method.Name == message.MethodName
                    let methodParameters = method.GetParameters()
                    where message.Parameters.Count == methodParameters.Length
                    select method).FirstOrDefault();
#endif
        };

        /// <summary>
        /// Sets the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding = context =>
        {
            var source = context.Source;

            DependencyObject currentElement = source;
            while (currentElement != null)
            {
                if (Action.HasTargetSet(currentElement))
                {
                    var target = Message.GetHandler(currentElement);
                    if (target != null)
                    {
                        var method = GetTargetMethod(context.Message, target);
                        if (method != null)
                        {
                            context.Method = method;
                            context.Target = target;
                            context.View = currentElement;
                            return;
                        }
                    }
                    else
                    {
                        context.View = currentElement;
                        return;
                    }
                }

                currentElement = BindingScope.GetVisualParent(currentElement);
            }
#if AVALONIA
            if (source != null  && context.Target != null)
            {
                var target = context.Target;
                var method = GetTargetMethod(context.Message, target);

                if (method != null)
                {
                    context.Target = target;
                    context.Method = method;
                    context.View = source;
                }
            }
#else
            if (source != null && source.DataContext != null)
            {
                var target = source.DataContext;
                var method = GetTargetMethod(context.Message, target);

                if (method != null)
                {
                    context.Target = target;
                    context.Method = method;
                    context.View = source;
                }
            }
#endif
        };

        /// <summary>
        /// Prepares the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext = context =>
        {
            SetMethodBinding(context);
            if (context.Target == null || context.Method == null)
            {
                return;
            }
            var possibleGuardNames = BuildPossibleGuardNames(context.Method).ToList();

            var guard = TryFindGuardMethod(context, possibleGuardNames);

            if (guard == null)
            {
                var inpc = context.Target as INotifyPropertyChanged;
                if (inpc == null)
                    return;

                var targetType = context.Target.GetType();
                string matchingGuardName = null;
                foreach (string possibleGuardName in possibleGuardNames)
                {
                    matchingGuardName = possibleGuardName;
                    guard = GetMethodInfo(targetType, "get_" + matchingGuardName);
                    if (guard != null)
                        break;
                }

                if (guard == null)
                    return;

                PropertyChangedEventHandler handler = null;
                handler = (s, e) =>
                {
                    if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == matchingGuardName)
                    {
                        Log.Info($"UpdateAvailablilty  {e.PropertyName}"); 
                        Caliburn.Micro.Execute.OnUIThread(() =>
                        {
                            var message = context.Message;
                            if (message == null)
                            {
                                inpc.PropertyChanged -= handler;
                                return;
                            }
                            message.UpdateAvailability();
                        });
                    }
                };

                inpc.PropertyChanged += handler;
                context.Disposing += delegate
                { inpc.PropertyChanged -= handler; };
                context.Message.Detaching += delegate
                { inpc.PropertyChanged -= handler; };
            }

            context.CanExecute = () => (bool)guard.Invoke(
                context.Target,
                MessageBinder.DetermineParameters(context, guard.GetParameters()));
        };

        /// <summary>
        /// Try to find a candidate for guard function, having: 
        ///    - a name matching any of <paramref name="possibleGuardNames"/>
        ///    - no generic parameters
        ///    - a bool return type
        ///    - no parameters or a set of parameters corresponding to the action method
        /// </summary>
        /// <param name="context">The execution context</param>
        /// <param name="possibleGuardNames">Method names to look for.</param>
        ///<returns>A MethodInfo, if found; null otherwise</returns>
        static MethodInfo TryFindGuardMethod(ActionExecutionContext context, IEnumerable<string> possibleGuardNames)
        {
            var targetType = context.Target.GetType();
            MethodInfo guard = null;
            foreach (string possibleGuardName in possibleGuardNames)
            {
                guard = GetMethodInfo(targetType, possibleGuardName);
                if (guard != null)
                    break;
            }

            if (guard == null)
                return null;
            if (guard.ContainsGenericParameters)
                return null;
            if (!typeof(bool).Equals(guard.ReturnType))
                return null;

            var guardPars = guard.GetParameters();
            var actionPars = context.Method.GetParameters();
            if (guardPars.Length == 0)
                return guard;
            if (guardPars.Length != actionPars.Length)
                return null;

            var comparisons = guardPars.Zip(
                context.Method.GetParameters(),
                (x, y) => x.ParameterType == y.ParameterType
                );

            if (comparisons.Any(x => !x))
            {
                return null;
            }

            return guard;
        }

        /// <summary>
        /// Returns the list of possible names of guard methods / properties for the given method.
        /// </summary>
        public static Func<MethodInfo, IEnumerable<string>> BuildPossibleGuardNames = method =>
        {

            var guardNames = new List<string>();

            const string GuardPrefix = "Can";

            var methodName = method.Name;

            guardNames.Add(GuardPrefix + methodName);

            const string AsyncMethodSuffix = "Async";

            if (methodName.EndsWith(AsyncMethodSuffix, StringComparison.OrdinalIgnoreCase))
            {
                guardNames.Add(GuardPrefix + methodName.Substring(0, methodName.Length - AsyncMethodSuffix.Length));
            }

            return guardNames;
        };

        static MethodInfo GetMethodInfo(Type t, string methodName)
        {
#if WINDOWS_UWP
            return t.GetRuntimeMethods().SingleOrDefault(m => m.Name == methodName);
#else
            return t.GetMethod(methodName);
#endif
        }
    }
}
