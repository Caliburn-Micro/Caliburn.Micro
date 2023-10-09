using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    internal sealed class AppManifestHelper {
        public static async Task<VisualElement> GetManifestVisualElementsAsync() {
            // the path for the manifest
            XDocument xmldoc;
            using (Stream manifestStream = await Windows.ApplicationModel.Package.Current.InstalledLocation.OpenStreamForReadAsync("AppxManifest.xml")) {
                xmldoc = XDocument.Load(manifestStream);
            }

            // set the XNamespace and name for the VisualElements node we want
            var xn = XName.Get("VisualElements", "http://schemas.microsoft.com/appx/2013/manifest");

            // parse the VisualElements node only, pulling out what we need
            // NOTE: This will get only the first Application (which is the mainstream case)
            // TODO: Need to take into account that DisplayName/Description may be localized using ms-resource:{foo}
            VisualElement visualElementNode
                = (from vel in xmldoc.Descendants(xn)
                   select new VisualElement {
                       DisplayName = vel.Attribute("DisplayName").Value,
                       Description = vel.Attribute("Description").Value,
                       LogoUri = new Uri(string.Format(CultureInfo.InvariantCulture, "ms-appx:///{0}", vel.Attribute("Square150x150Logo").Value.Replace(@"\", @"/"))),
                       SmallLogoUri = new Uri(string.Format(CultureInfo.InvariantCulture, "ms-appx:///{0}", vel.Attribute("Square30x30Logo").Value.Replace(@"\", @"/"))),
                       BackgroundColorAsString = vel.Attribute("BackgroundColor").Value,
                   }).FirstOrDefault();

            return visualElementNode
                ?? throw new ArgumentNullException("Could not parse the VisualElements from the app manifest.");
        }
    }
}
