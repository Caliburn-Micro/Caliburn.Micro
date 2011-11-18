namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Shell;

    /// <summary>
    ///   Implemented by services that provide access to the basic phone capabilities.
    /// </summary>
    public interface IPhoneService {
        /// <summary>
        ///   The state that is persisted during the tombstoning process.
        /// </summary>
        IDictionary<string, object> State { get; }

        /// <summary>
        ///   Gets the mode in which the application was started.
        /// </summary>
        StartupMode StartupMode { get; }

        /// <summary>
        ///   Occurs when a fresh instance of the application is launching.
        /// </summary>
        event EventHandler<LaunchingEventArgs> Launching;

        /// <summary>
        ///   Occurs when a previously paused/tombstoned app is resumed/resurrected.
        /// </summary>
        event EventHandler<ActivatedEventArgs> Activated;

        /// <summary>
        ///   Occurs when the application is being paused or tombstoned.
        /// </summary>
        event EventHandler<DeactivatedEventArgs> Deactivated;

        /// <summary>
        ///   Occurs when the application is closing.
        /// </summary>
        event EventHandler<ClosingEventArgs> Closing;

        /// <summary>
        ///   Occurs when the app is continuing from a temporarily paused state.
        /// </summary>
        event System.Action Continuing;

        /// <summary>
        ///   Occurs after the app has continued from a temporarily paused state.
        /// </summary>
        event System.Action Continued;

        /// <summary>
        ///   Occurs when the app is "resurrecting" from a tombstoned state.
        /// </summary>
        event System.Action Resurrecting;

        /// <summary>
        ///   Occurs after the app has "resurrected" from a tombstoned state.
        /// </summary>
        event System.Action Resurrected;

        /// <summary>
        ///   Gets or sets whether user idle detection is enabled.
        /// </summary>
        IdleDetectionMode UserIdleDetectionMode { get; set; }

        /// <summary>
        ///   Gets or sets whether application idle detection is enabled.
        /// </summary>
        IdleDetectionMode ApplicationIdleDetectionMode { get; set; }

        /// <summary>
        /// Gets if the app is currently resurrecting.
        /// </summary>
        bool IsResurrecting { get; }
    }

    /// <summary>
    ///   An implementation of <see cref = "IPhoneService" /> that adapts <see cref = "PhoneApplicationService" />.
    /// </summary>
    public class PhoneApplicationServiceAdapter : IPhoneService {
        readonly PhoneApplicationService service;

        /// <summary>
        ///   Creates an instance of <see cref = "PhoneApplicationServiceAdapter" />.
        /// </summary>
        public PhoneApplicationServiceAdapter(Frame rootFrame) {
            service = PhoneApplicationService.Current;
            service.Activated += (sender, args) => {
                if(!args.IsApplicationInstancePreserved) {
                    IsResurrecting = true;
                    Resurrecting();
                    NavigatedEventHandler onNavigated = null;
                    onNavigated = (s2, e2) => {
                        IsResurrecting = false;
                        Resurrected();
                        rootFrame.Navigated -= onNavigated;
                    };
                    rootFrame.Navigated += onNavigated;
                }
                else {
                    Continuing();
                    NavigatedEventHandler onNavigated = null;
                    onNavigated = (s2, e2) => {
                        Continued();
                        rootFrame.Navigated -= onNavigated;
                    };
                    rootFrame.Navigated += onNavigated;
                }
            };
        }

        /// <summary>
        /// Gets if the app is currently resurrecting.
        /// </summary>
        public bool IsResurrecting { get; private set; }

        /// <summary>
        ///   The state that is persisted during the tombstoning process.
        /// </summary>
        public IDictionary<string, object> State {
            get { return service.State; }
        }

        /// <summary>
        ///   Gets the mode in which the application was started.
        /// </summary>
        public StartupMode StartupMode {
            get { return service.StartupMode; }
        }

        /// <summary>
        ///   Occurs when a fresh instance of the application is launching.
        /// </summary>
        public event EventHandler<LaunchingEventArgs> Launching {
            add { service.Launching += value; }
            remove { service.Launching -= value; }
        }

        /// <summary>
        ///   Occurs when a previously paused/tombstoned application instance is resumed/resurrected.
        /// </summary>
        public event EventHandler<ActivatedEventArgs> Activated {
            add { service.Activated += value; }
            remove { service.Activated -= value; }
        }

        /// <summary>
        ///   Occurs when the application is being paused or tombstoned.
        /// </summary>
        public event EventHandler<DeactivatedEventArgs> Deactivated {
            add { service.Deactivated += value; }
            remove { service.Deactivated -= value; }
        }

        /// <summary>
        ///   Occurs when the application is closing.
        /// </summary>
        public event EventHandler<ClosingEventArgs> Closing {
            add { service.Closing += value; }
            remove { service.Closing -= value; }
        }

        /// <summary>
        ///   Occurs when the app is continuing from a temporarily paused state.
        /// </summary>
        public event System.Action Continuing = delegate { };

        /// <summary>
        ///   Occurs after the app has continued from a temporarily paused state.
        /// </summary>
        public event System.Action Continued = delegate { };

        /// <summary>
        ///   Occurs when the app is "resurrecting" from a tombstoned state.
        /// </summary>
        public event System.Action Resurrecting = delegate { };

        /// <summary>
        ///   Occurs after the app has "resurrected" from a tombstoned state.
        /// </summary>
        public event System.Action Resurrected = delegate { };

        /// <summary>
        ///   Gets or sets whether user idle detection is enabled.
        /// </summary>
        public IdleDetectionMode UserIdleDetectionMode {
            get { return service.UserIdleDetectionMode; }
            set { service.UserIdleDetectionMode = value; }
        }

        /// <summary>
        ///   Gets or sets whether application idle detection is enabled.
        /// </summary>
        public IdleDetectionMode ApplicationIdleDetectionMode {
            get { return service.ApplicationIdleDetectionMode; }
            set { service.ApplicationIdleDetectionMode = value; }
        }
    }
}