using TaskManager.Model;
using TaskManager.Model.TaskStatuses;

namespace TaskManager.Commands
{
    public class DeferTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.DeferredStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasDeferCommandTransition();
    }
}
