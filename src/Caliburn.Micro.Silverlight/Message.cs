namespace Caliburn.Micro
{
    using System.Windows;
    using System.Windows.Interactivity;

    public static class Message
    {
        internal static readonly DependencyProperty HandlerProperty =
            DependencyProperty.RegisterAttached(
                "Handler",
                typeof(object),
                typeof(Message),
                null
                );

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(string),
                typeof(Message),
                new PropertyMetadata(OnAttachChanged)
                );

        public static void SetAttach(DependencyObject d, string attachText)
        {
            d.SetValue(AttachProperty, attachText);
        }

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