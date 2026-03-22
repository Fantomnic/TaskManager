namespace TaskManager.Helpers
{
    public class Enums
    {
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

        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
            => Enum.GetValues(typeof(T)).Cast<T>();
    }
}
