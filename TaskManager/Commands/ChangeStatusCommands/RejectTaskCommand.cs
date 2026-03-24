using TaskManager.Model;
using TaskManager.Model.TaskStatuses;

namespace TaskManager.Commands
{
    public class RejectTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.RejectedStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasRejectCommandTransition();
    }
}
