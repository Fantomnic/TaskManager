namespace TaskManager.Model.TaskStatuses
{
    internal static class TaskStatusesInstances
    {
        /// <summary>Статус задачи "Ожидает принятия"</summary>
        internal static WaitingStatus WaitingStatus { get; } = new();

        /// <summary>Статус задачи "Текущее"</summary>
        internal static BeginningStatus BeginingStatus { get; } = new();

        /// <summary>Статус задачи "Завершено"</summary>
        internal static CompletedStatus CompletedStatus { get; } = new();

        /// <summary>Статус задачи "Отложено"</summary>
        internal static DeferredStatus DeferredStatus { get; } = new();

        /// <summary>Статус задачи "Отклонено"</summary>
        internal static RejectedStatus RejectedStatus { get; } = new();

        /// <summary>Статус задачи "Выполнено"</summary>
        internal static DoneStatus DoneStatus { get; } = new();
    }
}
