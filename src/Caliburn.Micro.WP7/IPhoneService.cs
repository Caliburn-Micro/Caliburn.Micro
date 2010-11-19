namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// Implemented by services that provide access to the basic phone capabilities.
    /// </summary>
    public interface IPhoneService
    {
        /// <summary>
        /// The state that is persisted during the tombstoning process.
        /// </summary>
        IDictionary<string, object> State { get; }

        /// <summary>
        /// Gets the mode in which the application was started.
        /// </summary>
        StartupMode StartupMode { get; }

        /// <summary>
        /// Occurs when a fresh instance of the application is launching.
        /// </summary>
        event EventHandler<LaunchingEventArgs> Launching;

        /// <summary>
        /// Occurs when a previously tombstoned instance is resurrected.
        /// </summary>
        event EventHandler<ActivatedEventArgs> Activated;

        /// <summary>
        /// Occurs when the application is being tombstoned.
        /// </summary>
        event EventHandler<DeactivatedEventArgs> Deactivated;

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        event EventHandler<ClosingEventArgs> Closing;

        /// <summary>
        /// Gets or sets whether user idle detection is enabled.
        /// </summary>
        IdleDetectionMode UserIdleDetectionMode { get; set; }

        /// <summary>
        /// Gets or sets whether application idle detection is enabled.
        /// </summary>
        IdleDetectionMode ApplicationIdleDetectionMode { get; set; }
    }

    /// <summary>
    /// An implementation of <see cref="IPhoneService"/> that adapts <see cref="PhoneApplicationService"/>.
    /// </summary>
    public class PhoneApplicationServiceAdapter : IPhoneService
    {
        readonly PhoneApplicationService phoneService;

        /// <summary>
        /// Creates an instance of <see cref="PhoneApplicationServiceAdapter"/>.
        /// </summary>
        /// <param name="phoneService">The <see cref="PhoneApplicationService"/> to adapt.</param>
        public PhoneApplicationServiceAdapter(PhoneApplicationService phoneService)
        {
            this.phoneService = phoneService;
        }

        /// <summary>
        /// The state that is persisted during the tombstoning process.
        /// </summary>
        public IDictionary<string, object> State
        {
            get { return phoneService.State; }
        }

        /// <summary>
        /// Gets the mode in which the application was started.
        /// </summary>
        public StartupMode StartupMode
        {
            get { return phoneService.StartupMode; }
        }

        /// <summary>
        /// Occurs when a fresh instance of the application is launching.
        /// </summary>
        public event EventHandler<LaunchingEventArgs> Launching
        {
            add { phoneService.Launching += value; }
            remove { phoneService.Launching -= value; }
        }

        /// <summary>
        /// Occurs when a previously tombstoned application instance is resurrected.
        /// </summary>
        public event EventHandler<ActivatedEventArgs> Activated
        {
            add { phoneService.Activated += value; }
            remove { phoneService.Activated -= value; }
        }

        /// <summary>
        /// Occurs when the application is being tombstoned.
        /// </summary>
        public event EventHandler<DeactivatedEventArgs> Deactivated
        {
            add { phoneService.Deactivated += value; }
            remove { phoneService.Deactivated -= value; }
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        public event EventHandler<ClosingEventArgs> Closing
        {
            add { phoneService.Closing += value; }
            remove { phoneService.Closing -= value; }
        }

        /// <summary>
        /// Gets or sets whether user idle detection is enabled.
        /// </summary>
        public IdleDetectionMode UserIdleDetectionMode {
            get { return phoneService.UserIdleDetectionMode; }
            set { phoneService.UserIdleDetectionMode = value; }
        }

        /// <summary>
        /// Gets or sets whether application idle detection is enabled.
        /// </summary>
        public IdleDetectionMode ApplicationIdleDetectionMode {
            get { return phoneService.ApplicationIdleDetectionMode; }
            set { phoneService.ApplicationIdleDetectionMode = value; }
        }
    }
}