namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Data;
    using EventTrigger = System.Windows.Interactivity.EventTrigger;
    using TriggerBase = System.Windows.Interactivity.TriggerBase;

    /// <summary>
    /// Parses text into a fully functional set of <see cref="TriggerBase"/> instances with <see cref="ActionMessage"/>.
    /// </summary>
    public static class Parser
    {
        static readonly Regex Regex = new Regex(@",(?=(?:[^']*'[^']*')*(?![^']*'))");

        /// <summary>
        /// Parses the specified message text.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="text">The message text.</param>
        /// <returns>The triggers parsed from the text.</returns>
        public static IEnumerable<TriggerBase> Parse(DependencyObject target, string text)
        {
            var triggers = new List<TriggerBase>();
            var items = text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var messageText in items)
            {
                var triggerPlusMessage = messageText.Split('=');
                string messageDetail = triggerPlusMessage.Last()
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Trim();

                var trigger = CreateTrigger(target.GetType(), triggerPlusMessage);
                var message = CreateMessage(target, messageDetail);

                trigger.Actions.Add(message);
                triggers.Add(trigger);
            }

            return triggers;
        }

        static TriggerBase CreateTrigger(Type targetType, string[] triggerPlusMessage)
        {
            if(triggerPlusMessage.Length == 1)
            {
                var defaults = ConventionManager.GetElementConvention(targetType);
                return defaults.CreateTrigger();
            }

            var triggerDetail = triggerPlusMessage[0]
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Replace("Event", string.Empty)
                .Trim();

            return new EventTrigger { EventName = triggerDetail };
        }

        static ActionMessage CreateMessage(DependencyObject target, string messageText)
        {
            var message = new ActionMessage();
            messageText = Regex.Replace(messageText, "^Action", string.Empty);

            var openingParenthesisIndex = messageText.IndexOf('(');
            if (openingParenthesisIndex < 0) openingParenthesisIndex = messageText.Length;
            var closingParenthesisIndex = messageText.LastIndexOf(')');
            if (closingParenthesisIndex < 0) closingParenthesisIndex = messageText.Length;

            var core = messageText.Substring(0, openingParenthesisIndex).Trim();

            message.MethodName = core;

            if (closingParenthesisIndex - openingParenthesisIndex > 1)
            {
                var paramString = messageText.Substring(openingParenthesisIndex + 1,
                    closingParenthesisIndex - openingParenthesisIndex - 1);

                var parameters = Regex.Split(paramString);

                foreach (var parameter in parameters)
                {
                    message.Parameters.Add(CreateParameter(target, parameter.Trim()));
                }
            }

            return message;
        }

        static Parameter CreateParameter(DependencyObject target, string parameter)
        {
            var actualParameter = new Parameter();

            if(parameter.StartsWith("'") && parameter.EndsWith("'"))
                actualParameter.Value = parameter.Substring(1, parameter.Length - 2);
            else if(MessageBinder.SpecialValues.Contains(parameter.ToLower()) || char.IsNumber(parameter[0]))
                actualParameter.Value = parameter;
            else if (target is FrameworkElement)
            {
                var fe = (FrameworkElement)target;
                var nameAndBindingMode = parameter.Split(':').Select(x => x.Trim()).ToArray();
                var index = nameAndBindingMode[0].IndexOf('.');

                RoutedEventHandler handler = null;
                handler = (s, e) =>{
                    BindParameter(
                        fe,
                        actualParameter,
                        index == -1 ? nameAndBindingMode[0] : nameAndBindingMode[0].Substring(0, index),
                        index == -1 ? null : nameAndBindingMode[0].Substring(index + 1),
                        nameAndBindingMode.Length == 2 ? (BindingMode)Enum.Parse(typeof(BindingMode), nameAndBindingMode[1], true) : BindingMode.OneWay
                        );
                    fe.Loaded -= handler;
                };

                if ((bool)fe.GetValue(View.IsLoadedProperty))
                    handler(null, null);
                else fe.Loaded += handler;
            }

            return actualParameter;
        }

        private static void BindParameter(FrameworkElement target, Parameter parameter, string elementName, string path, BindingMode bindingMode)
        {
            var element = elementName == "$this" 
                ? target 
                : ExtensionMethods.GetNamedElementsInScope(target).FindName(elementName);
            if (element == null)
                return;

            if(string.IsNullOrEmpty(path))
                path = ConventionManager.GetElementConvention(element.GetType()).ParameterProperty;

            var binding = new Binding(path) {
                Source = element,
                Mode = bindingMode
            };

#if SILVERLIGHT
            var expression = (BindingExpression)BindingOperations.SetBinding(parameter, Parameter.ValueProperty, binding);

            var field = element.GetType().GetField(path + "Property", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (field == null)
                return;

            ConventionManager.ApplySilverlightTriggers(element, (DependencyProperty)field.GetValue(null), x => expression);
#else
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(parameter, Parameter.ValueProperty, binding);
#endif
        }
    }
}