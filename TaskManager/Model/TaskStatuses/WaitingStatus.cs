namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Ожидает принятия"</summary>
    public class WaitingStatus : TaskStatusBase
    {
        public override string DisplayName => "Ожидает принятия";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus, TaskStatusesInstances.RejectedStatus];
    }
}