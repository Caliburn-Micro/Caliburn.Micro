namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

#if !SILVERLIGHT
    using System.ComponentModel;
#endif

    public static class MessageBinder
    {
        static readonly ILog Log = LogManager.GetLog(typeof(MessageBinder));
        public static readonly string[] SpecialValues = new[] { "$eventargs", "$parameter", "$datacontext", "$context", "$source" };

        public static IResult CreateResult(object returnValue)
        {
            if(returnValue is IResult)
                returnValue = new[] { returnValue as IResult };

            if(returnValue is IEnumerable<IResult>)
                return new SequentialResult(returnValue as IEnumerable<IResult>);

            return null;
        }

        public static object[] DetermineParameters(ActionMessage message, ParameterInfo[] requiredParameters, FrameworkElement source, object context)
        {
            var providedValues = message.Parameters.Select(x => x.Value).ToArray();
            var values = new object[requiredParameters.Length];

            if(providedValues.Length != requiredParameters.Length)
            {
                var ex = new Exception(string.Format("Parameter count mismatch for {0}.", message));
                Log.Error(ex);
                throw ex;
            }

            for (int i = 0; i < providedValues.Length; i++)
            {
                var value = providedValues[i];
                var stringValue = value as string;
                object potentialValue;

                if (stringValue != null)
                {
                    switch (stringValue.ToLower())
                    {
                        case "$eventargs":
                        case "$parameter":
                            potentialValue = context;
                            break;
                        case "$datacontext":
                        case "$context":
                            potentialValue = source.DataContext;
                            break;
                        case "$source":
                            potentialValue = source;
                            break;
                        default:
                            potentialValue = stringValue;
                            break;
                    }
                }
                else potentialValue = value;

                values[i] = CoerceValue(requiredParameters[i].ParameterType, potentialValue);
            }

            return values;
        }

        public static object CoerceValue(Type destinationType, object providedValue)
        {
            if (providedValue == null) return GetDefaultValue(destinationType);

            var providedType = providedValue.GetType();

            if (destinationType.IsAssignableFrom(providedType))
                return providedValue;

            try
            {
                var converter = TypeDescriptor.GetConverter(destinationType);

                if (converter.CanConvertFrom(providedType))
                    return converter.ConvertFrom(providedValue);

                converter = TypeDescriptor.GetConverter(providedType);

                if (converter.CanConvertTo(destinationType))
                    return converter.ConvertTo(providedValue, destinationType);
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

        public static object GetDefaultValue(Type type)
        {
            return type.IsClass || type.IsInterface ? null : Activator.CreateInstance(type);
        }
    }
}