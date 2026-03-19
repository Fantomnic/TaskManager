using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.Commands
{
    public class DeleteTaskCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is TaskObject taskObject)
                DeleteTask(taskObject);
        }

        internal void DeleteTask(TaskObject taskObject)
        {
            var currentSection = Helper.MainViewModel.SelectedSection;
            currentSection.RemoveTask(taskObject);
        }
    }
}
