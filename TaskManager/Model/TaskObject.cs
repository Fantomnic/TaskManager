using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static TaskManager.Enums;

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
        private Section _section;

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

        public Section Section
        {
            get => _section;
            set
            {
                _section = value;
                OnPropertyChanged(nameof(Section));
            }
        }

        #endregion Свойства
    }
}
