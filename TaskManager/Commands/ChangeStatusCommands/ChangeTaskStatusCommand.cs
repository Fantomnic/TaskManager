using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public abstract class ChangeTaskStatusCommand : BaseCommand
    {
        private protected virtual TaskStatusBase _targetStatus { get; }

        internal virtual bool CanChange(TaskObject taskObject) => true;

        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is TaskObjectViewModel taskObjectViewModel && CanChange(taskObjectViewModel.TaskObject))
                taskObjectViewModel.TaskStatus = _targetStatus;
        }
    }
}
