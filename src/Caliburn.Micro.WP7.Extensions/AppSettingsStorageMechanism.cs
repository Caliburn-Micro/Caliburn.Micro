namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.IO.IsolatedStorage;

    /// <summary>
    /// Stores data in the application settings.
    /// </summary>
    public class AppSettingsStorageMechanism : IStorageMechanism {
        readonly IPhoneContainer container;
        List<string> keys;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsStorageMechanism"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AppSettingsStorageMechanism(IPhoneContainer container) {
            this.container = container;
        }

        /// <summary>
        /// Indicates what storage modes this mechanism provides.
        /// </summary>
        /// <param name="mode">The storage mode to check.</param>
        /// <returns>
        /// Whether or not it is supported.
        /// </returns>
        public bool Supports(StorageMode mode) {
            return (mode & StorageMode.Permanent) == StorageMode.Permanent;
        }

        /// <summary>
        /// Begins the storage transaction.
        /// </summary>
        public void BeginStoring() {
            keys = new List<string>();
        }

        /// <summary>
        /// Stores the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        public void Store(string key, object data) {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(key)) {
                keys.Add(key);
            }

            IsolatedStorageSettings.ApplicationSettings[key] = data;
        }

        /// <summary>
        /// Ends the storage transaction.
        /// </summary>
        public void EndStoring() {
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public bool TryGet(string key, out object value) {
            return IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value);
        }

        /// <summary>
        /// Deletes the data with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Delete(string key) {
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }

        /// <summary>
        /// Clears the data stored in the last storage transaction.
        /// </summary>
        public void ClearLastSession() {
            if (keys != null) {
                keys.Apply(x => IsolatedStorageSettings.ApplicationSettings.Remove(x));
                keys = null;
            }
        }

        /// <summary>
        /// Registers service with the storage mechanism as a singleton.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterSingleton(Type service, string key, Type implementation) {
            container.RegisterWithAppSettings(service, key, implementation);
        }
    }
}