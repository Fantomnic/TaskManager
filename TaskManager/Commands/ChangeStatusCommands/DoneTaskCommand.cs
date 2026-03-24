using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Commands
{
    public class DoneTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.DoneStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Type == TaskType.Regular && taskObject.Status.HasDoneCommandTransition();
    }
}
