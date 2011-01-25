namespace Caliburn.Micro {
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
    public static class MessageBinder {
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
        /// Determines the parameters that a method should be invoked with.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        /// <param name="requiredParameters">The parameters required to complete the invocation.</param>
        /// <returns>The actual parameter values.</returns>
        public static object[] DetermineParameters(ActionExecutionContext context, ParameterInfo[] requiredParameters) {
            var providedValues = context.Message.Parameters.Select(x => x.Value).ToArray();
            var finalValues = new object[requiredParameters.Length];

            for(int i = 0; i < requiredParameters.Length; i++) {
                var parameterType = requiredParameters[i].ParameterType;
                var parameterValue = providedValues[i];
                var parameterAsString = parameterValue as string;

                if(parameterAsString != null)
                    finalValues[i] = CoerceValue(parameterType, EvaluateParameter(parameterAsString, parameterType, context));
                else finalValues[i] = CoerceValue(parameterType, parameterValue);
            }

            return finalValues;
        }

        /// <summary>
        /// Transforms the textual parameter into the actual parameter.
        /// </summary>
        public static Func<string, Type, ActionExecutionContext, object> EvaluateParameter = (text, parameterType, context) => {
            var lookup = text.ToLower(CultureInfo.InvariantCulture);
            Func<ActionExecutionContext, object> resolver;

            if(SpecialValues.TryGetValue(lookup, out resolver))
                return resolver(context);
            return text;
        };

        /// <summary>
        /// Coerces the provided value to the destination type.
        /// </summary>
        /// <param name="destinationType">The destination type.</param>
        /// <param name="providedValue">The provided value.</param>
        /// <returns>The coerced value.</returns>
        public static object CoerceValue(Type destinationType, object providedValue) {
            if(providedValue == null)
                return GetDefaultValue(destinationType);

            var providedType = providedValue.GetType();

            if(destinationType.IsAssignableFrom(providedType))
                return providedValue;

            try {
                var converter = TypeDescriptor.GetConverter(destinationType);

                if(converter.CanConvertFrom(providedType))
                    return converter.ConvertFrom(providedValue);

                converter = TypeDescriptor.GetConverter(providedType);

                if(converter.CanConvertTo(destinationType))
                    return converter.ConvertTo(providedValue, destinationType);
            }
            catch {
                return GetDefaultValue(destinationType);
            }

            try {
                return Convert.ChangeType(providedValue, destinationType, CultureInfo.CurrentUICulture);
            }
            catch {
                return GetDefaultValue(destinationType);
            }
        }

        /// <summary>
        /// Gets the default value for a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default value.</returns>
        public static object GetDefaultValue(Type type) {
            return type.IsClass || type.IsInterface ? null : Activator.CreateInstance(type);
        }
    }
}