using TaskManager.Helpers;
using TaskManager.Helpers.Exceptions;
using TaskManager.Model;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class DeleteTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel)
                return;

            if (taskObjectViewModel.AdditionalSectionViewModel is not null)
                throw new WarningException("Нельзя удалить задачу, которая содержится в неосновном разделе");

            if (Settings.ConfirmDeleteTask && !UIHelper.ShowMessage($"Удалить задачу \"{taskObjectViewModel.Name}\"?", System.Windows.MessageBoxImage.Question))
                return;

            var masterSectionViewModel = Helper.MasterSectionViewModel;
            masterSectionViewModel.RemoveTaskViewModel(taskObjectViewModel);
        }
    }
}
