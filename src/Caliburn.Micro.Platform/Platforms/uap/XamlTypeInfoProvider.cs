using System;
using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace Caliburn.Micro {
    internal sealed class XamlTypeInfoProvider {
        private readonly Dictionary<string, IXamlType> _xamlTypes = new Dictionary<string, IXamlType>();
        private readonly Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();
        private readonly Dictionary<Type, string> _xamlTypeToStandardName = new Dictionary<Type, string>();

        public IXamlType GetXamlTypeByType(Type type)
            => _xamlTypeToStandardName.TryGetValue(type, out string standardName)
                ? GetXamlTypeByName(standardName)
                : GetXamlTypeByName(type.FullName);

        public IXamlType GetXamlTypeByName(string typeName) {
            if (string.IsNullOrEmpty(typeName)) {
                return null;
            }

            if (_xamlTypes.TryGetValue(typeName, out IXamlType xamlType)) {
                return xamlType;
            }

            xamlType = CreateXamlType(typeName);
            if (xamlType != null) {
                _xamlTypes.Add(typeName, xamlType);
            }

            return xamlType;
        }

        public IXamlMember GetMemberByLongName(string longMemberName) {
            if (string.IsNullOrEmpty(longMemberName)) {
                return null;
            }

            if (_xamlMembers.TryGetValue(longMemberName, out IXamlMember xamlMember)) {
                return xamlMember;
            }

            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null) {
                _xamlMembers.Add(longMemberName, xamlMember);
            }

            return xamlMember;
        }

        private void AddToMapOfTypeToStandardName(Type t, string str) {
            if (!_xamlTypeToStandardName.ContainsKey(t)) {
                _xamlTypeToStandardName.Add(t, str);
            }
        }

        private IXamlType CreateXamlType(string typeName) {
            XamlSystemBaseType xamlType = null;
            XamlUserType userType;

            switch (typeName) {
                case "Object":
                    xamlType = new XamlSystemBaseType(typeName, typeof(object));
                    break;
                /*
                case "Caliburn.Micro.Message":
                    userType = new XamlUserType(this, typeName, typeof(Caliburn.Micro.Message), GetXamlTypeByName("Object"));
                    userType.AddMemberName("Handler");
                    AddToMapOfTypeToStandardName(typeof(System.Object), "Object");
                    xamlType = userType;
                    break;
                */
                case "Caliburn.Micro.View":
                    userType = new XamlUserType(this, typeName, typeof(View), GetXamlTypeByName("Object"));
                    userType.AddMemberName("Model");
                    AddToMapOfTypeToStandardName(typeof(object), "Object");
                    xamlType = userType;
                    break;
            }

            return xamlType;
        }

        private IXamlMember CreateXamlMember(string longMemberName) {
            XamlMember xamlMember = null;
            XamlUserType userType;

            switch (longMemberName) {
                case "Caliburn.Micro.View.Model":
                    userType = (XamlUserType)GetXamlTypeByName("Caliburn.Micro.View");
                    xamlMember = new XamlMember(this, "Model", "Object");
                    xamlMember.SetTargetTypeName("Windows.UI.Xaml.DependencyObject");
                    xamlMember.SetIsAttachable();
                    xamlMember.Getter = instance => View.GetModel((DependencyObject)instance);
                    xamlMember.Setter = (instance, value) => View.SetModel((DependencyObject)instance, value);
                    break;
            }

            return xamlMember;
        }
    }
}
