using System.Runtime.Serialization;
using TaskManager.Helpers.Exceptions;
using TaskManager.Model.BaseClasses;
using TaskManager.Model.TaskPriorities;
using TaskManager.Model.TaskStatuses;
using TaskManager.Resources;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    [DataContract(IsReference = true)]
    public class TaskObject : BaseObject
    {
        internal TaskObject(TaskPriorityBase priority, TaskType type) : base()
        {
            Status = TaskStatusesInstances.WaitingStatus;
            Priority = priority;
            Type = type;
            IsNew = true;
        }

        #region Свойства

        internal override string FileName => Guid.ToString() + Constants.TaskDataExtension;

        [DataMember]
        internal DateTime EndDate { get; set; }

        [DataMember]
        internal TaskType Type { get; set; }

        internal TaskStatusBase Status { get; set; }

        [DataMember]
        internal int TaskStatusID
        {
            get => Status.ID;
            set => Status = TaskStatusesInstances.AllStatuses.First(s => s.ID == value);
        }

        internal TaskPriorityBase Priority { get; set; }

        [DataMember]
        internal int TaskPriorityID
        {
            get => Priority.ID;
            set => Priority = TaskPrioritiesInstances.AllPriorities.First(p => p.ID == value);
        }

        [DataMember]
        internal string Description { get; set; }

        [DataMember]
        internal string Comment { get; set; }

        /// <summary>Неосновной раздел, к которому принадлежит задача</summary>
        /// <remarks>По умолчанию все задачи хранятся в базовом разделе. Если значение = null, то, кроме базового, ни в каком другом разделе её нет</remarks> 
        internal AdditionalSection? AdditionalSection { get; set; }

        internal bool IsNew { get; set; }

        [DataMember]
        internal TaskObject? Parent { get; set; }

        [DataMember]
        internal List<TaskObject> Children { get; set; } = [];

        #endregion Свойства

        internal bool AddChild(TaskObject child, bool checkAllParents = true, bool throwOnError = true)
        {
            if (ContainsChild(child))
            {
                if (throwOnError)
                    throw new WarningException("Данная подзадача уже добавлена");

                return false;
            }

            if (GetAllParents(this).Any(p => checkAllParents && p.ContainsChild(child) || p == child))
            {
                if (throwOnError)
                    throw new WarningException("Данная подзадача содержит задачу, в которую происходит добавление");

                return false;
            }

            Children.Add(child);

            return true;
        }

        /// <summary>Удалить подзадачу</summary>
        /// <remarks>Подзадачи удалённой подзадачи остаются у неё</remarks>
        internal bool RemoveChild(TaskObject child) => Children.Remove(child);

        /// <summary>Получить все задачи, в которые входит указанная задача (от ближайшего родителя до корневого)</summary>
        private static List<TaskObject> GetAllParents(TaskObject taskObject)
        {
            if (taskObject.Parent is not TaskObject parent)
                return [];

            var result = new List<TaskObject>() { parent };
            result.AddRange(GetAllParents(parent));

            return result;
        }

        /// <summary>Указывает, содержит ли задача указанную задачу</summary>
        internal bool ContainsChild(TaskObject task) => Children.Contains(task, new BaseComparer());
    }
}
