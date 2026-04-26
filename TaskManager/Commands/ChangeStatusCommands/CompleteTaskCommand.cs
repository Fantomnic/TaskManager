using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class CompleteTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.CompletedStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasCompleteCommandTransition();
    }
}
