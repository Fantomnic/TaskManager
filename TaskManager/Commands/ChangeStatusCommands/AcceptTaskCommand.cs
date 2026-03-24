using TaskManager.Model;
using TaskManager.Model.TaskStatuses;

namespace TaskManager.Commands
{
    public class AcceptTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.BeginingStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasAcceptCommandTransition();
    }
}
