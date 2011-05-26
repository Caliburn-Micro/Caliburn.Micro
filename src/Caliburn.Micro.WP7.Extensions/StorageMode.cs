namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// The mode used to save/restore data.
    /// </summary>
    [Flags]
    public enum StorageMode {
        /// <summary>
        /// Automatic Determine the Mode
        /// </summary>
        Automatic = 0,
        /// <summary>
        /// Use Temporary storage.
        /// </summary>
        Temporary = 2,
        /// <summary>
        /// Use Permenent storage.
        /// </summary>
        Permanent = 4,
        /// <summary>
        /// Use any storage mechanism available.
        /// </summary>
        Any = Temporary | Permanent
    }
}