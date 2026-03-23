using TaskManager.Commands;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using static TaskManager.Helpers.Enums;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления свойств задачи</summary>
    internal class TaskObjectViewModel : BaseViewModel
    {
        private SectionViewModel? _additionalSection;
        private bool _changeSectionEnabled;

        // TODO: можно поместить куда-нибудь в глобальную статику
        static TaskObjectViewModel()
        {
            PriorityList = GetEnumValues<TaskPriority>();
            TypeList = GetEnumValues<TaskType>();
        }

        public TaskObjectViewModel()
        {
            TaskObject = new() { Name = Settings.GetDefaultTaskName() };
        }

        internal TaskObjectViewModel(TaskObject taskObject)
        {
            TaskObject = taskObject;
        }

        // Используется для привязки к параметру команды в контекстном меню, которое определено в стиле шаблона объекта
        public TaskObjectViewModel Instance => this;

        internal TaskObject TaskObject { get; }

        public static IEnumerable<TaskPriority> PriorityList { get; private set; }

        public static IEnumerable<TaskType> TypeList { get; private set; }

        public List<TaskStatusBase> StatusList => TaskObject.Status.Transitions;

        public string Name
        {
            get => TaskObject.Name;
            set
            {
                TaskObject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        // Прим.: Обработку null-значения можно сделать тут, а можно в свойствах привязки через TargetNullValue
#pragma warning disable CS8603 // Possible null reference return.
        public string CreationDate => TaskObject?.CreationDate.ToString("dd.MM.yyyy");
#pragma warning restore CS8603 // Possible null reference return.

        // Прим.: Технически, можно настроить связь в событии SelectionChanged - например, если контрол принимает объекты другого типа
        public TaskPriority TaskPriority
        {
            get => TaskObject.Priority;
            set
            {
                TaskObject.Priority = value;
                OnPropertyChanged(nameof(TaskPriority));
            }
        }

        public TaskStatusBase TaskStatus
        {
            get => TaskObject.Status;
            set
            {
                TaskObject.Status = value;
                OnPropertyChanged(nameof(TaskStatus));
            }
        }

        public TaskType TaskType
        {
            get => TaskObject.Type;
            set
            {
                TaskObject.Type = value;
                OnPropertyChanged(nameof(TaskType));
            }
        }

        public SectionViewModel? AdditionalSection
        {
            get => _additionalSection;
            set
            {
                _additionalSection = value;
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

        public bool IsNew
        {
            get => TaskObject.IsNew;
            set
            {
                TaskObject.IsNew = value;
                OnPropertyChanged(nameof(IsNew));
            }
        }

        public bool AcceptCommandVisibility => CommandsInstances.AcceptTaskCommand.CanChange(TaskObject);

        public bool RejectCommandVisibility => CommandsInstances.RejectTaskCommand.CanChange(TaskObject);

        public bool DeferCommandVisibility => CommandsInstances.DeferTaskCommand.CanChange(TaskObject);

        public bool DoneCommandVisibility => CommandsInstances.DoneTaskCommand.CanChange(TaskObject);

        public bool CompleteCommandVisibility => CommandsInstances.CompleteTaskCommand.CanChange(TaskObject);

        internal void MoveToSection(SectionViewModel newSectionViewModel)
        {
            var additionalSection = TaskObject.AdditionalSection;

            if (!Helper.IsBaseSection(additionalSection) && Helper.FindSectionViewModel(additionalSection) is SectionViewModel additionalSectionViewModel)
                additionalSectionViewModel.RemoveTask(TaskObject, this);

            if (!Helper.IsBaseSection(newSectionViewModel.Section))
                newSectionViewModel!.AddTask(TaskObject, this);
        }
    }
}
