namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StorageCoordinator {
        readonly List<IStorageHandler> handlers = new List<IStorageHandler>();
        readonly List<WeakReference> tracked = new List<WeakReference>();

        public void TryAddTrackedInstance(object instance) {
            if(FindHandler(instance) != null) {
                tracked.Add(new WeakReference(instance));
            }
        }

        public void AddStorageMechanism(IStorageHandler handler) {
            handlers.Add(handler);
        }

        public void Save(StorageMode mode) {
            var alive = tracked.Select(x => x.Target).Where(x => x != null);

            foreach(var instance in alive) {
                var mechanism = FindHandler(instance);
                mechanism.Save(instance, mode);
            }
        }

        public void TryRestore(object instance) {
            var handler = FindHandler(instance);
            if (handler == null)
                return;

            tracked.Add(new WeakReference(instance));
            handler.Restore(instance);
        }

        IStorageHandler FindHandler(object instance) {
            return handlers.FirstOrDefault(x => x.Handles(instance));
        }
    }
}