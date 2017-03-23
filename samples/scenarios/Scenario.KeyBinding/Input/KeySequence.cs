using System;
using System.Text;
using System.Windows.Input;

namespace Scenario.KeyBinding.Input
{
    /// <summary>
    ///   Class used to store multi-key gesture data.
    /// </summary>
    public class KeySequence
    {
        /// <summary>
        ///   Gets the sequence of keys.
        /// </summary>
        /// <value> The sequence of keys. </value>
        public Key[] Keys { get; }

        /// <summary>
        ///   Gets the modifiers to be applied to the sequence.
        /// </summary>
        /// <value> The modifiers to be applied to the sequence. </value>
        public ModifierKeys Modifiers { get; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="KeySequence" /> class.
        /// </summary>
        public KeySequence(ModifierKeys modifiers, params Key[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));

            if (keys.Length < 1)
                throw new ArgumentException(@"At least 1 key should be provided", nameof(keys));

            Keys = new Key[keys.Length];
            keys.CopyTo(Keys, 0);
            Modifiers = modifiers;
        }

        /// <summary>
        ///   Returns a <see cref="string" /> that represents the current <see cref="object" /> .
        /// </summary>
        /// <returns> A <see cref="string" /> that represents the current <see cref="object" /> . </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Modifiers != ModifierKeys.None)
            {
                if ((Modifiers & ModifierKeys.Control) != ModifierKeys.None)
                    builder.Append("Ctrl+");
                if ((Modifiers & ModifierKeys.Alt) != ModifierKeys.None)
                    builder.Append("Alt+");
                if ((Modifiers & ModifierKeys.Shift) != ModifierKeys.None)
                    builder.Append("Shift+");
                if ((Modifiers & ModifierKeys.Windows) != ModifierKeys.None)
                    builder.Append("Windows+");
            }

            builder.Append(Keys[0]);

            for (var i = 1; i < Keys.Length; i++)
            {
                builder.Append("+" + Keys[i]);
            }

            return builder.ToString();
        }
    }
}