using System;
using System.Windows.Input;

namespace Notifications.Wpf.Utils
{
    public class DelegateCommand : ICommand
    {
        public Action<object> CommandAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }

        public DelegateCommand(Action<object> commandAction, Func<object, bool> canExecuteFunc = null)
        {
            CommandAction = commandAction;
            CanExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            CommandAction(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}