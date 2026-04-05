using System.Collections.ObjectModel;

namespace TaskManager.Model
{
    internal abstract class Section : BaseObject
    {
        public Section(string name)
        {
            Name = name;
        }

        internal abstract bool IsMasterSection { get; }

        /// <summary>Все задачи раздела</summary>
        internal ObservableCollection<TaskObject> Tasks { get; } = [];

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal static TaskObject CreateTask() => CreateTask(Settings.GetDefaultTaskName());

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal static TaskObject CreateTask(string name) => new() { Name = name };

        /// <summary>Добавить задачу в раздел</summary>
        internal virtual bool AddTask(TaskObject newTask, bool throwOnError = false)
        {
            if (Tasks.Contains(newTask))
            {
                if (throwOnError)
                    throw new InvalidOperationException($"Задача \"{newTask}\" уже добавлена в раздел \"{this}\"");

                return false;
            }

            Tasks.Add(newTask);
            return true;
        }

        /// <summary>Удалить задачу из раздела</summary>
        internal virtual bool RemoveTask(TaskObject task) => Tasks.Remove(task);
    }
}
