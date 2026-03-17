using TaskManager.Helpers;
using TaskManager.Model;
using static TaskManager.Helpers.Enums;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления свойств задачи</summary>
    internal class TaskObjectViewModel : BaseViewModel
    {
        private TaskObject _taskObject;

        // TODO: можно поместить куда-нибудь в глобальную статику
        static TaskObjectViewModel()
        {
            PriorityList = GetEnumValues<TaskPriority>();
            StatusList = GetEnumValues<Enums.TaskStatus>();
        }

        internal TaskObjectViewModel(TaskObject taskObject)
        {
            _taskObject = taskObject;
        }

        public static IEnumerable<TaskPriority> PriorityList { get; private set; }

        public static IEnumerable<Enums.TaskStatus> StatusList { get; private set; }

        public string Name
        {
            get => _taskObject.Name;
            set
            {
                _taskObject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        // Прим.: Обработку null-значения можно сделать тут, а можно в свойствах привязки через TargetNullValue
        public string CreationDate => _taskObject?.CreationDate.ToString("dd.MM.yyyy");

        // Прим.: Технически, можно настроить связь в событии SelectionChanged - например, если контрол принимает объекты другого типа
        public TaskPriority TaskPriority
        {
            get => _taskObject.Priority;
            set
            {
                _taskObject.Priority = value;
                OnPropertyChanged(nameof(TaskPriority));
            }
        }

        public Enums.TaskStatus TaskStatus
        {
            get => _taskObject.Status;
            set
            {
                _taskObject.Status = value;
                OnPropertyChanged(nameof(TaskStatus));
            }
        }
    }
}
