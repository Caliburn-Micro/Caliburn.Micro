namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using EventTrigger = System.Windows.Interactivity.EventTrigger;
    using TriggerBase = System.Windows.Interactivity.TriggerBase;

    public static class Parser
    {
        static readonly ILog Log = LogManager.GetLog(typeof(Parser));
        static readonly Regex Regex = new Regex(@",(?=(?:[^']*'[^']*')*(?![^']*'))");

        public static IEnumerable<TriggerBase> Parse(DependencyObject target, string text)
        {
            var triggers = new List<TriggerBase>();
            var items = text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var messageText in items)
            {
                Log.Info("Parsing: {0}.", messageText);

                var triggerPlusMessage = messageText.Split('=');
                string messageDetail = triggerPlusMessage.Last()
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Trim();

                var trigger = CreateTrigger(target.GetType(), triggerPlusMessage);
                Log.Info("Created trigger: {0}.", trigger);

                var message = CreateMessage(target, messageDetail);
                Log.Info("Created message: {0}.", message);

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
            else if(target is FrameworkElement)
            {
                var fe = (FrameworkElement)target;
                var nameAndBindingMode = parameter.Split(':').Select(x => x.Trim()).ToArray();
                var index = nameAndBindingMode[0].IndexOf('.');

                if(index == -1)
                    fe.Loaded += (s, e) => BindParameter(fe, actualParameter, nameAndBindingMode[0], null, BindingMode.OneWay);
                else
                {
                    var elementName = nameAndBindingMode[0].Substring(0, index);
                    var path = nameAndBindingMode[0].Substring(index + 1);
                    var mode = nameAndBindingMode.Length == 2 ? (BindingMode)Enum.Parse(typeof(BindingMode), nameAndBindingMode[1], true) : BindingMode.OneWay;

                    fe.Loaded += (s, e) => BindParameter(fe, actualParameter, elementName, path, mode);
                }
            }

            return actualParameter;
        }

        private static void BindParameter(FrameworkElement target, Parameter parameter, string elementName, string path, BindingMode bindingMode)
        {
            var element = elementName == "$this" ? target : (DependencyObject)target.FindName(elementName);
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

            var bindableProperty = (DependencyProperty)field.GetValue(null);

            var textBox = element as TextBox;
            if (textBox != null && bindableProperty == TextBox.TextProperty)
            {
                textBox.TextChanged += delegate { expression.UpdateSource(); };
                return;
            }

            var passwordBox = element as PasswordBox;
            if (passwordBox != null && bindableProperty == PasswordBox.PasswordProperty)
            {
                passwordBox.PasswordChanged += delegate { expression.UpdateSource(); };
                return;
            }
#else
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(parameter, Parameter.ValueProperty, binding);
#endif
        }
    }
}