namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Отложено"</summary>
    public class DeferredStatus : TaskStatusBase
    {
        public override string DisplayName => "Отложено";

        internal override List<TaskStatusBase> Transitions => [];
    }
}
