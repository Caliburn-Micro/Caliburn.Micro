using System;
using System.Collections.Generic;
using System.Linq;

#if WINDOWS_UWP
using System.Reflection;
using System.Text.RegularExpressions;

using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

using EventTrigger = Microsoft.Xaml.Interactions.Core.EventTriggerBehavior;
using TriggerAction = Microsoft.Xaml.Interactivity.IAction;
using TriggerBase = Microsoft.Xaml.Interactivity.IBehavior;
#elif XFORMS
using System.Reflection;
using System.Text.RegularExpressions;

using Xamarin.Forms;

using DependencyObject = Xamarin.Forms.BindableObject;
using FrameworkElement = Xamarin.Forms.VisualElement;
#elif MAUI
using System.Text.RegularExpressions;

using Microsoft.Maui.Controls;

using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
#else
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

using EventTrigger = Microsoft.Xaml.Behaviors.EventTrigger;
using TriggerAction = Microsoft.Xaml.Behaviors.TriggerAction;
using TriggerBase = Microsoft.Xaml.Behaviors.TriggerBase;
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
    /// Parses text into a fully functional set of <see cref="TriggerBase"/> instances with <see cref="ActionMessage"/>.
    /// </summary>
    public static class Parser {
        private static readonly Regex LongFormatRegularExpression = new Regex(@"^[\s]*\[[^\]]*\][\s]*=[\s]*\[[^\]]*\][\s]*$");
#if WINDOWS_UWP
        private static readonly ILog Log = LogManager.GetLog(typeof(Parser));
#endif

        /// <summary>
        /// Gets or sets the function used to generate a trigger.
        /// </summary>
        /// <remarks>The parameters passed to the method are the the target of the trigger and string representing the trigger.</remarks>
        public static Func<DependencyObject, string, TriggerBase> CreateTrigger { get; set; }
            = (target, triggerText) => {
                if (triggerText == null) {
                    ElementConvention defaults = ConventionManager.GetElementConvention(target.GetType());
                    return defaults.CreateTrigger();
                }

                string triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Replace("Event", string.Empty)
                    .Trim();
#if XFORMS || MAUI
                return new EventTrigger { Event = triggerDetail };
#else
                return new EventTrigger { EventName = triggerDetail };
#endif
            };

        /// <summary>
        /// Gets or sets function used to parse a string identified as a message.
        /// </summary>
        public static Func<DependencyObject, string, TriggerAction> InterpretMessageText { get; set; }
            = (target, text)
            => new ActionMessage { MethodName = Regex.Replace(text, "^Action", string.Empty).Trim() };

        /// <summary>
        /// Gets or sets function used to parse a string identified as a message parameter.
        /// </summary>
        public static Func<DependencyObject, string, Parameter> CreateParameter { get; set; }
            = (target, parameterText) => {
                var actualParameter = new Parameter();

                if (parameterText.StartsWith("'", StringComparison.OrdinalIgnoreCase) && parameterText.EndsWith("'", StringComparison.OrdinalIgnoreCase)) {
                    actualParameter.Value = parameterText.Substring(1, parameterText.Length - 2);
                } else if (MessageBinder.SpecialValues.ContainsKey(parameterText.ToLowerInvariant()) || decimal.TryParse(parameterText, out _)) {
                    actualParameter.Value = parameterText;
                } else if (target is FrameworkElement fe) {
                    string[] nameAndBindingMode = parameterText.Split(':').Select(x => x.Trim()).ToArray();
                    int index = nameAndBindingMode[0].IndexOf('.');

                    View.ExecuteOnLoad(
                        fe,
                        (sender, e)
                            => BindParameter(
                                fe,
                                actualParameter,
                                index == -1 ? nameAndBindingMode[0] : nameAndBindingMode[0].Substring(0, index),
                                index == -1 ? null : nameAndBindingMode[0].Substring(index + 1),
                                nameAndBindingMode.Length == 2 ? (BindingMode)Enum.Parse(typeof(BindingMode), nameAndBindingMode[1], true) : BindingMode.OneWay));
                }

                return actualParameter;
            };

        /// <summary>
        /// Parses the specified message text.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="text">The message text.</param>
        /// <returns>The triggers parsed from the text.</returns>
        public static IEnumerable<TriggerBase> Parse(DependencyObject target, string text) {
            if (string.IsNullOrEmpty(text)) {
                return Array.Empty<TriggerBase>();
            }

            var triggers = new List<TriggerBase>();
            string[] messageTexts = StringSplitter.Split(text, ';');

            foreach (string messageText in messageTexts) {
                string[] triggerPlusMessage = LongFormatRegularExpression.IsMatch(messageText)
                                             ? StringSplitter.Split(messageText, '=')
                                             : new[] { null, messageText };

                string messageDetail = triggerPlusMessage.Last()
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Trim();

                TriggerBase trigger = CreateTrigger(target, triggerPlusMessage.Length == 1 ? null : triggerPlusMessage[0]);
                TriggerAction message = CreateMessage(target, messageDetail);

#if WINDOWS_UWP || XFORMS || MAUI
                AddActionToTrigger(target, message, trigger);
#else
                trigger.Actions.Add(message);
#endif

                triggers.Add(trigger);
            }

            return triggers;
        }

        /// <summary>
        /// Creates an instance of <see cref="ActionMessage"/> by parsing out the textual dsl.
        /// </summary>
        /// <param name="target">The target of the message.</param>
        /// <param name="messageText">The textual message dsl.</param>
        /// <returns>The created message.</returns>
        public static TriggerAction CreateMessage(DependencyObject target, string messageText) {
            int openingParenthesisIndex = messageText.IndexOf('(');
            if (openingParenthesisIndex < 0) {
                openingParenthesisIndex = messageText.Length;
            }

            int closingParenthesisIndex = messageText.LastIndexOf(')');
            if (closingParenthesisIndex < 0) {
                closingParenthesisIndex = messageText.Length;
            }

            string core = messageText.Substring(0, openingParenthesisIndex).Trim();
            TriggerAction message = InterpretMessageText(target, core);
            if (message is IHaveParameters withParameters) {
                if (closingParenthesisIndex - openingParenthesisIndex > 1) {
                    string paramString = messageText.Substring(openingParenthesisIndex + 1, closingParenthesisIndex - openingParenthesisIndex - 1);
                    string[] parameters = StringSplitter.SplitParameters(paramString);

                    foreach (string parameter in parameters) {
                        withParameters.Parameters.Add(CreateParameter(target, parameter.Trim()));
                    }
                }
            }

            return message;
        }

        /// <summary>
        /// Creates a binding on a <see cref="Parameter"/>.
        /// </summary>
        /// <param name="target">The target to which the message is applied.</param>
        /// <param name="parameter">The parameter object.</param>
        /// <param name="elementName">The name of the element to bind to.</param>
        /// <param name="path">The path of the element to bind to.</param>
        /// <param name="bindingMode">The binding mode to use.</param>
        public static void BindParameter(FrameworkElement target, Parameter parameter, string elementName, string path, BindingMode bindingMode) {
#if XFORMS || MAUI
            FrameworkElement element = elementName == "$this" ? target : null;
            if (element == null) {
                return;
            }

            if (string.IsNullOrEmpty(path)) {
                path = ConventionManager.GetElementConvention(element.GetType()).ParameterProperty;
            }

            var binding = new Binding(path) {
                Source = element,
                Mode = bindingMode,
            };

            parameter.SetBinding(Parameter.ValueProperty, binding);
#else
            FrameworkElement element = elementName == "$this"
                ? target
                : BindingScope.GetNamedElements(target).FindName(elementName);
            if (element == null) {
                return;
            }

            if (string.IsNullOrEmpty(path)) {
                path = ConventionManager.GetElementConvention(element.GetType()).ParameterProperty;
            }
#if WINDOWS_UWP
            var binding = new Binding {
                Path = new PropertyPath(path),
                Source = element,
                Mode = bindingMode,
            };
#else
            var binding = new Binding(path) {
                Source = element,
                Mode = bindingMode,
            };
#endif

#if !WINDOWS_UWP
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
#endif
            BindingOperations.SetBinding(parameter, Parameter.ValueProperty, binding);
#endif
        }

#if XFORMS || MAUI
        private static void AddActionToTrigger(DependencyObject target, TriggerAction message, TriggerBase trigger) {
            if (trigger is EventTrigger eventTrigger) {
                eventTrigger.Actions.Add(message);
            }

            trigger.EnterActions.Add(message);

            // TriggerAction doesn't have an associated object property so we have
            // to create it ourselves, could be potential issues here with leaking the associated
            // object and not correctly detaching, this may depend if the trigger implements it's
            // AssociatedObject as a DependencyProperty.
            if (message is ActionMessage actionMessage &&
                target is FrameworkElement targetElement) {
                actionMessage.AssociatedObject = targetElement;
            }
        }
#endif

#if WINDOWS_UWP

        private static void AddActionToTrigger(DependencyObject target, TriggerAction message, TriggerBase trigger) {
            // This is stupid, but there a number of limitiations in the 8.1 Behaviours SDK

            // The first is that there is no base class for a Trigger, just IBehaviour. Which
            // means there's no strongly typed way to add an action to a trigger. Every trigger
            // in the SDK implements the same pattern but no interface, we're going to have to
            // use reflection to set it.

            // More stupidity, ActionCollection doesn't care about IAction, but DependencyObject
            // and there's no actual implementation of
            if (message is not DependencyObject messageDependencyObject) {
                Log.Warn("{0} doesn't inherit DependencyObject and can't be added to ActionCollection", trigger.GetType().FullName);
                return;
            }

            // 95% of the time the trigger will be an EventTrigger, let's optimise for that case
            if (trigger is EventTrigger eventTrigger) {
                eventTrigger.Actions.Add(messageDependencyObject);
            } else {
                PropertyInfo actionsProperty = trigger.GetType().GetRuntimeProperty("Actions");
                if (actionsProperty == null) {
                    Log.Warn("Could not find Actions collection on trigger {0}.", trigger.GetType().FullName);
                    return;
                }

                if (actionsProperty.GetValue(trigger) is not ActionCollection actionCollection) {
                    Log.Warn("{0}.Actions is either not an ActionCollection or is null.", trigger.GetType().FullName);
                    return;
                }

                actionCollection.Add(messageDependencyObject);
            }

            // The second is the IAction doesn't have an associated object property so we have
            // to create it ourselves, could be potential issues here with leaking the associated
            // object and not correctly detaching, this may depend if the trigger implements it's
            // AssociatedObject as a DependencyProperty.
            // Turns out trying to a binding won't work because the trigger doesn't notify the
            // binding of changes, so we just need to set it, yay.
            if (message is ActionMessage actionMessage &&
                target is FrameworkElement targetElement) {
                // var binding = new Binding { Source = trigger, Path = new PropertyPath("AssociatedObject") };
                // BindingOperations.SetBinding(actionMessage, ActionMessage.AssociatedObjectProperty, binding);
                actionMessage.AssociatedObject = targetElement;
            }
        }
#endif
    }
}
