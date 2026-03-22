using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModel;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Commands
{
    public class ChangeStatusCommands
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

        public class AcceptTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.BeginingStatus;

            internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasAcceptCommandTransition();
        }

        public class RejectTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.RejectedStatus;

            internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasRejectCommandTransition();
        }

        public class DeferTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.DeferredStatus;

            internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasDeferCommandTransition();
        }

        public class DoneTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.DoneStatus;

            internal override bool CanChange(TaskObject taskObject) => taskObject.Type == TaskType.Regular && taskObject.Status.HasDoneCommandTransition();
        }

        public class CompleteTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.CompletedStatus;

            internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasCompleteCommandTransition();
        }
    }
}
