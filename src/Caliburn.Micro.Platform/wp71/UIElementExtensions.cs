using System;
using System.Windows;
using System.Windows.Media;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension methods for <see cref="System.Windows.UIElement"/>
    /// </summary>
    public static class UIElementExtensions {
        static readonly ILog Log = LogManager.GetLog(typeof(UIElementExtensions));

        /// <summary>
        /// Calls TransformToVisual on the specified element for the specified visual, suppressing the ArgumentException that can occur in some cases.
        /// </summary>
        /// <param name="element">Element on which to call TransformToVisual.</param>
        /// <param name="visual">Visual to pass to the call to TransformToVisual.</param>
        /// <returns>Resulting GeneralTransform object.</returns>
        public static GeneralTransform SafeTransformToVisual(this UIElement element, UIElement visual)
        {
            GeneralTransform result;
            try {
                result = element.TransformToVisual(visual);
            } catch (ArgumentException ex) {
                Log.Error(ex);

                // Not perfect, but better than throwing an exception
                result = new TranslateTransform();
            }
            return result;
        }
    }
}
