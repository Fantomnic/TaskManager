using System.Runtime.Serialization;
using TaskManager.Model.BaseClasses;
using TaskManager.Model.TaskPriorities;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    [Serializable]
    internal abstract class Section : BaseObject
    {
        protected Section(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Tasks = (List<TaskObject>)info.GetValue(nameof(Tasks), typeof(List<TaskObject>));
            DefaultTaskType = (TaskType)info.GetValue(nameof(DefaultTaskType), typeof(TaskType));

            int defaultPriorityID = info.GetInt32(nameof(DefaultPriorityID));
            DefaultPriority = TaskPrioritiesInstances.GetTaskPriority(defaultPriorityID);

            Comment = info.GetString(nameof(Comment));
        }

        public Section(string name) : base()
        {
            Name = name;
            DefaultPriority = TaskPrioritiesInstances.LowPriority;
        }

        #region Свойства

        internal abstract bool IsMasterSection { get; }

        /// <summary>Все задачи раздела</summary>
        internal List<TaskObject> Tasks { get; } = [];

        internal TaskType DefaultTaskType { get; set; }

        internal TaskPriorityBase DefaultPriority { get; set; }

        internal int DefaultPriorityID => DefaultPriority.ID;

        internal string Comment { get; set; }

        #endregion Свойства

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal TaskObject CreateTask() => CreateTask(Settings.GetDefaultTaskName());

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal TaskObject CreateTask(string name) => new(DefaultPriority, DefaultTaskType) { Name = name };

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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Tasks), Tasks);
            info.AddValue(nameof(DefaultTaskType), DefaultTaskType);
            info.AddValue(nameof(DefaultPriorityID), DefaultPriorityID);
            info.AddValue(nameof(Comment), Comment);
        }
    }
}
