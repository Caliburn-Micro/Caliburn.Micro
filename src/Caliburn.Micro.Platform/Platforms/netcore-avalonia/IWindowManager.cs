using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// A service that manages windows.
    /// </summary>
    public interface IWindowManager
    {
        /// <summary>
        /// Shows a modal dialog for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The optional dialog settings.</param>
        Task<bool?> ShowDialogAsync(object rootModel, object context = null, IDictionary<string, object> settings = null);

        /// <summary>
        /// Shows a non-modal window for the specified model.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The context.</param>
        /// <param name="settings">The optional window settings.</param>
        Task ShowWindowAsync(object rootModel, object context = null, IDictionary<string, object> settings = null);

        /// <summary>
        /// Shows a popup at the current mouse position.
        /// </summary>
        /// <param name="rootModel">The root model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        Task ShowPopupAsync(object rootModel, object context = null, IDictionary<string, object> settings = null);

        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="rootModel">The view model.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The window.</returns>
        Task<Window> CreateWindowAsync(object rootModel, object context = null, IDictionary<string, object> settings = null);
    }
}
