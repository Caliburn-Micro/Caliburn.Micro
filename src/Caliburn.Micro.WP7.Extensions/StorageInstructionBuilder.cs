namespace Caliburn.Micro {
    using System;

    public class StorageInstructionBuilder<T> {
        readonly StorageInstruction<T> storageInstruction;

        public StorageInstructionBuilder(StorageInstruction<T> storageInstruction) {
            this.storageInstruction = storageInstruction;
        }

        public StorageInstructionBuilder<T> Configure(Action<StorageInstruction<T>> configure) {
            configure(storageInstruction);
            return this;
        }
    }
}