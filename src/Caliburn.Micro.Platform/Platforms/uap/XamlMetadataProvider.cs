using System;

using Windows.UI.Xaml.Markup;

namespace Caliburn.Micro {
    /// <summary>
    /// Implements XAML schema context concepts that support XAML parsing.
    /// </summary>
    public class XamlMetadataProvider : IXamlMetadataProvider {
        private XamlTypeInfoProvider provider;

        /// <summary>
        /// Gets the set of XMLNS (XAML namespace) definitions that apply to the context.
        /// </summary>
        /// <returns>The set of XMLNS (XAML namespace) definitions.</returns>
        public XmlnsDefinition[] GetXmlnsDefinitions()
            => Array.Empty<XmlnsDefinition>();

        /// <summary>
        /// Implements XAML schema context access to underlying type mapping, based on providing a helper value that describes a type.
        /// </summary>
        /// <param name="type">The type as represented by the relevant type system or interop support type.</param>
        /// <returns>The schema context's implementation of the <see cref="IXamlType"/> concept.</returns>
        public IXamlType GetXamlType(Type type) {
            provider ??= new XamlTypeInfoProvider();

            return provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// Implements XAML schema context access to underlying type mapping, based on specifying a full type name.
        /// </summary>
        /// <param name="type">The name of the class for which to return a XAML type mapping.</param>
        /// <returns>The schema context's implementation of the IXamlType concept.</returns>
        public IXamlType GetXamlType(string type) {
            provider ??= new XamlTypeInfoProvider();

            return provider.GetXamlTypeByName(type);
        }
    }
}
