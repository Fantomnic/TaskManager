using TaskManager.Helpers;
using TaskManager.Model.TaskStatuses;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    // TODO: Нужно ли всё-таки тут INotifyPropertyChanged - разобраться
    internal class TaskObject : BaseObject
    {
        private Guid _guid;
        private DateTime _creationDate;
        private DateTime _endDate;
        private TaskType _type;
        private TaskStatusBase _status;
        private TaskPriority _priority;
        private AdditionalSection? _additionalSection;
        private bool _isNew;
        private TaskObject? _parent;
        private List<TaskObject> _children = [];

        internal TaskObject()
        {
            _creationDate = DateTime.Now;
            _status = TaskStatusesInstances.WaitingStatus;
            _isNew = true;
        }

        #region Свойства

        // public, чтобы UI видел значение при биндинге
        public DateTime CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                OnPropertyChanged(nameof(CreationDate));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public TaskType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public TaskStatusBase Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public TaskPriority Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        /// <summary>Неосновной раздел, к которому принадлежит задача</summary>
        /// <remarks>По умолчанию все задачи хранятся в базовом разделе. Если значение = null, то, кроме базового, ни в каком другом разделе её нет</remarks> 
        public AdditionalSection? AdditionalSection
        {
            get => _additionalSection;
            set
            {
                _additionalSection = value;
                OnPropertyChanged(nameof(AdditionalSection));
            }
        }

        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                OnPropertyChanged(nameof(IsNew));
            }
        }

        public TaskObject? Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                OnPropertyChanged(nameof(Parent));
            }
        }

        public List<TaskObject> Children
        {
            get => _children;
            set
            {
                _children = value;
                OnPropertyChanged(nameof(Children));
            }
        }

        #endregion Свойства

        internal void AddChild(TaskObject child)
        {
            if (Children.Contains(child))
                throw new InvalidOperationException("Данная подзадача уже добавлена");

            if (GetAllParents(this).Contains(child))
                throw new InvalidOperationException("Данная подзадача содержит задачу, в которую происходит добавление");

            Children.Add(child);

            if (child.Parent is TaskObject parent)
                parent.Children.Remove(child);

            child.Parent = this;
        }

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
