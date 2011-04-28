namespace Caliburn.Micro {
    using System;

    public class InstructionBuilder<T> {
        readonly Instruction<T> instruction;

        public InstructionBuilder(Instruction<T> instruction) {
            this.instruction = instruction;
        }

        public InstructionBuilder<T> Configure(Action<Instruction<T>> configure) {
            configure(instruction);
            return this;
        }
    }
}