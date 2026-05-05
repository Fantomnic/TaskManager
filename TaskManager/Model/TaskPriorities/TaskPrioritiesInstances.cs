using System.Collections.ObjectModel;
using static TaskManager.Helpers.DataHelper;

namespace TaskManager.Model.TaskPriorities
{
    internal static class TaskPrioritiesInstances
    {
        private static readonly List<TaskPriorityBase> _prioritiesList3;

        private static readonly List<TaskPriorityBase> _prioritiesList5;

        static TaskPrioritiesInstances()
        {
            LowPriority = new();
            PostLowPriority = new();
            MiddlePriority = new();
            PreHighPriority = new();
            HighPriority = new();

            _prioritiesList3 =
            [
                LowPriority,
                MiddlePriority,
                HighPriority,
            ];

            _prioritiesList5 =
            [
                LowPriority,
                PostLowPriority,
                MiddlePriority,
                PreHighPriority,
                HighPriority,
            ];

            AllPriorities = [.. _prioritiesList5];
        }

        // Сначала загружаем все приоритеты, т.к. они используются при инициализации видов - до загрузки данных
        public static ObservableCollection<TaskPriorityBase> AllPriorities { get; }

        /// <summary>Приоритет "Низкий"/"Минимальный"</summary>
        internal static LowPriority LowPriority { get; }

        /// <summary>Приоритет "Пониженный"</summary>
        internal static PostLowPriority PostLowPriority { get; }

        /// <summary>Приоритет "Средний"</summary>
        internal static MiddlePriority MiddlePriority { get; }

        /// <summary>Приоритет "Повышенный"</summary>
        internal static PreHighPriority PreHighPriority { get; }

        /// <summary>Приоритет "Высокий"/"Максимальный"</summary>
        internal static HighPriority HighPriority { get; }

        internal static void ResetPriorities(int setId)
        {
            PrioritySaver.Save();

            AllPriorities.Clear();

            var collection = Settings.PrioritiesSetID == 0 ? _prioritiesList3 : _prioritiesList5;

            foreach (var item in collection)
                AllPriorities.Add(item);

            PrioritySaver.Fill();
        }

        internal static void ResetForegrounds()
        {
            foreach (var priority in AllPriorities)
                priority.ResetForeground();
        }

        internal static TaskPriorityBase GetVisiblePriority(TaskPriorityBase sourcePriority)
            => Settings.PrioritiesSetID == 0 && (sourcePriority.ID == 2 || sourcePriority.ID == 4)
                ? MiddlePriority : sourcePriority;
    }
}
