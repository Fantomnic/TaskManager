using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.Commands
{
    public class DeleteTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
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
