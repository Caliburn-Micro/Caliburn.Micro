namespace Caliburn.Micro {
    /// <summary>
    /// Handles the storage of an object instance.
    /// </summary>
    public interface IStorageHandler {
        /// <summary>
        /// Gets or sets the coordinator.
        /// </summary>
        /// <value>
        /// The coordinator.
        /// </value>
        StorageCoordinator Coordinator { get; set; }

        /// <summary>
        /// Overrided by inheritors to configure the handler for use.
        /// </summary>
        void Configure();

        /// <summary>
        /// Indicates whether the specified instance can be stored by this handler.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        bool Handles(object instance);

        /// <summary>
        /// Saves the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="mode">The mode.</param>
        void Save(object instance, StorageMode mode);

        /// <summary>
        /// Restores the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="mode">The mode.</param>
        void Restore(object instance, StorageMode mode);
    }
}