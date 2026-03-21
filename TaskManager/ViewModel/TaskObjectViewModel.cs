using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
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
            //StatusList = GetEnumValues<Enums.TaskStatus>();
        }

        internal TaskObjectViewModel(TaskObject taskObject)
        {
            _taskObject = taskObject;
        }

        public static IEnumerable<TaskPriority> PriorityList { get; private set; }

        public List<TaskStatusBase> StatusList => _taskObject.Status.Transitions;

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
#pragma warning disable CS8603 // Possible null reference return.
        public string CreationDate => _taskObject?.CreationDate.ToString("dd.MM.yyyy");
#pragma warning restore CS8603 // Possible null reference return.

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

        public TaskStatusBase TaskStatus
        {
            get => _taskObject.Status;
            set
            {
                _taskObject.Status = value;
                OnPropertyChanged(nameof(TaskStatus));
            }
        }

        //public List<TaskStatusBase> StatusList
        //{
        //    get => _taskObject.GetStatusesToTransition();
        //    set
        //    {
        //        _taskObject.Status = value;
        //        OnPropertyChanged(nameof(StatusList));
        //    }
        //}
    }
}
