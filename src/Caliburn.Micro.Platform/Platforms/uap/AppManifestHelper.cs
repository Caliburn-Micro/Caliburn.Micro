//
// Copyright (c) 2012 Tim Heuer
//
// Licensed under the Microsoft Public License (Ms-PL) (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Caliburn.Micro
{
    internal class AppManifestHelper
    {
        public async static Task<VisualElement> GetManifestVisualElementsAsync()
        {
            // the path for the manifest
            XDocument xmldoc;
            using (Stream manifestStream = await Windows.ApplicationModel.Package.Current.InstalledLocation.OpenStreamForReadAsync("AppxManifest.xml"))
            {
                xmldoc = XDocument.Load(manifestStream);
            }

            // set the XNamespace and name for the VisualElements node we want
            var xn = XName.Get("VisualElements", "http://schemas.microsoft.com/appx/2013/manifest");

            // parse the VisualElements node only, pulling out what we need
            // NOTE: This will get only the first Application (which is the mainstream case)
            // TODO: Need to take into account that DisplayName/Description may be localized using ms-resource:{foo}
            var visualElementNode = (from vel in xmldoc.Descendants(xn)
                                     select new VisualElement
                                     {
                                         DisplayName = vel.Attribute("DisplayName").Value,
                                         Description = vel.Attribute("Description").Value,
                                         LogoUri = new Uri(string.Format("ms-appx:///{0}", vel.Attribute("Square150x150Logo").Value.Replace(@"\", @"/"))),
                                         SmallLogoUri = new Uri(string.Format("ms-appx:///{0}", vel.Attribute("Square30x30Logo").Value.Replace(@"\", @"/"))),
                                         BackgroundColorAsString = vel.Attribute("BackgroundColor").Value
                                     }).FirstOrDefault();

            if (visualElementNode == null)
                throw new ArgumentNullException("Could not parse the VisualElements from the app manifest.");

            return visualElementNode;
        }

    }

    internal class VisualElement
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Uri LogoUri { get; set; }
        public Uri SmallLogoUri { get; set; }
        public string BackgroundColorAsString { get; set; }

        public Windows.UI.Color BackgroundColor
        {
            get
            {
                return ToColor(BackgroundColorAsString);
            }
        }

        private static Windows.UI.Color ToColor(string hexValue)
        {
            // if 'transparent' is entered in the app manifest, return Windows.UI.Colors.Transparent
            // in order to prevent parsing failures
            if (string.Equals(hexValue, "transparent", StringComparison.OrdinalIgnoreCase))
#if WinUI3
                return Microsoft.UI.Colors.Transparent;
#else
                return Windows.UI.Colors.Transparent;
#endif


            hexValue = hexValue.Replace("#", string.Empty);

            // some loose validation (not bullet-proof)
            if (hexValue.Length < 6)
            {
                throw new ArgumentException("This does not appear to be a proper hex color number");
            }

            bool isEightCharactersLong = hexValue.Length == 8;
            // if the string is 8 characters it includes the a part
            int startPosition = isEightCharactersLong ? 2 : 0;

            // the case where alpha is provided

            byte a = isEightCharactersLong ? byte.Parse(hexValue.Substring(0, 2), NumberStyles.HexNumber) : (byte)255;

            byte r = byte.Parse(hexValue.Substring(startPosition, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hexValue.Substring(startPosition + 2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hexValue.Substring(startPosition + 4, 2), NumberStyles.HexNumber);

            return Windows.UI.Color.FromArgb(a, r, g, b);
        }
    }
}
