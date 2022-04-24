using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Caliburn.Micro
{
    internal class CaliburnCommand : ICommand
    {
        Action<object> _commandToExecute;
        Func<object, bool> _canExecuteCommand;

        public CaliburnCommand(Action<object> commandToExecute, Func<object, bool> canExecuteCommand)
        {
            _commandToExecute = commandToExecute;
            _canExecuteCommand = canExecuteCommand;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecuteCommand != null)
            {
                return _canExecuteCommand(parameter);
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            _commandToExecute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;

        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
