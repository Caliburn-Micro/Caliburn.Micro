namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StorageCoordinator {
        readonly List<IStorageHandler> handlers = new List<IStorageHandler>();
        readonly List<IStorageMechanism> storageMechanisms = new List<IStorageMechanism>();
        readonly List<WeakReference> tracked = new List<WeakReference>();

        public void TrackInstance(object instance) {
            if(GetStorageHandlerFor(instance) != null) {
                tracked.Add(new WeakReference(instance));
            }
        }

        public T GetStorageMechanism<T>() {
            return storageMechanisms.OfType<T>().First();
        }

        public void AddStorageMechanism(IStorageMechanism storageMechanism) {
            storageMechanisms.Add(storageMechanism);
        }

        public void AddStorageHandler(IStorageHandler handler) {
            handlers.Add(handler);
        }

        public IStorageHandler GetStorageHandlerFor(object instance) {
            return handlers.FirstOrDefault(x => x.Handles(instance));
        }

        public void Save(StorageMode mode) {
            var toSave = tracked.Select(x => x.Target).Where(x => x != null);
            var mechanisms = storageMechanisms.Where(x => x.Supports(mode));

            mechanisms.Apply(x => x.Begin());

            foreach(var item in toSave) {
                var handler = GetStorageHandlerFor(item);
                handler.Save(item, mode);
            }

            mechanisms.Apply(x => x.End());
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