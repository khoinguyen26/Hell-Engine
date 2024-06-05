using System.Windows.Input;

namespace HellEditor
{
    public class RelayCommand<T> : ICommand
    {
        // contain what need to be done
        private readonly Action<T> _execute;
        // determine if the action can be done
        private readonly Predicate<T> _canExec;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExec?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute((T)parameter);
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExec = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExec = canExec;
        }
    }
}
