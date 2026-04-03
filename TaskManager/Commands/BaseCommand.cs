using System.Windows;
using System.Windows.Input;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal virtual bool CanExecuteImplement(object? parameter) => true;

        internal abstract void ExecuteImplement(object? parameter);
    }
}
