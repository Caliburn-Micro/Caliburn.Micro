namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Phone.Shell;

    public class StorageCoordinator {
        readonly List<IStorageHandler> handlers = new List<IStorageHandler>();
        readonly IPhoneContainer container;
        readonly IPhoneService phoneService;
        readonly IEnumerable<IStorageMechanism> storageMechanisms;
        readonly List<WeakReference> tracked = new List<WeakReference>();

        public StorageCoordinator(IPhoneContainer container, IPhoneService phoneService, IEnumerable<IStorageMechanism> storageMechanisms, IEnumerable<IStorageHandler> handlers) {
            this.container = container;
            this.phoneService = phoneService;
            this.storageMechanisms = storageMechanisms;

            handlers.Apply(x => AddStorageHandler(x));
        }

        public void Start() {
            phoneService.Closing += OnClosing;
            phoneService.Deactivated += OnDeactivated;
            container.Activated += Restore;
        }

        public void Stop() {
            phoneService.Closing -= OnClosing;
            phoneService.Deactivated -= OnDeactivated;
            container.Activated -= Restore;
        }

        public T GetStorageMechanism<T>() where T : IStorageMechanism {
            return storageMechanisms.OfType<T>().FirstOrDefault();
        }

        public StorageCoordinator AddStorageHandler(IStorageHandler handler) {
            handler.Coordinator = this;
            handler.Configure();
            handlers.Add(handler);
            return this;
        }

        public IStorageHandler GetStorageHandlerFor(object instance) {
            return handlers.FirstOrDefault(x => x.Handles(instance));
        }

        public void Save(StorageMode mode) {
            var toSave = tracked.Select(x => x.Target).Where(x => x != null);
            var mechanisms = storageMechanisms.Where(x => x.Supports(mode));

            mechanisms.Apply(x => x.BeginStoring());

            foreach(var item in toSave) {
                var handler = GetStorageHandlerFor(item);
                handler.Save(item, mode);
            }

            mechanisms.Apply(x => x.EndStoring());
        }

        public void Restore(object instance) {
            var handler = GetStorageHandlerFor(instance);
            if(handler == null)
                return;

            tracked.Add(new WeakReference(instance));

            //this isn't quite right...
            handler.Restore(instance, phoneService.StartupMode == StartupMode.Activate ? StorageMode.Temporary : StorageMode.Permanent);
        }


        void OnDeactivated(object sender, DeactivatedEventArgs e) {
            Save(StorageMode.Temporary);
        }

        void OnClosing(object sender, ClosingEventArgs e) {
            Save(StorageMode.Permanent);
        }
    }
}