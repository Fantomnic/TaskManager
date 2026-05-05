using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModels;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Commands
{
    public class DoneTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.DoneStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Type == TaskType.Regular && taskObject.Status.HasDoneCommandTransition();

        internal override void ExecuteImplement(object? parameter)
        {
            if (!ValidateParameter(parameter, out var taskObjectViewModel, out var taskObject) || !ExecuteImplementCore(taskObjectViewModel, taskObject))
                return;

            taskObjectViewModel.ExecutionsCount++;
        }
    }
}
