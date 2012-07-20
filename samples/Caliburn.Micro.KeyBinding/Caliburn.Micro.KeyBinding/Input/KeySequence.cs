namespace Caliburn.Micro.KeyBinding.Input {
    using System;
    using System.Text;
    using System.Windows.Input;

    /// <summary>
    ///   Class used to store multi-key gesture data.
    /// </summary>
    public class KeySequence {
        /// <summary>
        ///   The sequence of keys.
        /// </summary>
        readonly Key[] keys;

        /// <summary>
        ///   The key modifiers to be applied to the sequence.
        /// </summary>
        readonly ModifierKeys modifiers;

        /// <summary>
        ///   Gets the sequence of keys.
        /// </summary>
        /// <value> The sequence of keys. </value>
        public Key[] Keys {
            get { return keys; }
        }

        /// <summary>
        ///   Gets the modifiers to be applied to the sequence.
        /// </summary>
        /// <value> The modifiers to be applied to the sequence. </value>
        public ModifierKeys Modifiers {
            get { return modifiers; }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="KeySequence" /> class.
        /// </summary>
        public KeySequence(ModifierKeys modifiers, params Key[] keys) {
            if(keys == null)
                throw new ArgumentNullException("keys");
            if(keys.Length < 1)
                throw new ArgumentException("At least 1 key should be provided", "keys");

            this.keys = new Key[keys.Length];
            keys.CopyTo(this.keys, 0);
            this.modifiers = modifiers;
        }

        /// <summary>
        ///   Returns a <see cref="string" /> that represents the current <see cref="object" /> .
        /// </summary>
        /// <returns> A <see cref="string" /> that represents the current <see cref="object" /> . </returns>
        public override string ToString() {
            var builder = new StringBuilder();

            if(modifiers != ModifierKeys.None) {
                if((modifiers & ModifierKeys.Control) != ModifierKeys.None)
                    builder.Append("Ctrl+");
                if((modifiers & ModifierKeys.Alt) != ModifierKeys.None)
                    builder.Append("Alt+");
                if((modifiers & ModifierKeys.Shift) != ModifierKeys.None)
                    builder.Append("Shift+");
                if((modifiers & ModifierKeys.Windows) != ModifierKeys.None)
                    builder.Append("Windows+");
            }

            builder.Append(keys[0]);
            for(var i = 1; i < keys.Length; i++)
                builder.Append("+" + keys[i]);

            return builder.ToString();
        }
    }
}