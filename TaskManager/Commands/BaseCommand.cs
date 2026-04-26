using System.Windows;
using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.Helpers.Exceptions;

namespace TaskManager.Commands
{
    /// <summary>Базовый класс для команд приложения</summary>
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => CanExecuteImplement(parameter);

        public void Execute(object? parameter)
        {
            try
            {
                ExecuteImplement(parameter);
            }
            catch (WarningException warn)
            {
                UIHelper.ShowMessage(warn.Message, MessageBoxImage.Warning);
            }
        }

        internal virtual bool CanExecuteImplement(object? parameter) => true;

        internal abstract void ExecuteImplement(object? parameter);
    }
}
