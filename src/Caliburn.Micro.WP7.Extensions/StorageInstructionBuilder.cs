namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Used to create a fluent interface for building up a storage instruction.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StorageInstructionBuilder<T> {
        readonly StorageInstruction<T> storageInstruction;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageInstructionBuilder&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="storageInstruction">The storage instruction.</param>
        public StorageInstructionBuilder(StorageInstruction<T> storageInstruction) {
            this.storageInstruction = storageInstruction;
        }

        /// <summary>
        /// Configures the instruction with the specified behavior.
        /// </summary>
        /// <param name="configure">The configuration callback.</param>
        /// <returns>Itself</returns>
        public StorageInstructionBuilder<T> Configure(Action<StorageInstruction<T>> configure) {
            configure(storageInstruction);
            return this;
        }
    }
}