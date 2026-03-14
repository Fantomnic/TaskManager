namespace TaskManager.Helpers
{
    public class Enums
    {
        /// <summary>Тип задачи</summary>
        public enum TaskType
        {
            /// <summary>Одноразовая</summary>
            Once,
            /// <summary>Ежедневная</summary>
            Daily,
            /// <summary>Еженедельная</summary>
            Weekly,
            /// <summary>Периодическая</summary>
            Regular,
            /// <summary>Долгосрочная</summary>
            LongTime,
        }

        /// <summary>Статус задачи</summary>
        public enum TaskStatus
        {
            /// <summary>Ожидание принятия</summary>
            None,
            /// <summary>Текущее</summary>
            Begining,
            /// <summary>Выполнено</summary>
            Completed,
            /// <summary>Отложено</summary>
            Deferred,
            /// <summary>Отклонено</summary>
            Rejected,
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
