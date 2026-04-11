namespace TaskManager.Model.TaskPriorities
{
    internal static class TaskPrioritiesInstances
    {
        public static List<TaskPriorityBase> AllPriorities =>
            [
                LowPriority,
                MiddlePriority,
                HighPriority,
            ];

        /// <summary>Приоритет "Низкий"</summary>
        internal static LowPriority LowPriority { get; } = new();

        /// <summary>Приоритет "Средний"</summary>
        internal static MiddlePriority MiddlePriority { get; } = new();

        /// <summary>Приоритет "Высокий"</summary>
        internal static HighPriority HighPriority { get; } = new();

        internal static void ResetForegrounds()
        {
            foreach (var priority in AllPriorities)
                priority.ResetForeground();
        }
    }
}
