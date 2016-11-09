#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#else
namespace Caliburn.Micro
#endif 
{
#if WinRT
    using System.Linq;
    using Windows.UI.Xaml;
    using System.Reflection;
#elif XFORMS
    using UIElement = global::Xamarin.Forms.Element;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
#else
    using System.Windows;
#endif

    /// <summary>
    ///   A host for action related attached properties.
    /// </summary>
    public static class Action {
        static readonly ILog Log = LogManager.GetLog(typeof(Action));

        /// <summary>
        ///   A property definition representing the target of an <see cref="ActionMessage" /> . The DataContext of the element will be set to this instance.
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Target",
                typeof(object),
                typeof(Action),
                null, 
                OnTargetChanged
                );

        /// <summary>
        ///   A property definition representing the target of an <see cref="ActionMessage" /> . The DataContext of the element is not set to this instance.
        /// </summary>
        public static readonly DependencyProperty TargetWithoutContextProperty =
            DependencyPropertyHelper.RegisterAttached(
                "TargetWithoutContext",
                typeof(object),
                typeof(Action),
                null, 
                OnTargetWithoutContextChanged
                );

        /// <summary>
        ///   Sets the target of the <see cref="ActionMessage" /> .
        /// </summary>
        /// <param name="d"> The element to attach the target to. </param>
        /// <param name="target"> The target for instances of <see cref="ActionMessage" /> . </param>
        public static void SetTarget(DependencyObject d, object target) {
            d.SetValue(TargetProperty, target);
        }

        /// <summary>
        ///   Gets the target for instances of <see cref="ActionMessage" /> .
        /// </summary>
        /// <param name="d"> The element to which the target is attached. </param>
        /// <returns> The target for instances of <see cref="ActionMessage" /> </returns>
        public static object GetTarget(DependencyObject d) {
            return d.GetValue(TargetProperty);
        }

        /// <summary>
        ///   Sets the target of the <see cref="ActionMessage" /> .
        /// </summary>
        /// <param name="d"> The element to attach the target to. </param>
        /// <param name="target"> The target for instances of <see cref="ActionMessage" /> . </param>
        /// <remarks>
        ///   The DataContext will not be set.
        /// </remarks>
        public static void SetTargetWithoutContext(DependencyObject d, object target) {
            d.SetValue(TargetWithoutContextProperty, target);
        }

        /// <summary>
        ///   Gets the target for instances of <see cref="ActionMessage" /> .
        /// </summary>
        /// <param name="d"> The element to which the target is attached. </param>
        /// <returns> The target for instances of <see cref="ActionMessage" /> </returns>
        public static object GetTargetWithoutContext(DependencyObject d) {
            return d.GetValue(TargetWithoutContextProperty);
        }

        ///<summary>
        ///  Checks if the <see cref="ActionMessage" /> -Target was set.
        ///</summary>
        ///<param name="element"> DependencyObject to check </param>
        ///<returns> True if Target or TargetWithoutContext was set on <paramref name="element" /> </returns>
        public static bool HasTargetSet(DependencyObject element) {
            if (GetTarget(element) != null || GetTargetWithoutContext(element) != null)
                return true;
#if XFORMS
            return false;
#else
            var frameworkElement = element as FrameworkElement;
            if (frameworkElement == null)
                return false;

            return ConventionManager.HasBinding(frameworkElement, TargetProperty)
                   || ConventionManager.HasBinding(frameworkElement, TargetWithoutContextProperty);
#endif
        }

#if !XFORMS
        ///<summary>
        ///  Uses the action pipeline to invoke the method.
        ///</summary>
        ///<param name="target"> The object instance to invoke the method on. </param>
        ///<param name="methodName"> The name of the method to invoke. </param>
        ///<param name="view"> The view. </param>
        ///<param name="source"> The source of the invocation. </param>
        ///<param name="eventArgs"> The event args. </param>
        ///<param name="parameters"> The method parameters. </param>
        public static void Invoke(object target, string methodName, DependencyObject view = null, FrameworkElement source = null, object eventArgs = null, object[] parameters = null) {

            var message = new ActionMessage {MethodName = methodName};

            var context = new ActionExecutionContext {
                Target = target,
#if WinRT
                Method = target.GetType().GetRuntimeMethods().Single(m => m.Name == methodName),
#else
                Method = target.GetType().GetMethod(methodName),
#endif
                Message = message,
                View = view,
                Source = source,
                EventArgs = eventArgs
            };

            if (parameters != null) {
                parameters.Apply(x => context.Message.Parameters.Add(x as Parameter ?? new Parameter { Value = x }));
            }

            ActionMessage.InvokeAction(context);

            // This is a bit of hack but keeps message being garbage collected
            Log.Info("Invoking action {0} on {1}.", message.MethodName, target);
        }
#endif

        static void OnTargetWithoutContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            SetTargetCore(e, d, false);
        }

        static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            SetTargetCore(e, d, true);
        }

        static void SetTargetCore(DependencyPropertyChangedEventArgs e, DependencyObject d, bool setContext) {
            if (e.NewValue == e.OldValue || (Execute.InDesignMode && e.NewValue is string)) {
                return;
            }

            var target = e.NewValue;
            var containerKey = e.NewValue as string;
            if (containerKey != null) {
                target = IoC.GetInstance(null, containerKey);
            }
#if XFORMS
            Log.Info("Attaching message handler {0} to {1}.", target, d);
            Message.SetHandler(d, target);

            if (setContext && d is FrameworkElement) {
                Log.Info("Setting DC of {0} to {1}.", d, target);
                ((FrameworkElement)d).BindingContext = target;
            }
#else
            if (setContext && d is FrameworkElement) {
                Log.Info("Setting DC of {0} to {1}.", d, target);
                ((FrameworkElement)d).DataContext = target;
            }

             Log.Info("Attaching message handler {0} to {1}.", target, d);
             Message.SetHandler(d, target);
#endif
            

        }
    }
}
