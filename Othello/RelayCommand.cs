using System;
using System.Windows.Input;

/// <summary>
/// https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
/// </summary>
namespace Othello
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> _execute) : this(_execute, null)
        {
            
        }

        public RelayCommand(Action<object> _execute, Predicate<object> _canExecute)
        {
            execute = _execute;
            canExecute = _canExecute;
        }

        public bool CanExecute(object _parameter)
        {
            return canExecute?.Invoke(_parameter) ?? true;
        }

        public void Execute(object _parameter)
        {
            execute(_parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
