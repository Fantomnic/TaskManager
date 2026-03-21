using TaskManager.Model;
using TaskManager.Model.TaskStatuses;

namespace TaskManager.Commands
{
    public class ChangeStatusCommands
    {
        public abstract class ChangeTaskStatusCommand : BaseCommand
        {
            private protected virtual TaskStatusBase _targetStatus { get; }

            internal override void ExecuteImplement(object? parameter)
            {
                if (parameter is TaskObject taskObject && taskObject.Status.Transitions.Contains(_targetStatus))
                    taskObject.Status = _targetStatus;
            }
        }

        public class AcceptTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.BeginingStatus;
        }

        public class RejectTaskCommand : ChangeTaskStatusCommand
        {
            private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.RejectedStatus;
        }
    }
}
