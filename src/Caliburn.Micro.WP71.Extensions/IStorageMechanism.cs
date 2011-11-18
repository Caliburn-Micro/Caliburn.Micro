namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Implemented by classes that know how to store data.
    /// </summary>
    public interface IStorageMechanism {
        /// <summary>
        /// Indicates what storage modes this mechanism provides.
        /// </summary>
        /// <param name="mode">The storage mode to check.</param>
        /// <returns>Whether or not it is supported.</returns>
        bool Supports(StorageMode mode);

        /// <summary>
        /// Begins the storage transaction.
        /// </summary>
        void BeginStoring();

        /// <summary>
        /// Stores the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        void Store(string key, object data);

        /// <summary>
        /// Ends the storage transaction.
        /// </summary>
        void EndStoring();

        /// <summary>
        /// Tries to get the data previously stored with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if found; false otherwise</returns>
        bool TryGet(string key, out object value);

        /// <summary>
        /// Deletes the data with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Delete(string key);

        /// <summary>
        /// Clears the data stored in the last storage transaction.
        /// </summary>
        void ClearLastSession();

        /// <summary>
        /// Registers service with the storage mechanism as a singleton.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="implementation">The implementation.</param>
        void RegisterSingleton(Type service, string key, Type implementation);
    }
}