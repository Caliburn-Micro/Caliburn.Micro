namespace Caliburn.Micro {
    /// <summary>
    /// Denotes an object that can be closed.
    /// </summary>
    public interface IClose {
        /// <summary>
        /// Tries to close this instance.
        /// </summary>
        void TryClose();
    }
}
