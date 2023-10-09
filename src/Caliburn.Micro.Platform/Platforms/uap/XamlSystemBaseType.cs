using System;

using Windows.UI.Xaml.Markup;

namespace Caliburn.Micro {
    internal class XamlSystemBaseType : IXamlType {
        public XamlSystemBaseType(string fullName, Type underlyingType) {
            FullName = fullName;
            UnderlyingType = underlyingType;
        }

        public string FullName { get; }

        public Type UnderlyingType { get; }

        public virtual IXamlType BaseType
            => throw new NotImplementedException();

        public virtual IXamlMember ContentProperty
            => throw new NotImplementedException();

        public virtual bool IsArray
            => throw new NotImplementedException();

        public virtual bool IsCollection
            => throw new NotImplementedException();

        public virtual bool IsConstructible
            => throw new NotImplementedException();

        public virtual bool IsDictionary
            => throw new NotImplementedException();

        public virtual bool IsMarkupExtension
            => throw new NotImplementedException();

        public virtual bool IsBindable
            => throw new NotImplementedException();

        public virtual IXamlType ItemType
            => throw new NotImplementedException();

        public virtual IXamlType KeyType
            => throw new NotImplementedException();

        public virtual IXamlMember GetMember(string name)
            => throw new NotImplementedException();

        public virtual object ActivateInstance()
            => throw new NotImplementedException();

        public virtual void AddToMap(object instance, object key, object item)
            => throw new NotImplementedException();

        public virtual void AddToVector(object instance, object item)
            => throw new NotImplementedException();

        public virtual void RunInitializer()
            => throw new NotImplementedException();

        public virtual object CreateFromString(string input)
            => throw new NotImplementedException();
    }
}
