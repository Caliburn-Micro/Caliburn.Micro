namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Defines an interface through which the storage system can communicate with an IoC container.
    /// </summary>
    public interface IPhoneContainer {
        /// <summary>
        /// Occurs when a new instance is created.
        /// </summary>
        event Action<object> Activated;

        /// <summary>
        /// Registers the service as a singleton stored in the phone state.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="phoneStateKey">The phone state key.</param>
        /// <param name="implementation">The implementation.</param>
        void RegisterWithPhoneService(Type service, string phoneStateKey, Type implementation);

        /// <summary>
        /// Registers the service as a singleton stored in the app settings.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="appSettingsKey">The app settings key.</param>
        /// <param name="implementation">The implementation.</param>
        void RegisterWithAppSettings(Type service, string appSettingsKey, Type implementation);
    }
}