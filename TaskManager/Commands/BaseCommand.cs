using System.Windows.Input;

namespace TaskManager.Commands
{
    /// <summary>Базовый класс для команд приложения</summary>
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => CanExecuteImplement(parameter);

        public void Execute(object? parameter) => ExecuteImplement(parameter);

        internal virtual bool CanExecuteImplement(object? parameter) => true;

        internal virtual void ExecuteImplement(object? parameter)
        {

        }
    }
}
