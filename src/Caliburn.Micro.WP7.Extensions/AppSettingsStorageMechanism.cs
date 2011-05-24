namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.IO.IsolatedStorage;

    public class AppSettingsStorageMechanism : IStorageMechanism {
        readonly IPhoneContainer container;
        List<string> keys;

        public AppSettingsStorageMechanism(IPhoneContainer container) {
            this.container = container;
        }

        public bool Supports(StorageMode mode) {
            return (mode & StorageMode.Permanent) == StorageMode.Permanent;
        }

        public void BeginStoring() {
            keys = new List<string>();
        }

        public void Store(string key, object data) {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(key)) {
                keys.Add(key);
            }

            IsolatedStorageSettings.ApplicationSettings[key] = data;
        }

        public void EndStoring() {
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public bool TryGet(string key, out object value) {
            return IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value);
        }

        public void Delete(string key) {
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }

        public void ClearLastSession() {
            if (keys != null) {
                keys.Apply(x => IsolatedStorageSettings.ApplicationSettings.Remove(x));
                keys = null;
            }
        }

        public void RegisterSingleton(Type service, string key, Type implementation) {
            container.RegisterWithAppSettings(service, key, implementation);
        }
    }
}