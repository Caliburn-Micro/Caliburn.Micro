#if SILVERLIGHT
namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Provides information about the characteristics for a component, such as its attributes, properties, and events. This class cannot be inherited.
    /// </summary>
    public static class TypeDescriptor {
        static readonly Dictionary<Type, TypeConverter> Cache = new Dictionary<Type, TypeConverter>();

        /// <summary>
        /// Returns a type converter for the specified type.
        /// </summary>
        /// <param name="type">The System.Type of the target component.</param>
        /// <returns>A System.ComponentModel.TypeConverter for the specified type.</returns>
        public static TypeConverter GetConverter(Type type) {
            TypeConverter converter;

            if(!Cache.TryGetValue(type, out converter)) {
                var customAttributes = type.GetAttributes<TypeConverterAttribute>(true);

                if (!customAttributes.Any()) {
                    return new TypeConverter();
                }

                converter = Activator.CreateInstance(Type.GetType(customAttributes.First().ConverterTypeName)) as TypeConverter;
                Cache[type] = converter;
            }

            return converter;
        }
    }
}
#endif