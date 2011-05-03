namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StorageCoordinator {
        readonly List<IStorageHandler> handlers = new List<IStorageHandler>();
        readonly IEnumerable<IStorageMechanism> storageMechanisms;
        readonly List<WeakReference> tracked = new List<WeakReference>();

        public StorageCoordinator(IEnumerable<IStorageMechanism> storageMechanisms) {
            this.storageMechanisms = storageMechanisms;
        }

        public T GetStorageMechanism<T>() where T : IStorageMechanism {
            return storageMechanisms.OfType<T>().FirstOrDefault();
        }

        public StorageCoordinator AddStorageHandler(IStorageHandler handler) {
            handler.Coordinator = this;
            handlers.Add(handler);
            return this;
        }

        public StorageCoordinator AddStorageHandler<T>(Action<StorageHandler<T>> configure) {
            var handler = new StorageHandler<T>();
            AddStorageHandler(handler);

            configure(handler);
            return this;
        }

        public IStorageHandler GetStorageHandlerFor(object instance) {
            return handlers.FirstOrDefault(x => x.Handles(instance));
        }

        public void Save(StorageMode mode) {
            var toSave = tracked.Select(x => x.Target).Where(x => x != null);
            var mechanisms = storageMechanisms.Where(x => x.Supports(mode));

            mechanisms.Apply(x => x.BeginStore());

            foreach(var item in toSave) {
                var handler = GetStorageHandlerFor(item);
                handler.Save(item, mode);
            }

            mechanisms.Apply(x => x.EndStore());
        }

        public void Restore(object instance) {
            var handler = GetStorageHandlerFor(instance);
            if(handler == null)
                return;

            tracked.Add(new WeakReference(instance));
            handler.Restore(instance);
        }
    }
}