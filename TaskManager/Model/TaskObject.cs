using System.Runtime.Serialization;
using TaskManager.Model.BaseClasses;
using TaskManager.Model.TaskPriorities;
using TaskManager.Model.TaskStatuses;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    [Serializable]
    internal class TaskObject : BaseObject
    {
        protected TaskObject(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            CreationDate = info.GetDateTime(nameof(CreationDate));
            EndDate = info.GetDateTime(nameof(EndDate));
            Type = (TaskType)info.GetValue(nameof(Type), typeof(TaskType));

            int taskStatusID = info.GetInt32(nameof(TaskStatusID));
            Status = TaskStatusesInstances.GetTaskStatus(taskStatusID);

            int taskPriorityID = info.GetInt32(nameof(TaskPriorityID));
            Priority = TaskPrioritiesInstances.GetTaskPriority(taskPriorityID);

            Description = info.GetString(nameof(Description));
            Comment = info.GetString(nameof(Comment));
            //AdditionalSection = (AdditionalSection?)info.GetValue(nameof(AdditionalSection), typeof(AdditionalSection));
            Parent = (TaskObject?)info.GetValue(nameof(Parent), typeof(TaskObject));
            Children = (List<TaskObject>)info.GetValue(nameof(Children), typeof(List<TaskObject>));
        }

        internal TaskObject(TaskPriorityBase priority, TaskType type) : base()
        {
            CreationDate = DateTime.Now;
            Status = TaskStatusesInstances.WaitingStatus;
            Priority = priority;
            Type = type;
            IsNew = true;
        }

        #region Свойства

        internal DateTime CreationDate { get; set; }

        internal DateTime EndDate { get; set; }

        internal TaskType Type { get; set; }

        internal TaskStatusBase Status { get; set; }

        internal int TaskStatusID => Status.ID;

        internal TaskPriorityBase Priority { get; set; }

        internal int TaskPriorityID => Priority.ID;

        internal string Description { get; set; }

        internal string Comment { get; set; }

        /// <summary>Неосновной раздел, к которому принадлежит задача</summary>
        /// <remarks>По умолчанию все задачи хранятся в базовом разделе. Если значение = null, то, кроме базового, ни в каком другом разделе её нет</remarks> 
        internal AdditionalSection? AdditionalSection { get; set; }

        internal bool IsNew { get; set; }

        internal TaskObject? Parent { get; set; }

        internal List<TaskObject> Children { get; set; } = [];

        #endregion Свойства

        // TODO: Сделать отдельные типы исключений с типом Инфо
        internal void AddChild(TaskObject child, bool throwOnError = true)
        {
            if (ContainsChild(child))
            {
                if (throwOnError)
                    throw new InvalidOperationException("Данная подзадача уже добавлена");

                return;
            }

            if (GetAllParents(this).Any(p => p.ContainsChild(child)))
            {
                if (throwOnError)
                    throw new InvalidOperationException("Данная подзадача содержит задачу, в которую происходит добавление");

                return;
            }

            Children.Add(child);
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(CreationDate), CreationDate);
            info.AddValue(nameof(EndDate), EndDate);
            info.AddValue(nameof(Type), Type);
            info.AddValue(nameof(TaskStatusID), TaskStatusID);
            info.AddValue(nameof(TaskPriorityID), TaskPriorityID);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(Comment), Comment);
            //info.AddValue(nameof(AdditionalSection), AdditionalSection);
            info.AddValue(nameof(Parent), Parent);
            info.AddValue(nameof(Children), Children);
        }
    }
}
