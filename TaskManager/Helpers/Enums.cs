namespace TaskManager.Helpers
{
    public class Enums
    {
        public static IEnumerable<TaskType> AllTypes { get; } = GetEnumValues<TaskType>();

        public static IEnumerable<TaskPriority> AllPriorities { get; } = GetEnumValues<TaskPriority>();

        public static IEnumerable<Themes> AllThemes { get; } = GetEnumValues<Themes>();

        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
            => Enum.GetValues(typeof(T)).Cast<T>();

        /// <summary>Тип задачи</summary>
        public enum TaskType
        {
            /// <summary>Одноразовая</summary>
            Once,
            /// <summary>Многоразовая</summary>
            Regular,
            /// <summary>Долгосрочная</summary>
            LongTime,
        }

        /// <summary>Приоритет задачи</summary>
        public enum TaskPriority
        {
            /// <summary>Низкий</summary>
            Low,
            /// <summary>Средний</summary>
            Middle,
            /// <summary>Высокий</summary>
            High,
        }

        public enum Themes
        {
            Light,
            Dark,
        }
    }
}
