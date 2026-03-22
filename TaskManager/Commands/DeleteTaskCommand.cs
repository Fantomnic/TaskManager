using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public class DeleteTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            //if (parameter is not TaskObjectViewModel taskObjectViewModel)
            //    return;

            //var currentSection = Helper.MainViewModel.SelectedSectionViewModel;
            //currentSection.RemoveTask(taskObjectViewModel.TaskObject);
        }
    }
}
