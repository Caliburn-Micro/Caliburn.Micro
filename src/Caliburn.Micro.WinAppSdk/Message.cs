﻿namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml;
    using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
    using Microsoft.UI.Xaml;



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

            var allTriggers = Interaction.GetBehaviors(d);

            if (messageTriggers != null)
            {
                messageTriggers.OfType<DependencyObject>().Apply(x => allTriggers.Remove(x));
            }

            var newTriggers = Parser.Parse(d, e.NewValue as string).ToArray();
            newTriggers.OfType<DependencyObject>().Apply(allTriggers.Add);

            if (newTriggers.Length > 0) {
                d.SetValue(MessageTriggersProperty, newTriggers);
            }
            else {
                d.ClearValue(MessageTriggersProperty);
            }
        }
    }
}
