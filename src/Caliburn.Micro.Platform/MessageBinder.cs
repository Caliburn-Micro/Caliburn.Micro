using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;

#else
using System.ComponentModel;
#endif

#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    /// A service that is capable of properly binding values to a method's parameters and creating instances of <see cref="IResult"/>.
    /// </summary>
    public static class MessageBinder {
        /// <summary>
        /// The special parameter values recognized by the message binder along with their resolvers.
        /// Parameter names are case insensitive so the specified names are unique and can be used with different case variations.
        /// </summary>
        public static readonly Dictionary<string, Func<ActionExecutionContext, object>> SpecialValues =
            new Dictionary<string, Func<ActionExecutionContext, object>>(StringComparer.OrdinalIgnoreCase)
            {
                { "$eventargs", c => c.EventArgs },
#if XFORMS || MAUI
                { "$datacontext", c => c.Source.BindingContext },
                { "$bindingcontext", c => c.Source.BindingContext },
#else
                { "$datacontext", c => c.Source.DataContext },
#endif
#if WINDOWS_UWP
                { "$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem },
#endif
                { "$source", c => c.Source },
                { "$executioncontext", c => c },
                { "$view", c => c.View },
            };

        /// <summary>
        /// Custom converters used by the framework registered by destination type for which they will be selected.
        /// The converter is passed the existing value to convert and a "context" object.
        /// </summary>
        public static readonly Dictionary<Type, Func<object, object, object>> CustomConverters =
            new Dictionary<Type, Func<object, object, object>>
            {
                {
                    typeof(DateTime), (value, context) => {
                        DateTime.TryParse(value.ToString(), out DateTime result);

                        return result;
                    }
                },
            };

        /// <summary>
        /// Gets or sets func to transforms the textual parameter into the actual parameter.
        /// </summary>
        public static Func<string, Type, ActionExecutionContext, object> EvaluateParameter { get; set; }
            = (text, parameterType, context)
                => SpecialValues.TryGetValue(text, out Func<ActionExecutionContext, object> resolver) ? resolver(context) : text;

        /// <summary>
        /// Determines the parameters that a method should be invoked with.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        /// <param name="requiredParameters">The parameters required to complete the invocation.</param>
        /// <returns>The actual parameter values.</returns>
        public static object[] DetermineParameters(ActionExecutionContext context, ParameterInfo[] requiredParameters) {
            object[] providedValues = context.Message.Parameters.OfType<Parameter>().Select(x => x.Value).ToArray();
            object[] finalValues = new object[requiredParameters.Length];

            for (int i = 0; i < requiredParameters.Length; i++) {
                Type parameterType = requiredParameters[i].ParameterType;
                object parameterValue = providedValues[i];

                finalValues[i] = parameterValue is string parameterAsString
                    ? CoerceValue(parameterType, EvaluateParameter(parameterAsString, parameterType, context), context)
                    : CoerceValue(parameterType, parameterValue, context);
            }

            return finalValues;
        }

        /// <summary>
        /// Coerces the provided value to the destination type.
        /// </summary>
        /// <param name="destinationType">The destination type.</param>
        /// <param name="providedValue">The provided value.</param>
        /// <param name="context">An optional context value which can be used during conversion.</param>
        /// <returns>The coerced value.</returns>
        public static object CoerceValue(Type destinationType, object providedValue, object context) {
            if (providedValue == null) {
                return GetDefaultValue(destinationType);
            }

            Type providedType = providedValue.GetType();
            if (destinationType.GetTypeInfo().IsAssignableFrom(providedType.GetTypeInfo())) {
                return providedValue;
            }

            if (CustomConverters.TryGetValue(destinationType, out Func<object, object, object> valueFunc)) {
                return valueFunc(providedValue, context);
            }

            try {
#if !WINDOWS_UWP && !XFORMS && !MAUI
                TypeConverter converter = TypeDescriptor.GetConverter(destinationType);

                if (converter.CanConvertFrom(providedType)) {
                    return converter.ConvertFrom(providedValue);
                }

                converter = TypeDescriptor.GetConverter(providedType);

                if (converter.CanConvertTo(destinationType)) {
                    return converter.ConvertTo(providedValue, destinationType);
                }
#endif
#if WINDOWS_UWP || XFORMS || MAUI
                if (destinationType.GetTypeInfo().IsEnum) {
#else
                if (destinationType.IsEnum) {
#endif
                    return providedValue is string stringValueLocal
                        ? Enum.Parse(destinationType, stringValueLocal, true)
                        : Enum.ToObject(destinationType, providedValue);
                }

                if (typeof(Guid).GetTypeInfo().IsAssignableFrom(destinationType.GetTypeInfo()) &&
                    providedValue is string stringValue) {
                    return new Guid(stringValue);
                }
            } catch {
                return GetDefaultValue(destinationType);
            }

            try {
                return Convert.ChangeType(providedValue, destinationType, CultureInfo.CurrentCulture);
            } catch {
                return GetDefaultValue(destinationType);
            }
        }

        /// <summary>
        /// Gets the default value for a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default value.</returns>
        public static object GetDefaultValue(Type type) {
#if WINDOWS_UWP || XFORMS || MAUI
            TypeInfo typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass || typeInfo.IsInterface
                ? null
                : System.Activator.CreateInstance(type);
#else
            _ = string.Empty;

            return type.IsClass || type.IsInterface
                ? null
                : System.Activator.CreateInstance(type);
#endif
        }
    }
}
