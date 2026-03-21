using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public class NewTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            AddTask();
        }

        internal void AddTask()
        {
            var newTask = new TaskObject() { Name = GetDefaultTaskName() };
            var newTaskViewModel = new TaskObjectViewModel(newTask);

            var newTaskWindow = new NewTaskWindow(newTaskViewModel);
            newTaskWindow.OpenEditDescription();

            if (newTaskWindow.ShowDialog() != true)
                return;

            Section currentSection = Helper.MainViewModel.SelectedSection;
            currentSection.AddTask(newTask);
        }

        private static string GetDefaultTaskName()
        {
            if (Settings.SetDefaultTaskName != true)
                return String.Empty;

            string result = Settings.DefaultTaskName;

            if (Settings.IncrementTaskName == true)
            {
                var existingNames = Helper.GetAllTasks().Select(s => s.Name);
                result = Helper.GetStringWithCounter(result, existingNames);
            }

            return result;
        }
    }
}
