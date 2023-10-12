using System.Linq;

#if WINDOWS_UWP
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;

using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif XFORMS
using System.Collections.Generic;

using Xamarin.Forms;

using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;

#elif MAUI
using System.Collections.Generic;

using Microsoft.Maui.Controls;

using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;
#else
using System.Windows;

using Microsoft.Xaml.Behaviors;

using TriggerBase = Microsoft.Xaml.Behaviors.TriggerBase;
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
    ///   Host's attached properties related to routed UI messaging.
    /// </summary>
    public static class Message {
        /// <summary>
        ///   A property definition representing attached triggers and messages.
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(Message),
                null,
                OnAttachChanged);

        internal static readonly DependencyProperty HandlerProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Handler",
                typeof(object),
                typeof(Message),
                null);

        private static readonly DependencyProperty MessageTriggersProperty =
            DependencyPropertyHelper.RegisterAttached(
                "MessageTriggers",
                typeof(TriggerBase[]),
                typeof(Message),
                null);

        /// <summary>
        ///   Places a message handler on this element.
        /// </summary>
        /// <param name="d"> The element. </param>
        /// <param name="value"> The message handler. </param>
        public static void SetHandler(DependencyObject d, object value)
            => d.SetValue(HandlerProperty, value);

        /// <summary>
        ///   Gets the message handler for this element.
        /// </summary>
        /// <param name="d"> The element. </param>
        /// <returns> The message handler. </returns>
        public static object GetHandler(DependencyObject d)
            => d.GetValue(HandlerProperty);

        /// <summary>
        ///   Sets the attached triggers and messages.
        /// </summary>
        /// <param name="d"> The element to attach to. </param>
        /// <param name="attachText"> The parsable attachment text. </param>
        public static void SetAttach(DependencyObject d, string attachText)
            => d.SetValue(AttachProperty, attachText);

        /// <summary>
        ///   Gets the attached triggers and messages.
        /// </summary>
        /// <param name="d"> The element that was attached to. </param>
        /// <returns> The parsable attachment text. </returns>
        public static string GetAttach(DependencyObject d)
            => d.GetValue(AttachProperty) as string;

        private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (e.NewValue == e.OldValue) {
                return;
            }

            var messageTriggers = (TriggerBase[])d.GetValue(MessageTriggersProperty);
#if WINDOWS_UWP
            BehaviorCollection allTriggers = Interaction.GetBehaviors(d);
            messageTriggers?.OfType<DependencyObject>().Apply(x => allTriggers.Remove(x));
            TriggerBase[] newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.OfType<DependencyObject>().Apply(allTriggers.Add);
#elif XFORMS || MAUI
            IList<TriggerBase> allTriggers = d is VisualElement visualElement ? visualElement.Triggers : new List<TriggerBase>();
            messageTriggers?.Apply(x => allTriggers.Remove(x));
            TriggerBase[] newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.Apply(allTriggers.Add);
#else
            Microsoft.Xaml.Behaviors.TriggerCollection allTriggers = Interaction.GetTriggers(d);
            messageTriggers?.Apply(x => allTriggers.Remove(x));
            TriggerBase[] newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.Apply(allTriggers.Add);
#endif

            if (newTriggers.Length > 0) {
                d.SetValue(MessageTriggersProperty, newTriggers);
            } else {
                d.ClearValue(MessageTriggersProperty);
            }
        }
    }
}
