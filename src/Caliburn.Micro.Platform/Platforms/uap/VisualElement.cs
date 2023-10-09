using System;
using System.Globalization;

/*
 Copyright (c) 2012 Tim Heuer

 Licensed under the Microsoft Public License (Ms-PL) (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at

    http://opensource.org/licenses/Ms-PL.html

 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
*/

namespace Caliburn.Micro {
    internal sealed class VisualElement {
        public Windows.UI.Color BackgroundColor
            => ToColor(BackgroundColorAsString);

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Uri LogoUri { get; set; }

        public Uri SmallLogoUri { get; set; }

        public string BackgroundColorAsString { get; set; }

        private static Windows.UI.Color ToColor(string hexValue) {
            // if 'transparent' is entered in the app manifest, return Windows.UI.Colors.Transparent
            // in order to prevent parsing failures
            if (string.Equals(hexValue, "transparent", StringComparison.OrdinalIgnoreCase)) {
                return Windows.UI.Colors.Transparent;
            }

            hexValue = hexValue.Replace("#", string.Empty);

            // some loose validation (not bullet-proof)
            if (hexValue.Length < 6) {
                throw new ArgumentException("This does not appear to be a proper hex color number");
            }

            byte a = 255;
            int startPosition = 0;

            // the case where alpha is provided
            if (hexValue.Length == 8) {
                a = byte.Parse(hexValue.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                startPosition = 2;
            }

            byte r = byte.Parse(hexValue.Substring(startPosition, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte g = byte.Parse(hexValue.Substring(startPosition + 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte b = byte.Parse(hexValue.Substring(startPosition + 4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            return Windows.UI.Color.FromArgb(a, r, g, b);
        }
    }
}
