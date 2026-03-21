namespace TaskManager.Model.TaskStatuses
{
    internal static class TaskStatusesInstances
    {
        static TaskStatusesInstances()
        {
            WaitingStatus = new WaitingStatus();
            BeginingStatus = new BeginningStatus();
            CompletedStatus = new CompletedStatus();
            DeferredStatus = new DeferredStatus();
            RejectedStatus = new RejectedStatus();
        }

        /// <summary>Статус задачи "Ожидает принятия"</summary>
        internal static WaitingStatus WaitingStatus { get; }

        /// <summary>Статус задачи "Текущее"</summary>
        internal static BeginningStatus BeginingStatus { get; }

        /// <summary>Статус задачи "Выполнено"</summary>
        internal static CompletedStatus CompletedStatus { get; }

        /// <summary>Статус задачи "Отложено"</summary>
        internal static DeferredStatus DeferredStatus { get; }

        /// <summary>Статус задачи "Отклонено"</summary>
        internal static RejectedStatus RejectedStatus { get; }
    }
}
