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
            if (parameter is not TaskObjectViewModel taskObjectViewModel || !CanChange(taskObjectViewModel.TaskObject))
                return;

            taskObjectViewModel.TaskStatus = _targetStatus;
            Helper.MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();
        }
    }
}
