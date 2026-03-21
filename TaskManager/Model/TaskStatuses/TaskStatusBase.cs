namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Базовый класс статуса задачи</summary>
    public abstract class TaskStatusBase
    {
        public virtual string DisplayName => "";

        /// <summary>Статусы, в которые можно перейти из текущего статуса</summary>
        internal virtual List<TaskStatusBase> Transitions => [];

        internal bool IsAcceptCommandVisible() => Transitions.Contains(TaskStatusesInstances.BeginingStatus);
    }
}
