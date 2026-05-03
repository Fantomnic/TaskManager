using System.Runtime.Serialization;
using TaskManager.Helpers.Exceptions;
using TaskManager.Model.BaseClasses;
using TaskManager.Model.TaskPriorities;
using TaskManager.Resources;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    [DataContract]
    public abstract class Section : BaseObject
    {
        public Section(string name) : base()
        {
            Name = name;
            DefaultPriority = TaskPrioritiesInstances.LowPriority;
        }

        #region Свойства

        internal override string FileName => Guid.ToString() + Constants.SectionDataExtension;

        internal abstract bool IsMasterSection { get; }

        [DataMember]
        /// <summary>Все задачи раздела</summary>
        internal List<TaskObject> Tasks { get; private set; } = [];

        [DataMember]
        internal TaskType DefaultTaskType { get; set; }

        internal TaskPriorityBase DefaultPriority { get; set; }

        [DataMember]
        internal int DefaultPriorityID
        {
            get => DefaultPriority.ID;
            set => DefaultPriority = TaskPrioritiesInstances.AllPriorities.First(p => p.ID == value);
        }

        [DataMember]
        internal string Comment { get; set; }

        [DataMember]
        internal int DefaultReleaseDays { get; set; }

        #endregion Свойства

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal TaskObject CreateTask() => CreateTask(Settings.GetDefaultTaskName());

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal TaskObject CreateTask(string name) => new(DefaultPriority, DefaultTaskType) { Name = name, EndDate = DateTime.Now.AddDays(DefaultReleaseDays).Date };

        /// <summary>Добавить задачу в раздел</summary>
        internal virtual bool AddTask(TaskObject newTask, bool throwOnError = false)
        {
            if (ContainsTask(newTask))
            {
                if (throwOnError)
                    throw new WarningException($"Задача \"{newTask}\" уже добавлена в раздел \"{this}\"");

                return false;
            }

            Tasks.Add(newTask);
            return true;
        }

        /// <summary>Удалить задачу из раздела</summary>
        internal virtual bool RemoveTask(TaskObject task) => Tasks.RemoveAll(t => t.Guid == task.Guid) > 0;

        internal bool ContainsTask(TaskObject task) => Tasks.Contains(task, new BaseComparer());
    }
}
