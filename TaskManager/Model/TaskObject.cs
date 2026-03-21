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
        private Section? _section;
        private bool _changeSectionEnabled;

        internal TaskObject()
        {
            _creationDate = DateTime.Now;
            _status = TaskStatusesInstances.WaitingStatus;
        }

        #region Свойства

        // Используется для привязки к параметру команды в контекстном меню, которое определено в стиле шаблона объекта
        public TaskObject Instance => this;

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
        public Section? AdditionalSection
        {
            get => _section;
            set
            {
                _section = value;
                OnPropertyChanged(nameof(AdditionalSection));
            }
        }

        /// <summary>Доступность команды контекстного меню "Изменить раздел"</summary>
        public bool ChangeSectionEnabled
        {
            get => _changeSectionEnabled;
            set
            {
                _changeSectionEnabled = value;
                OnPropertyChanged(nameof(ChangeSectionEnabled));
            }
        }

        public bool AcceptCommandVisibility => Status.IsAcceptCommandVisible();

        #endregion Свойства

        internal void MoveToSection(Section? newSection)
        {
            if (!Helper.IsBaseSection(AdditionalSection))
                AdditionalSection!.RemoveTask(this);

            if (!Helper.IsBaseSection(newSection))
                newSection!.AddTask(this);
        }
    }
}
