using System;

using Windows.UI.Xaml.Markup;

namespace Caliburn.Micro {
    internal sealed class XamlMember : IXamlMember {
        private readonly XamlTypeInfoProvider _provider;
        private readonly string _typeName;
        private string _targetTypeName;

        public XamlMember(XamlTypeInfoProvider provider, string name, string typeName) {
            Name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get; }

        public IXamlType Type
            => _provider.GetXamlTypeByName(_typeName);

        public IXamlType TargetType
            => _provider.GetXamlTypeByName(_targetTypeName);

        public bool IsAttachable { get; private set; }

        public bool IsDependencyProperty { get; private set; }

        public bool IsReadOnly { get; private set; }

        public Getter Getter { get; set; }

        public Setter Setter { get; set; }

        public void SetTargetTypeName(string targetTypeName)
            => _targetTypeName = targetTypeName;

        public void SetIsAttachable()
            => IsAttachable = true;

        public void SetIsDependencyProperty()
            => IsDependencyProperty = true;

        public void SetIsReadOnly()
            => IsReadOnly = true;

        public object GetValue(object instance)
            => Getter == null
                ? throw new InvalidOperationException(nameof(GetValue))
                : Getter(instance);

        public void SetValue(object instance, object value) {
            if (Setter == null) {
                throw new InvalidOperationException(nameof(SetValue));
            }

            Setter(instance, value);
        }
    }
}
