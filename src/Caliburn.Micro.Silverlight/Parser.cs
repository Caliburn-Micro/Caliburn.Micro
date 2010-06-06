namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Data;
    using EventTrigger = System.Windows.Interactivity.EventTrigger;
    using TriggerBase = System.Windows.Interactivity.TriggerBase;

    public static class Parser
    {
        private static readonly Regex Regex = new Regex(@",(?=(?:[^']*'[^']*')*(?![^']*'))");

        public static IEnumerable<TriggerBase> Parse(DependencyObject target, string text)
        {
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
                yield return trigger;
            }
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
            messageText = messageText.Replace("Action", string.Empty);

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
                    message.Parameters.Add(
                        CreateParameter(target, parameter.Trim())
                        );
                }
            }

            return message;
        }

        static Parameter CreateParameter(DependencyObject target, string parameter)
        {
            var actualParameter = new Parameter();

            if (parameter.StartsWith("'") && parameter.EndsWith("'"))
                actualParameter.Value = parameter.Substring(1, parameter.Length - 2);
            else if (MessageBinder.SpecialValues.Contains(parameter.ToLower()) || char.IsNumber(parameter[0]))
                actualParameter.Value = parameter;
            else
            {
                var nameAndBindingMode = parameter.Split(':')
                    .Select(x => x.Trim()).ToArray();

                var index = nameAndBindingMode[0].IndexOf('.');

                if (index == -1)
                {
                    var fe = target as FrameworkElement;
                    if (fe != null)
                    {
                        fe.Loaded += delegate{
                            var element = fe.FindName(nameAndBindingMode[0]);
                            if (element == null)
                                return;

                            var convention = ConventionManager.GetElementConvention(element.GetType());
                            var binding = new Binding(convention.ParameterProperty) {
                                ElementName = nameAndBindingMode[0]
                            };

                            BindingOperations.SetBinding(actualParameter, Parameter.ValueProperty, binding);
                        };
                    }
                }
                else
                {
                    var elementName = nameAndBindingMode[0].Substring(0, index);
                    var path = new PropertyPath(nameAndBindingMode[0].Substring(index + 1));

                    var binding = elementName == "$this"
                        ? new Binding {
                            Path = path,
                            Source = target
                        }
                        : new Binding {
                            Path = path,
                            ElementName = elementName
                        };

                    if (nameAndBindingMode.Length == 2)
                        binding.Mode = (BindingMode)Enum.Parse(typeof(BindingMode), nameAndBindingMode[1], true);

                    BindingOperations.SetBinding(actualParameter, Parameter.ValueProperty, binding);
                }
            }

            return actualParameter;
        }
    }
}