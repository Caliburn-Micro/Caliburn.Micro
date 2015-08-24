namespace Caliburn.Micro {
    using System.Collections.Generic;

    /// <summary>
    /// The settings window manager.
    /// </summary>
    public interface ISettingsWindowManager {

#if WinRT81 
        /// <summary>
        /// Shows a settings flyout panel for the specified model.
        /// </summary>
        /// <param name="viewModel">The settings view model.</param>
        /// <param name="commandLabel">The settings command label.</param>
        /// <param name="viewSettings">The optional dialog settings.</param> 
        /// <param name="independent">Whether to show the settings flyout as an independent one.</param> 
        void ShowSettingsFlyout(object viewModel, string commandLabel, IDictionary<string, object> viewSettings = null, bool independent = false);
        
#else
        /// <summary>
        /// Shows a settings flyout panel for the specified model.
        /// </summary>
        /// <param name="viewModel">The settings view model.</param>
        /// <param name="commandLabel">The settings command label.</param>
        /// <param name="viewSettings">The optional dialog settings.</param> 
        void ShowSettingsFlyout(object viewModel, string commandLabel, IDictionary<string, object> viewSettings = null);
#endif
                                            }
}
