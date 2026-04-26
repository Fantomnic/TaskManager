using System.Reflection.Metadata;
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
            if (parameter is not TaskObjectViewModel taskObjectViewModel || taskObjectViewModel.TaskObject is not TaskObject taskObject)
                return;

            ExecuteImplementCore(taskObjectViewModel, taskObject);
        }

        internal bool ExecuteImplementCore(TaskObjectViewModel taskObjectViewModel, TaskObject taskObject)
        {
            if (!CanChange(taskObject)
                || Settings.ConfirmCompleteTask
                && _targetStatus is CompletedStatus
                && !UIHelper.ShowMessage($"Завершить задачу \"{taskObject.Name}\"? Вы не сможете принять её повторно", System.Windows.MessageBoxImage.Question))
            {
                return false;
            }

            taskObjectViewModel.TaskStatus = _targetStatus;
            taskObjectViewModel.RefreshAfterChangeStatus();
            Helper.MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();

            return true;
        }
    }
}
