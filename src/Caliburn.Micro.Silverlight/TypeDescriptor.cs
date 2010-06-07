namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public static class TypeDescriptor
    {
        static readonly Dictionary<Type, TypeConverter> Cache = new Dictionary<Type, TypeConverter>();

        public static TypeConverter GetConverter(Type type)
        {
            TypeConverter converter;

            if(!Cache.TryGetValue(type, out converter))
            {
                var customAttributes = type.GetAttributes<TypeConverterAttribute>(true);

                if(!customAttributes.Any())
                    return new TypeConverter();

                converter = Activator.CreateInstance(Type.GetType(customAttributes.First().ConverterTypeName)) as TypeConverter;
                Cache[type] = converter;
            }

            return converter;
        }
    }
}