namespace TaskManager.Model.TaskStatuses
{
    public static class TaskStatusesInstances
    {
        private static List<TaskStatusBase> AllStatuses =>
            [
                WaitingStatus,
                BeginingStatus,
                CompletedStatus,
                DeferredStatus,
                RejectedStatus,
                DoneStatus,
            ];

        /// <summary>Статус задачи "Ожидает принятия"</summary>
        public static WaitingStatus WaitingStatus { get; } = new();

        /// <summary>Статус задачи "Текущее"</summary>
        public static BeginningStatus BeginingStatus { get; } = new();

        /// <summary>Статус задачи "Завершено"</summary>
        public static CompletedStatus CompletedStatus { get; } = new();

        /// <summary>Статус задачи "Отложено"</summary>
        public static DeferredStatus DeferredStatus { get; } = new();

        /// <summary>Статус задачи "Отклонено"</summary>
        public static RejectedStatus RejectedStatus { get; } = new();

        /// <summary>Статус задачи "Выполнено"</summary>
        public static DoneStatus DoneStatus { get; } = new();

        internal static void ResetBackgrounds()
        {
            foreach (var status in AllStatuses)
                status.ResetBackground();
        }

        internal static TaskStatusBase GetTaskStatus(int id) => AllStatuses.First(s => s.ID == id);
    }
}
