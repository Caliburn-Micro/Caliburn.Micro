namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public static class TypeDescriptor
    {
        static readonly Dictionary<Type, TypeConverter> Cache = new Dictionary<Type, TypeConverter>();

        public static TypeConverter GetConverter(Type type)
        {
            TypeConverter converter;

            if(!Cache.TryGetValue(type, out converter))
            {
                var customAttributes = type.GetCustomAttributes(typeof(TypeConverterAttribute), true);

                if(customAttributes.Length == 0)
                    return new TypeConverter();

                converter = CreateConverter(((TypeConverterAttribute)customAttributes[0]).ConverterTypeName);
                Cache[type] = converter;
            }

            return converter;
        }

        static TypeConverter CreateConverter(string converterTypeName)
        {
            return (Activator.CreateInstance(Type.GetType(converterTypeName)) as TypeConverter);
        }
    }
}