using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public abstract class ChangeTaskStatusCommand : BaseCommand
    {
        private protected abstract TaskStatusBase _targetStatus { get; }

        internal virtual bool CanChange(TaskObject taskObject) => true;

        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel
                || taskObjectViewModel.TaskObject is not TaskObject taskObject
                || !CanChange(taskObject)
                || Settings.ConfirmCompleteTask
                && _targetStatus is CompletedStatus
                && !UIHelper.ShowMessage($"Завершить задачу \"{taskObject.Name}\"? Вы не сможете принять её повторно", System.Windows.MessageBoxImage.Question))
            {
                return;
            }

            taskObjectViewModel.TaskStatus = _targetStatus;
            Helper.MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();
        }
    }
}
