using TaskManager.Model.TaskPriorities;
using TaskManager.Model.TaskStatuses;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    // TODO: Нужно ли всё-таки тут INotifyPropertyChanged - разобраться
    internal class TaskObject : BaseObject
    {
        internal TaskObject(TaskPriorityBase priority, TaskType type)
        {
            CreationDate = DateTime.Now;
            Status = TaskStatusesInstances.WaitingStatus;
            Priority = priority;
            Type = type;
        }

        #region Свойства

        internal DateTime CreationDate { get; set; }

        internal DateTime EndDate { get; set; }

        internal TaskType Type { get; set; }

        internal TaskStatusBase Status { get; set; }

        internal TaskPriorityBase Priority { get; set; }

        internal string Description { get; set; }

        internal string Comment { get; set; }

        /// <summary>Неосновной раздел, к которому принадлежит задача</summary>
        /// <remarks>По умолчанию все задачи хранятся в базовом разделе. Если значение = null, то, кроме базового, ни в каком другом разделе её нет</remarks> 
        internal AdditionalSection? AdditionalSection { get; set; }

        internal bool IsNew { get; set; } = true;

        internal TaskObject? Parent { get; set; }

        internal List<TaskObject> Children { get; set; } = [];

        #endregion Свойства

        // TODO: Сделать отдельные типы исключений с типом Инфо
        internal void AddChild(TaskObject child)
        {
            if (Children.Contains(child))
                throw new InvalidOperationException("Данная подзадача уже добавлена");

            if (GetAllParents(this).Contains(child))
                throw new InvalidOperationException("Данная подзадача содержит задачу, в которую происходит добавление");

            Children.Add(child);
        }

        /// <summary>Удалить подзадачу</summary>
        /// <remarks>Подзадачи удалённой подзадачи остаются у неё</remarks>
        internal bool RemoveChild(TaskObject child) => Children.Remove(child);

        private static List<TaskObject> GetAllParents(TaskObject taskObject)
        {
            if (taskObject.Parent is not TaskObject parent)
                return [];

            var result = new List<TaskObject>() { parent };
            result.AddRange(GetAllParents(parent));

            return result;
        }
    }
}
