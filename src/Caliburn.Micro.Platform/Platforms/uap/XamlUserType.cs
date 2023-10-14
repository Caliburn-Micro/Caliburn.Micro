using System;
using System.Collections.Generic;
using System.Globalization;

using Windows.UI.Xaml.Markup;

namespace Caliburn.Micro {
    internal sealed class XamlUserType : XamlSystemBaseType {
        private readonly XamlTypeInfoProvider _provider;
        private readonly IXamlType _baseType;
        private bool _isArray;
        private bool _isMarkupExtension;
        private bool _isBindable;

        private string _contentPropertyName;
        private string _itemTypeName;
        private string _keyTypeName;
        private Dictionary<string, string> _memberNames;
        private Dictionary<string, object> _enumValues;

        public XamlUserType(XamlTypeInfoProvider provider, string fullName, Type fullType, IXamlType baseType)
            : base(fullName, fullType) {
            _provider = provider;
            _baseType = baseType;
        }

        public Activator Activator { get; set; }

        public AddToCollection CollectionAdd { get; set; }

        public AddToDictionary DictionaryAdd { get; set; }

        public override IXamlType BaseType
            => _baseType;

        public override bool IsArray
            => _isArray;

        public override bool IsCollection
            => CollectionAdd != null;

        public override bool IsConstructible
            => Activator != null;

        public override bool IsDictionary
            => DictionaryAdd != null;

        public override bool IsMarkupExtension
            => _isMarkupExtension;

        public override bool IsBindable
            => _isBindable;

        public override IXamlMember ContentProperty
            => _provider.GetMemberByLongName(_contentPropertyName);

        public override IXamlType ItemType
            => _provider.GetXamlTypeByName(_itemTypeName);

        public override IXamlType KeyType
            => _provider.GetXamlTypeByName(_keyTypeName);

        public override IXamlMember GetMember(string name)
            => _memberNames == null
                ? null
                : _memberNames.TryGetValue(name, out string longName)
                    ? _provider.GetMemberByLongName(longName)
                    : null;

        public override object ActivateInstance()
            => Activator();

        public override void AddToMap(object instance, object key, object item)
            => DictionaryAdd(instance, key, item);

        public override void AddToVector(object instance, object item)
            => CollectionAdd(instance, item);

        public override void RunInitializer()
            => System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);

        public void SetContentPropertyName(string contentPropertyName)
            => _contentPropertyName = contentPropertyName;

        public void SetIsArray()
            => _isArray = true;

        public void SetIsMarkupExtension()
            => _isMarkupExtension = true;

        public void SetIsBindable()
            => _isBindable = true;

        public void SetItemTypeName(string itemTypeName)
            => _itemTypeName = itemTypeName;

        public void SetKeyTypeName(string keyTypeName)
            => _keyTypeName = keyTypeName;

        public override object CreateFromString(string input) {
            if (_enumValues == null) {
                throw new ArgumentException(input, FullName);
            }

            int value = 0;
            string[] valueParts = input.Split(',');
            foreach (string valuePart in valueParts) {
                int enumFieldValue = 0;
                try {
                    if (_enumValues.TryGetValue(valuePart.Trim(), out object partValue)) {
                        enumFieldValue = Convert.ToInt32(partValue, CultureInfo.InvariantCulture);
                    } else {
                        try {
                            enumFieldValue = Convert.ToInt32(valuePart.Trim(), CultureInfo.InvariantCulture);
                        } catch (FormatException) {
                            foreach (string key in _enumValues.Keys) {
                                if (string.Equals(valuePart.Trim(), key, StringComparison.OrdinalIgnoreCase)) {
                                    if (_enumValues.TryGetValue(key.Trim(), out partValue)) {
                                        enumFieldValue = Convert.ToInt32(partValue, CultureInfo.InvariantCulture);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    value |= enumFieldValue;
                } catch (FormatException) {
                    throw new ArgumentException(input, FullName);
                }
            }

            return value;
        }

        public void AddMemberName(string shortName) {
            _memberNames ??= new Dictionary<string, string>();
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value) {
            _enumValues ??= new Dictionary<string, object>();
            _enumValues.Add(name, value);
        }
    }
}
