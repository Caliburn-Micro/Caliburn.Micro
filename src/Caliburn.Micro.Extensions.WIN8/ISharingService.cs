using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// Service that handles sharing data with the Share Charm.
    /// </summary>
    public interface ISharingService
    {
        /// <summary>
        /// Programmatically initiates the user interface for sharing content with another app.
        /// </summary>
        void ShowShareUI();
    }
}