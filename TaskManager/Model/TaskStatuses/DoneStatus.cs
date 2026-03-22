namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Выполнено"</summary>
    public class DoneStatus : TaskStatusBase
    {
        public override string DisplayName => "Выполнено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus];
    }
}
