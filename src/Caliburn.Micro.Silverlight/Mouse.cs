namespace Caliburn.Micro {
    using System.Windows;

    /// <summary>
    /// A mouse helper utility.
    /// </summary>
    public static class Mouse {
        /// <summary>
        /// The current position of the mouse.
        /// </summary>
        public static Point Position { get; set; }

        /// <summary>
        /// Initializes the mouse helper with the UIElement to use in mouse tracking.
        /// </summary>
        /// <param name="element">The UIElement to use for mouse tracking.</param>
        public static void Initialize(UIElement element) {
            element.MouseMove += (s, e) => { Position = e.GetPosition(null); };
        }
    }
}