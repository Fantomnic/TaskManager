using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    internal class NewTaskCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            AddTask();
        }

        internal void AddTask()
        {
            var newTask = new TaskObject();
            var newTaskViewModel = new TaskObjectViewModel(newTask);

            var newTaskWindow = new NewTaskWindow(newTaskViewModel);
            newTaskWindow.OpenEditDescription();

            if (newTaskWindow.ShowDialog() != true)
                return;

            var currentSection = Helper.MainViewModel.SelectedSection;

            currentSection.Tasks.Add(newTask);
        }
    }
}
