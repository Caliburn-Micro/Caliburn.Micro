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

        public T GetStorageMechanism<T>() where T : IStorageMechanism {
            var mechanism = storageMechanisms.OfType<T>().FirstOrDefault();

            if(mechanism == null) {
                mechanism = Activator.CreateInstance<T>();
                storageMechanisms.Add(mechanism);
            }

            return mechanism;
        }

        public void AddStorageMechanism(IStorageMechanism storageMechanism) {
            storageMechanisms.Add(storageMechanism);
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