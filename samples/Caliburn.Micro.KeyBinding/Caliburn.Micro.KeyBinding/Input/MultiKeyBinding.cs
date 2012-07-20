namespace Caliburn.Micro.KeyBinding.Input {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    ///   Class used to define a
    /// </summary>
    public class MultiKeyBinding : InputBinding {
        /// <summary>
        ///   Gets the multi-key gesture.
        /// </summary>
        /// <value> The multi-key gesture. </value>
        MultiKeyGesture MultiKeyGesture {
            get { return base.Gesture as MultiKeyGesture; }
        }

        /// <summary>
        ///   Gets or sets the key sequences.
        /// </summary>
        /// <value> The key sequences. </value>
        public IEnumerable<KeySequence> KeySequences {
            get { return MultiKeyGesture.KeySequences; }
            set { base.Gesture = new MultiKeyGesture(value.ToArray()); }
        }

        /// <summary>
        ///   Gets or sets the <see cref="System.Windows.Input.InputGesture" /> associated with this input binding.
        /// </summary>
        /// <value> </value>
        /// <returns> The associated gesture. The default is null. </returns>
        [TypeConverter(typeof(MultiKeyGestureConverter))]
        public override InputGesture Gesture {
            get { return base.Gesture; }
            set {
                if(!(value is MultiKeyGesture))
                    throw new ArgumentException("The type " + value.GetType() + " is not a valid " + typeof(MultiKeyGesture) + " type", "value");

                base.Gesture = value;
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="MultiKeyBinding" /> class.
        /// </summary>
        public MultiKeyBinding() {
            base.Gesture = new MultiKeyGesture(new KeySequence(ModifierKeys.None, Key.None));
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="MultiKeyBinding" /> class.
        /// </summary>
        /// <param name="command"> The command. </param>
        /// <param name="gesture"> The gesture. </param>
        public MultiKeyBinding(ICommand command, MultiKeyGesture gesture)
            : base(command, gesture) {
            base.Gesture = new MultiKeyGesture(new KeySequence(ModifierKeys.None, Key.None));
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="MultiKeyBinding" /> class.
        /// </summary>
        /// <param name="command"> The command. </param>
        /// <param name="sequences"> The key sequences. </param>
        public MultiKeyBinding(ICommand command, params KeySequence[] sequences)
            : base(command, new MultiKeyGesture(sequences)) {
            base.Gesture = new MultiKeyGesture(new KeySequence(ModifierKeys.None, Key.None));
        }
    }
}