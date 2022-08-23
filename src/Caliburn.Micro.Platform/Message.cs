﻿#if XFORMS
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
    public static class Message {
        internal static readonly DependencyProperty HandlerProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Handler",
                typeof(object),
                typeof(Message),
                null
                );

        static readonly DependencyProperty MessageTriggersProperty =
            DependencyPropertyHelper.RegisterAttached(
                "MessageTriggers",
                typeof(TriggerBase[]),
                typeof(Message),
                null
                );

        /// <summary>
        ///   Places a message handler on this element.
        /// </summary>
        /// <param name="d"> The element. </param>
        /// <param name="value"> The message handler. </param>
        public static void SetHandler(DependencyObject d, object value) {
            d.SetValue(HandlerProperty, value);
        }

        /// <summary>
        ///   Gets the message handler for this element.
        /// </summary>
        /// <param name="d"> The element. </param>
        /// <returns> The message handler. </returns>
        public static object GetHandler(DependencyObject d) {
            return d.GetValue(HandlerProperty);
        }

        /// <summary>
        ///   A property definition representing attached triggers and messages.
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyPropertyHelper.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(Message),
                null, 
                OnAttachChanged
                );

        /// <summary>
        ///   Sets the attached triggers and messages.
        /// </summary>
        /// <param name="d"> The element to attach to. </param>
        /// <param name="attachText"> The parsable attachment text. </param>
        public static void SetAttach(DependencyObject d, string attachText) {
            d.SetValue(AttachProperty, attachText);
        }

        /// <summary>
        ///   Gets the attached triggers and messages.
        /// </summary>
        /// <param name="d"> The element that was attached to. </param>
        /// <returns> The parsable attachment text. </returns>
        public static string GetAttach(DependencyObject d) {
            return d.GetValue(AttachProperty) as string;
        }

        static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (e.NewValue == e.OldValue) {
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
            var allTriggers = Interaction.GetTriggers(d);

             if (messageTriggers != null) {
                messageTriggers.Apply(x => allTriggers.Remove(x));
            }

            var newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.Apply(allTriggers.Add);
#endif

            if (newTriggers.Length > 0) {
                d.SetValue(MessageTriggersProperty, newTriggers);
            }
            else {
                d.ClearValue(MessageTriggersProperty);
            }
        }
    }
}
