namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// Coordinates the saving and loading of objects based on application lifecycle events.
    /// </summary>
    public class StorageCoordinator {
        readonly List<IStorageHandler> handlers = new List<IStorageHandler>();
        readonly IPhoneContainer container;
        readonly IPhoneService phoneService;
        readonly List<IStorageMechanism> storageMechanisms;
        readonly List<WeakReference> tracked = new List<WeakReference>();
        StorageMode currentRestoreMode = StorageMode.Permanent;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageCoordinator"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="phoneService">The phone service.</param>
        /// <param name="storageMechanisms">The storage mechanisms.</param>
        /// <param name="handlers">The handlers.</param>
        public StorageCoordinator(IPhoneContainer container, IPhoneService phoneService, IEnumerable<IStorageMechanism> storageMechanisms, IEnumerable<IStorageHandler> handlers) {
            this.container = container;
            this.phoneService = phoneService;
            this.storageMechanisms = storageMechanisms.ToList();

            handlers.Apply(x => AddStorageHandler(x));

            phoneService.Resurrecting += () => currentRestoreMode = StorageMode.Any;
            phoneService.Continuing += () => storageMechanisms.Apply(x => x.ClearLastSession());
        }

        /// <summary>
        /// Starts monitoring application and container events.
        /// </summary>
        public void Start() {
            phoneService.Closing += OnClosing;
            phoneService.Deactivated += OnDeactivated;
            container.Activated += OnContainerActivated;
        }

        /// <summary>
        /// Stops monitoring application and container events.
        /// </summary>
        public void Stop() {
            phoneService.Closing -= OnClosing;
            phoneService.Deactivated -= OnDeactivated;
            container.Activated -= OnContainerActivated;
        }

        /// <summary>
        /// Gets the storage mechanism.
        /// </summary>
        /// <typeparam name="T">The type of storage mechanism to get.</typeparam>
        /// <returns>The storage mechanism.</returns>
        public T GetStorageMechanism<T>() where T : IStorageMechanism {
            return storageMechanisms.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Adds the storage mechanism.
        /// </summary>
        /// <param name="storageMechanism">The storage mechanism.</param>
        public void AddStorageMechanism(IStorageMechanism storageMechanism) {
            storageMechanisms.Add(storageMechanism);
        }

        /// <summary>
        /// Adds the storage handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>Itself</returns>
        public StorageCoordinator AddStorageHandler(IStorageHandler handler) {
            handler.Coordinator = this;
            handler.Configure();
            handlers.Add(handler);
            return this;
        }

        /// <summary>
        /// Gets the storage handler for a paricular instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The storage handler.</returns>
        public IStorageHandler GetStorageHandlerFor(object instance) {
            return handlers.FirstOrDefault(x => x.Handles(instance));
        }

        /// <summary>
        /// Saves all monitored instances according to the provided mode.
        /// </summary>
        /// <param name="saveMode">The save mode.</param>
        public void Save(StorageMode saveMode) {
            var toSave = tracked.Select(x => x.Target).Where(x => x != null);
            var mechanisms = storageMechanisms.Where(x => x.Supports(saveMode));

            mechanisms.Apply(x => x.BeginStoring());

            foreach(var item in toSave) {
                var handler = GetStorageHandlerFor(item);
                handler.Save(item, saveMode);
            }

            mechanisms.Apply(x => x.EndStoring());
        }

        /// <summary>
        /// Restores the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="restoreMode">The restore mode.</param>
        public void Restore(object instance, StorageMode restoreMode = StorageMode.Automatic) {
            var handler = GetStorageHandlerFor(instance);
            if (handler == null) {
                return;
            }

            tracked.Add(new WeakReference(instance));

            handler.Restore(instance, restoreMode == StorageMode.Automatic ? currentRestoreMode : restoreMode);
        }

        void OnDeactivated(object sender, DeactivatedEventArgs e) {
            Save(StorageMode.Any);
        }

        void OnClosing(object sender, ClosingEventArgs e) {
            Save(StorageMode.Permanent);
        }

        void OnContainerActivated(object instance) {
            Restore(instance);
        }
    }
}