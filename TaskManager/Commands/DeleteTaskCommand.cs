using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public class DeleteTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is TaskObjectViewModel taskObjectViewModel)
                DeleteTask(taskObjectViewModel);
        }

        internal void DeleteTask(TaskObjectViewModel taskObjectViewModel)
        {
            var currentSection = Helper.MainViewModel.SelectedSectionViewModel;
            currentSection.RemoveTask(taskObjectViewModel);
        }
    }
}
