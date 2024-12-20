#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
#if WINDOWS_UWP
    using Windows.UI.Xaml;
    using Microsoft.Xaml.Interactivity;
    using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif XFORMS
    using global::Xamarin.Forms;
    using UIElement = global::Xamarin.Forms.Element;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using DependencyObject = global::Xamarin.Forms.BindableObject;
#elif AVALONIA  
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Xaml.Interactivity;
    using DependencyObject = Avalonia.AvaloniaObject;
    using TriggerBase = Avalonia.Xaml.Interactivity.Trigger;
    using DependencyPropertyChangedEventArgs = Avalonia.AvaloniaPropertyChangedEventArgs;
    using DependencyProperty = Avalonia.AvaloniaProperty;
    using EventTrigger = Avalonia.Xaml.Interactions.Core.EventTriggerBehavior;
#elif MAUI
    using global::Microsoft.Maui.Controls;
    using UIElement = global::Microsoft.Maui.Controls.Element;
    using FrameworkElement = global::Microsoft.Maui.Controls.VisualElement;
    using DependencyProperty = global::Microsoft.Maui.Controls.BindableProperty;
    using DependencyObject = global::Microsoft.Maui.Controls.BindableObject;
#else
    using System.Windows;
    using Microsoft.Xaml.Behaviors;
    using TriggerBase = Microsoft.Xaml.Behaviors.TriggerBase;
#endif


    /// <summary>
    ///   Host's attached properties related to routed UI messaging.
    /// </summary>
    public static class Message
    {
        internal static readonly DependencyProperty HandlerProperty =
#if AVALONIA
        AvaloniaProperty.RegisterAttached<AvaloniaObject, object>("Handler", typeof(Message));
#else
            DependencyPropertyHelper.RegisterAttached(
                "Handler",
                typeof(object),
                typeof(Message),
                null
                );
#endif
        static readonly ILog Log = LogManager.GetLog(typeof(Message));

        static readonly DependencyProperty MessageTriggersProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, EventTrigger[]>("MessageTriggers", typeof(Message));
#else
            DependencyPropertyHelper.RegisterAttached(
                "MessageTriggers",
                typeof(TriggerBase[]),
                typeof(Message),
                null
                );
#endif

        /// <summary>
        ///   Places a message handler on this element.
        /// </summary>
        /// <param name="d"> The element. </param>
        /// <param name="value"> The message handler. </param>
        public static void SetHandler(DependencyObject d, object value)
        {
            Log.Info("Setting handler for {0} to {1}.", d, value);
            d.SetValue(HandlerProperty, value);
        }

        /// <summary>
        ///   Gets the message handler for this element.
        /// </summary>
        /// <param name="d"> The element. </param>
        /// <returns> The message handler. </returns>
        public static object GetHandler(DependencyObject d)
        {
            return d.GetValue(HandlerProperty);
        }

        /// <summary>
        ///   A property definition representing attached triggers and messages.
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
#if AVALONIA
            AvaloniaProperty.RegisterAttached<AvaloniaObject, string>("Attach", typeof(Message));
#else
            DependencyPropertyHelper.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(Message),
                null, 
                OnAttachChanged
                );
#endif

#if AVALONIA
        static Message()
        {
            AttachProperty.Changed.Subscribe(args => OnAttachChanged(args.Sender, args));
        }
#endif
        /// <summary>
        ///   Sets the attached triggers and messages.
        /// </summary>
        /// <param name="d"> The element to attach to. </param>
        /// <param name="attachText"> The parsable attachment text. </param>
        public static void SetAttach(DependencyObject d, string attachText)
        {
            d.SetValue(AttachProperty, attachText);
        }

        /// <summary>
        ///   Gets the attached triggers and messages.
        /// </summary>
        /// <param name="d"> The element that was attached to. </param>
        /// <returns> The parsable attachment text. </returns>
        public static string GetAttach(DependencyObject d)
        {
            return d.GetValue(AttachProperty) as string;
        }

        static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (object.ReferenceEquals(e.NewValue, e.OldValue))
            {
                return;
            }

            var messageTriggers = (TriggerBase[])d.GetValue(MessageTriggersProperty);

#if WINDOWS_UWP
            var allTriggers = Interaction.GetBehaviors(d);

            if (messageTriggers != null)
            {
                messageTriggers.OfType<DependencyObject>().Apply(x => allTriggers.Remove(x));
            }

            var newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.OfType<DependencyObject>().Apply(allTriggers.Add);
#elif XFORMS || MAUI
            var visualElement = d as VisualElement;

            var allTriggers = visualElement != null ? visualElement.Triggers : new List<TriggerBase>();

            if (messageTriggers != null) {
                messageTriggers.Apply(x => allTriggers.Remove(x));
            }

            var newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.Apply(allTriggers.Add);

#else
#if AVALONIA
            var allTriggers = Interaction.GetBehaviors(d);
#else
            var allTriggers = Interaction.GetTriggers(d);
#endif
            if (messageTriggers != null)
            {
                messageTriggers.Apply(x => allTriggers.Remove(x));
            }

            var newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.Apply(allTriggers.Add);
#if AVALONIA
            //TODO: (Avalonia) Fix this workaround if there is a way to detect Trigger.AssociatedObject changes to update Trigger.Actions[].AssociatedObject 
            foreach (var t in newTriggers.Where(t => t.Actions != null && t.AssociatedObject != null))
            {
                foreach (var a in t.Actions.Cast<TriggerAction>())
                {
                    a.AssociatedObject = t.AssociatedObject as Control;
                }
            }
#endif
#endif

            if (newTriggers.Length > 0)
            {
                d.SetValue(MessageTriggersProperty, newTriggers);
            }
            else
            {
                d.ClearValue(MessageTriggersProperty);
            }
        }
    }
}
