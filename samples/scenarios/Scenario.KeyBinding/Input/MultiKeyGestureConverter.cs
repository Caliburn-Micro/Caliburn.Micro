using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace Scenario.KeyBinding.Input
{
    /// <summary>
    ///   Class used to define a converter for the <see cref="MultiKeyGesture" /> class.
    /// </summary>
    /// <remarks>
    ///   At the moment it is able to convert strings like <c>Alt+K,R</c> in proper multi-key gestures.
    /// </remarks>
    public class MultiKeyGestureConverter : TypeConverter
    {
        /// <summary>
        ///   The default instance of the converter.
        /// </summary>
        public static readonly MultiKeyGestureConverter DefaultConverter = new MultiKeyGestureConverter();

        /// <summary>
        ///   The inner key converter.
        /// </summary>
        static readonly KeyConverter keyConverter = new KeyConverter();

        /// <summary>
        ///   The inner modifier key converter.
        /// </summary>
        static readonly ModifierKeysConverter modifierKeysConverter = new ModifierKeysConverter();

        /// <summary>
        ///   Tries to get the modifier equivalent to the specified string.
        /// </summary>
        /// <param name="str"> The string. </param>
        /// <param name="modifier"> The modifier. </param>
        /// <returns> <c>True</c> if a valid modifier was found; otherwise, <c>false</c> . </returns>
        static bool TryGetModifierKeys(string str, out ModifierKeys modifier)
        {
            switch (str.ToUpper())
            {
                case "CONTROL":
                case "CTRL":
                    modifier = ModifierKeys.Control;
                    return true;
                case "SHIFT":
                    modifier = ModifierKeys.Shift;
                    return true;
                case "ALT":
                    modifier = ModifierKeys.Alt;
                    return true;
                case "WINDOWS":
                case "WIN":
                    modifier = ModifierKeys.Windows;
                    return true;
                default:
                    modifier = ModifierKeys.None;
                    return false;
            }
        }

        /// <summary>
        ///   Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="sourceType"> A <see cref="System.Type" /> that represents the type you want to convert from. </param>
        /// <returns> true if this converter can perform the conversion; otherwise, false. </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        ///   Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="culture"> The <see cref="System.Globalization.CultureInfo" /> to use as the current culture. </param>
        /// <param name="value"> The <see cref="object" /> to convert. </param>
        /// <returns> An <see cref="object" /> that represents the converted value. </returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = (value as string);

            if (!string.IsNullOrEmpty(str))
            {
                var sequences = str.Split(',');
                string[] keyStrings;

                var keySequences = new List<KeySequence>();


                foreach (var sequence in sequences)
                {
                    var modifier = ModifierKeys.None;
                    var keys = new List<Key>();
                    keyStrings = sequence.Split('+');
                    var modifiersCount = 0;

                    ModifierKeys currentModifier;
                    string temp;
                    while ((temp = keyStrings[modifiersCount]) != null && TryGetModifierKeys(temp.Trim(), out currentModifier))
                    {
                        modifiersCount++;
                        modifier |= currentModifier;
                    }

                    for (var i = modifiersCount; i < keyStrings.Length; i++)
                    {
                        var keyString = keyStrings[i];
                        if (keyString != null)
                        {
                            var key = (Key)keyConverter.ConvertFrom(keyString.Trim());
                            keys.Add(key);
                        }
                    }

                    keySequences.Add(new KeySequence(modifier, keys.ToArray()));
                }

                return new MultiKeyGesture(str, keySequences.ToArray());
            }

            throw GetConvertFromException(value);
        }

        /// <summary>
        ///   Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="culture"> A <see cref="System.Globalization.CultureInfo" /> . If null is passed, the current culture is assumed. </param>
        /// <param name="value"> The <see cref="object" /> to convert. </param>
        /// <param name="destinationType"> The <see cref="System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
        /// <returns> An <see cref="object" /> that represents the converted value. </returns>
        /// <exception cref="System.ArgumentNullException">The
        ///   <paramref name="destinationType" />
        ///   parameter is null.</exception>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var gesture = value as MultiKeyGesture;

                if (gesture != null)
                {
                    var builder = new StringBuilder();

                    KeySequence sequence;

                    for (var i = 0; i < gesture.KeySequences.Length; i++)
                    {
                        if (i > 0)
                            builder.Append(", ");

                        sequence = gesture.KeySequences[i];
                        if (sequence.Modifiers != ModifierKeys.None)
                        {
                            builder.Append((string)modifierKeysConverter.ConvertTo(context, culture, sequence.Modifiers, destinationType));
                            builder.Append("+");
                        }

                        builder.Append((string)keyConverter.ConvertTo(context, culture, sequence.Keys[0], destinationType));

                        for (var j = 1; j < sequence.Keys.Length; j++)
                        {
                            builder.Append("+");
                            builder.Append((string)keyConverter.ConvertTo(context, culture, sequence.Keys[0], destinationType));
                        }
                    }

                    return builder.ToString();
                }
            }

            throw GetConvertToException(value, destinationType);
        }
    }
}