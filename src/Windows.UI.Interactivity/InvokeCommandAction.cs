using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Executes a specified ICommand when invoked.
    /// 
    /// </summary>
    public sealed class InvokeCommandAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeCommandAction), null);
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeCommandAction), null);
        private string commandName;

        /// <summary>
        /// Gets or sets the name of the command this action should invoke.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The name of the command this action should invoke.
        /// </value>
        /// 
        /// <remarks>
        /// This property will be superseded by the Command property if both are set.
        /// </remarks>
        public string CommandName
        {
            get
            {
                return this.commandName;
            }
            set
            {
                if (!(this.CommandName != value))
                {
                    return;
                }
                this.commandName = value;
            }
        }

        /// <summary>
        /// Gets or sets the command this action should invoke. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The command to execute.
        /// </value>
        /// 
        /// <remarks>
        /// This property will take precedence over the CommandName property if both are set.
        /// </remarks>
        public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(InvokeCommandAction.CommandProperty);
            }
            set
            {
                this.SetValue(InvokeCommandAction.CommandProperty, (object)value);
            }
        }

        /// <summary>
        /// Gets or sets the command parameter. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The command parameter.
        /// </value>
        /// 
        /// <remarks>
        /// This is the value passed to ICommand.CanExecute and ICommand.Execute.
        /// </remarks>
        public object CommandParameter
        {
            get
            {
                return this.GetValue(InvokeCommandAction.CommandParameterProperty);
            }
            set
            {
                this.SetValue(InvokeCommandAction.CommandParameterProperty, value);
            }
        }

        static InvokeCommandAction()
        {
        }

        /// <summary>
        /// Invokes the action.
        /// 
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject == null)
                return;
            ICommand command = this.ResolveCommand();
            if (command == null || !command.CanExecute(this.CommandParameter))
                return;
            command.Execute(this.CommandParameter);
        }

        private ICommand ResolveCommand()
        {
            ICommand command = null;

            if (this.Command != null)
            {
                command = this.Command;
            }
            else if (this.AssociatedObject != null)
            {
                foreach (PropertyInfo propertyInfo in this.AssociatedObject.GetType().GetTypeInfo().DeclaredProperties)
                {
                    if (typeof(ICommand).GetTypeInfo().IsAssignableFrom(propertyInfo.PropertyType.GetTypeInfo()) && string.Equals(propertyInfo.Name, this.CommandName, StringComparison.Ordinal))
                    {
                        command = (ICommand)propertyInfo.GetValue((object)this.AssociatedObject, (object[])null);
                    }
                }
            }
            return command;
        }
    }
}
