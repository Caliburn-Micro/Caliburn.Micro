namespace Caliburn.Micro
{
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// Host's attached properties related to routed UI messaging.
    /// </summary>
    public static class Message {
        internal static readonly DependencyProperty HandlerProperty =
            DependencyProperty.RegisterAttached(
                "Handler",
                typeof(object),
                typeof(Message),
                null
                );

        /// <summary>
        /// Places a message handler on this element.
        /// </summary>
        /// <param name="d">The element.</param>
        /// <param name="value">The message handler.</param>
        public static void SetHandler(DependencyObject d, object value)
        {
            d.SetValue(HandlerProperty, value);
        }

        /// <summary>
        /// Gets the message handler for this element.
        /// </summary>
        /// <param name="d">The element.</param>
        /// <returns>The message handler.</returns>
        public static object GetHandler(DependencyObject d)
        {
            return d.GetValue(HandlerProperty);
        }

        /// <summary>
        /// A property definition representing attached triggers and messages.
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(Message),
                new PropertyMetadata(OnAttachChanged)
                );

        /// <summary>
        /// Sets the attached triggers and messages.
        /// </summary>
        /// <param name="d">The element to attach to.</param>
        /// <param name="attachText">The parsable attachment text.</param>
        public static void SetAttach(DependencyObject d, string attachText)
        {
            d.SetValue(AttachProperty, attachText);
        }

        /// <summary>
        /// Gets the attached triggers and messages.
        /// </summary>
        /// <param name="d">The element that was attached to.</param>
        /// <returns>The parsable attachment text.</returns>
        public static string GetAttach(DependencyObject d)
        {
            return d.GetValue(AttachProperty) as string;
        }

        private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue == e.OldValue)
                return;

            var text = e.NewValue as string;
            if(string.IsNullOrEmpty(text))
                return;

            var triggers = Interaction.GetTriggers(d);
            Parser.Parse(d, text).Apply(triggers.Add);
        }
    }
}