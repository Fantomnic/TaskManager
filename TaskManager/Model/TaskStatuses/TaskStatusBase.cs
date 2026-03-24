using System.Windows.Media;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Базовый класс статуса задачи</summary>
    public abstract class TaskStatusBase
    {
        public virtual string DisplayName => "";

        /// <summary>Статусы, в которые можно перейти из текущего статуса</summary>
        internal virtual List<TaskStatusBase> Transitions => [];

        public virtual SolidColorBrush Background => new();

        internal bool HasAcceptCommandTransition() => Transitions.Contains(TaskStatusesInstances.BeginingStatus);

        internal bool HasRejectCommandTransition() => Transitions.Contains(TaskStatusesInstances.RejectedStatus);

        internal bool HasDeferCommandTransition() => Transitions.Contains(TaskStatusesInstances.DeferredStatus);

        internal bool HasDoneCommandTransition() => Transitions.Contains(TaskStatusesInstances.DoneStatus);

        internal bool HasCompleteCommandTransition() => Transitions.Contains(TaskStatusesInstances.CompletedStatus);
    }
}
