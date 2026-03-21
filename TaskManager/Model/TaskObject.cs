using TaskManager.Helpers;
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
        private Enums.TaskStatus _status;
        private TaskPriority _priority;
        private Section? _section;

        internal TaskObject()
        {
            _creationDate = DateTime.Now;
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

        public Enums.TaskStatus Status
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

        #endregion Свойства

        internal void ChangeToSection(Section? newSection)
        {
            if (!Helper.IsBaseSection(AdditionalSection))
                AdditionalSection!.RemoveTask(this);

            if (!Helper.IsBaseSection(newSection))
                newSection!.AddTask(this);
        }
    }
}
