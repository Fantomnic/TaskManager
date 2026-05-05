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
            if (!ValidateParameter(parameter, out var taskObjectViewModel, out var taskObject))
                return;

            ExecuteImplementCore(taskObjectViewModel, taskObject);
        }

        protected static bool ValidateParameter(object? parameter, out TaskObjectViewModel taskObjectViewModel, out TaskObject taskObject)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            taskObjectViewModel = parameter as TaskObjectViewModel;
            taskObject = taskObjectViewModel?.TaskObject;
#pragma warning restore CS8601 // Possible null reference assignment.
            return taskObjectViewModel is not null && taskObject is not null;
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
            Helper.MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();

            return true;
        }
    }
}
