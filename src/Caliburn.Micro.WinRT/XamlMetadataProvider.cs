using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml;

namespace Caliburn.Micro
{
    public class XamlMetadataProvider : IXamlMetadataProvider
    {
        private XamlTypeInfoProvider _provider;

        public IXamlType GetXamlType(Type type)
        {
            if (_provider == null)
            {
                _provider = new XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        public IXamlType GetXamlType(String typeName)
        {
            if (_provider == null)
            {
                _provider = new XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(typeName);
        }

        public XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new XmlnsDefinition[0];
        }
    }

    internal class XamlTypeInfoProvider
    {
        public IXamlType GetXamlTypeByType(Type type)
        {
            string standardName;
            IXamlType xamlType = null;
            if (_xamlTypeToStandardName.TryGetValue(type, out standardName))
            {
                xamlType = GetXamlTypeByName(standardName);
            }
            else
            {
                xamlType = GetXamlTypeByName(type.FullName);
            }
            return xamlType;
        }

        public IXamlType GetXamlTypeByName(string typeName)
        {
            if (String.IsNullOrEmpty(typeName))
            {
                return null;
            }
            IXamlType xamlType;
            if (_xamlTypes.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            xamlType = CreateXamlType(typeName);
            if (xamlType != null)
            {
                _xamlTypes.Add(typeName, xamlType);
            }
            return xamlType;
        }

        public IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (String.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        Dictionary<string, IXamlType> _xamlTypes = new Dictionary<string, IXamlType>();
        Dictionary<string, IXamlMember> _xamlMembers = new Dictionary<string, IXamlMember>();
        Dictionary<Type, string> _xamlTypeToStandardName = new Dictionary<Type, string>();

        private void AddToMapOfTypeToStandardName(Type t, String str)
        {
            if (!_xamlTypeToStandardName.ContainsKey(t))
            {
                _xamlTypeToStandardName.Add(t, str);
            }
        }

        private IXamlType CreateXamlType(string typeName)
        {
            XamlSystemBaseType xamlType = null;
            XamlUserType userType;

            switch (typeName)
            {
                case "Object":
                    xamlType = new XamlSystemBaseType(typeName, typeof(System.Object));
                    break;

                //case "Caliburn.Micro.Message":
                //    userType = new XamlUserType(this, typeName, typeof(Caliburn.Micro.Message), GetXamlTypeByName("Object"));
                //    userType.AddMemberName("Handler");
                //    AddToMapOfTypeToStandardName(typeof(System.Object),
                //                                       "Object");
                //    xamlType = userType;
                //    break;

                case "Caliburn.Micro.View":
                    userType = new XamlUserType(this, typeName, typeof(Caliburn.Micro.View), GetXamlTypeByName("Object"));
                    userType.AddMemberName("Model");
                    AddToMapOfTypeToStandardName(typeof(Object), "Object");
                    xamlType = userType;
                    break;

            }
            return xamlType;
        }

        private object get_1_View_Model(object instance)
        {
            return Caliburn.Micro.View.GetModel((Windows.UI.Xaml.DependencyObject)instance);
        }

        private void set_1_View_Model(object instance, object Value)
        {
            Caliburn.Micro.View.SetModel((Windows.UI.Xaml.DependencyObject)instance, (System.Object)Value);
        }

        private IXamlMember CreateXamlMember(string longMemberName)
        {
            XamlMember xamlMember = null;
            XamlUserType userType;

            switch (longMemberName)
            {
                case "Caliburn.Micro.View.Model":
                    userType = (XamlUserType)GetXamlTypeByName("Caliburn.Micro.View");
                    xamlMember = new XamlMember(this, "Model", "Object");
                    xamlMember.SetTargetTypeName("Windows.UI.Xaml.DependencyObject");
                    xamlMember.SetIsAttachable();
                    xamlMember.Getter = get_1_View_Model;
                    xamlMember.Setter = set_1_View_Model;
                    break;
            }
            return xamlMember;
        }

    }


    internal class XamlSystemBaseType : IXamlType
    {
        string _fullName;
        Type _underlyingType;

        public XamlSystemBaseType(string fullName, Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        public virtual IXamlType BaseType { get { throw new NotImplementedException(); } }
        public virtual IXamlMember ContentProperty { get { throw new NotImplementedException(); } }
        public virtual IXamlMember GetMember(string name) { throw new NotImplementedException(); }
        public virtual bool IsArray { get { throw new NotImplementedException(); } }
        public virtual bool IsCollection { get { throw new NotImplementedException(); } }
        public virtual bool IsConstructible { get { throw new NotImplementedException(); } }
        public virtual bool IsDictionary { get { throw new NotImplementedException(); } }
        public virtual bool IsMarkupExtension { get { throw new NotImplementedException(); } }
        public virtual bool IsBindable { get { throw new NotImplementedException(); } }
        public virtual IXamlType ItemType { get { throw new NotImplementedException(); } }
        public virtual IXamlType KeyType { get { throw new NotImplementedException(); } }
        public virtual object ActivateInstance() { throw new NotImplementedException(); }
        public virtual void AddToMap(object instance, object key, object item) { throw new NotImplementedException(); }
        public virtual void AddToVector(object instance, object item) { throw new NotImplementedException(); }
        public virtual void RunInitializer() { throw new NotImplementedException(); }
        public virtual object CreateFromString(String input) { throw new NotImplementedException(); }
    }

    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    internal class XamlUserType : XamlSystemBaseType
    {
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
            : base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        public override IXamlType BaseType { get { return _baseType; } }
        public override bool IsArray { get { return _isArray; } }
        public override bool IsCollection { get { return (CollectionAdd != null); } }
        public override bool IsConstructible { get { return (Activator != null); } }
        public override bool IsDictionary { get { return (DictionaryAdd != null); } }
        public override bool IsMarkupExtension { get { return _isMarkupExtension; } }
        public override bool IsBindable { get { return _isBindable; } }

        public override IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        public override IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        public override IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        public override IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        public override object ActivateInstance()
        {
            return Activator();
        }

        public override void AddToMap(object instance, object key, object item)
        {
            DictionaryAdd(instance, key, item);
        }

        public override void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        public override void RunInitializer()
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        public override object CreateFromString(String input)
        {
            if (_enumValues != null)
            {
                Int32 value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts)
                {
                    object partValue;
                    Int32 enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = Convert.ToInt32(valuePart.Trim());
                            }
                            catch (FormatException)
                            {
                                foreach (string key in _enumValues.Keys)
                                {
                                    if (String.Compare(valuePart.Trim(), key, System.StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        if (_enumValues.TryGetValue(key.Trim(), out partValue))
                                        {
                                            enumFieldValue = Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue;
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException(input, FullName);
                    }
                }

                return value;
            }
            throw new ArgumentException(input, FullName);
        }

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true;
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if (_memberNames == null)
            {
                _memberNames = new Dictionary<string, string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    internal class XamlMember : IXamlMember
    {
        private readonly XamlTypeInfoProvider _provider;
        private readonly string _name;
        private bool _isAttachable;
        private bool _isDependencyProperty;
        private bool _isReadOnly;

        private readonly string _typeName;
        private string _targetTypeName;

        public XamlMember(XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(String targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }

        public IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable()
        {
            _isAttachable = true;
        }

        public bool IsAttachable
        {
            get
            {
                return _isAttachable;
            }
        }

        public void SetIsDependencyProperty()
        {
            _isDependencyProperty = true;
        }

        public bool IsDependencyProperty
        {
            get
            {
                return _isDependencyProperty;
            }
        }

        public void SetIsReadOnly()
        {
            _isReadOnly = true;
        }

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
        }

        public Getter Getter
        {
            get;
            set;
        }

        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new InvalidOperationException("GetValue");
        }

        public Setter Setter
        {
            get;
            set;
        }

        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new InvalidOperationException("SetValue");
        }
    }
}


