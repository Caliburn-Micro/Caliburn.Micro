namespace Caliburn.Micro
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

#if !SILVERLIGHT
    using System.ComponentModel;
#endif

    /// <summary>
    /// A service that is capable of properly binding values to a method's parameters and creating instances of <see cref="IResult"/>.
    /// </summary>
    public static class MessageBinder
    {
        /// <summary>
        /// The special parameter values recognized by the message binder along with their resolvers.
        /// </summary>
        public static readonly Dictionary<string, Func<ActionExecutionContext, object>> SpecialValues =
            new Dictionary<string, Func<ActionExecutionContext, object>>{
                { "$eventargs", c => c.EventArgs },
                { "$datacontext", c => c.Source.DataContext },
                { "$source", c => c.Source },
                { "$executioncontext", c => c },
                { "$view", c => c.View }
            };

        /// <summary>
        /// Custom converters used by the framework registered by destination type for which they will be selected.
        /// The converter is passed the existing value to convert and a "context" object.
        /// </summary>
        public static readonly Dictionary<Type, Func<object, object, object>> CustomConverters =
            new Dictionary<Type, Func<object, object, object>> {
                {
                    typeof(DateTime), (value, context) => {
                        DateTime result;
                        DateTime.TryParse(value.ToString(), out result);
                        return result;
                    }
                }
            };

        /// <summary>
        /// Determines the parameters that a method should be invoked with.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        /// <param name="requiredParameters">The parameters required to complete the invocation.</param>
        /// <returns>The actual parameter values.</returns>
        public static object[] DetermineParameters(ActionExecutionContext context, ParameterInfo[] requiredParameters)
        {
            var providedValues = context.Message.Parameters.Select(x => x.Value).ToArray();
            var finalValues = new object[requiredParameters.Length];

            for (int i = 0; i < requiredParameters.Length; i++)
            {
                var parameterType = requiredParameters[i].ParameterType;
                var parameterValue = providedValues[i];
                var parameterAsString = parameterValue as string;

                if (parameterAsString != null)
                    finalValues[i] = CoerceValue(parameterType, EvaluateParameter(parameterAsString, parameterType, context), context);
                else finalValues[i] = CoerceValue(parameterType, parameterValue, context);
            }

            return finalValues;
        }

        /// <summary>
        /// Transforms the textual parameter into the actual parameter.
        /// </summary>
        public static Func<string, Type, ActionExecutionContext, object> EvaluateParameter = (text, parameterType, context) =>
        {
#if WinRT
            var lookup = text.ToLower();
#else
            var lookup = text.ToLower(CultureInfo.InvariantCulture);
#endif
            Func<ActionExecutionContext, object> resolver;
            return SpecialValues.TryGetValue(lookup, out resolver) ? resolver(context) : text;
        };

        /// <summary>
        /// Coerces the provided value to the destination type.
        /// </summary>
        /// <param name="destinationType">The destination type.</param>
        /// <param name="providedValue">The provided value.</param>
        /// <param name="context">An optional context value which can be used during conversion.</param>
        /// <returns>The coerced value.</returns>
        public static object CoerceValue(Type destinationType, object providedValue, object context)
        {
            if (providedValue == null)
            {
                return GetDefaultValue(destinationType);
            }

            var providedType = providedValue.GetType();
            if (destinationType.IsAssignableFrom(providedType))
            {
                return providedValue;
            }

            if (CustomConverters.ContainsKey(destinationType))
            {
                return CustomConverters[destinationType](providedValue, context);
            }

            try
            {
#if !WinRT
                var converter = TypeDescriptor.GetConverter(destinationType);

                if (converter.CanConvertFrom(providedType)) {
                    return converter.ConvertFrom(providedValue);
                }

                converter = TypeDescriptor.GetConverter(providedType);

                if (converter.CanConvertTo(destinationType)) {
                    return converter.ConvertTo(providedValue, destinationType);
                }
#endif
#if WinRT
                if (destinationType.GetTypeInfo().IsEnum)
                {
#else
                if (destinationType.IsEnum) {
#endif
                    var stringValue = providedValue as string;
                    if (stringValue != null)
                    {
                        return Enum.Parse(destinationType, stringValue, true);
                    }

                    return Enum.ToObject(destinationType, providedValue);
                }

                if (typeof(Guid).IsAssignableFrom(destinationType))
                {
                    var stringValue = providedValue as string;
                    if (stringValue != null)
                    {
                        return new Guid(stringValue);
                    }
                }
            }
            catch
            {
                return GetDefaultValue(destinationType);
            }

            try
            {
                return Convert.ChangeType(providedValue, destinationType, CultureInfo.CurrentUICulture);
            }
            catch
            {
                return GetDefaultValue(destinationType);
            }
        }

        /// <summary>
        /// Gets the default value for a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default value.</returns>
#if WinRT
        public static object GetDefaultValue(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass || typeInfo.IsInterface ? null : System.Activator.CreateInstance(type);
        }
#else
        public static object GetDefaultValue(Type type) {
            return type.IsClass || type.IsInterface ? null : Activator.CreateInstance(type);
        }
#endif
    }
}