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
        private AdditionalSectionViewModel? _additionalSection;
        private bool _changeSectionEnabled;
        private TaskObjectViewModel? _parentViewModel;
        private List<TaskObjectViewModel> _childrenViewModels = [];
        private bool _isSelected;

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

        public AdditionalSectionViewModel? AdditionalSection
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

        public TaskObjectViewModel? ParentViewModel
        {
            get => _parentViewModel;
            set
            {
                _parentViewModel = value;
                OnPropertyChanged(nameof(ParentViewModel));
            }
        }

        public List<TaskObjectViewModel> ChildrenViewModels
        {
            get => _childrenViewModels;
            set
            {
                _childrenViewModels = value;
                OnPropertyChanged(nameof(ChildrenViewModels));
            }
        }

        // Используется только для дерева
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool AcceptCommandVisibility => Helper.GetCommandInstance<AcceptTaskCommand>().CanChange(TaskObject);

        public bool RejectCommandVisibility => Helper.GetCommandInstance<RejectTaskCommand>().CanChange(TaskObject);

        public bool DeferCommandVisibility => Helper.GetCommandInstance<DeferTaskCommand>().CanChange(TaskObject);

        public bool DoneCommandVisibility => Helper.GetCommandInstance<DoneTaskCommand>().CanChange(TaskObject);

        public bool CompleteCommandVisibility => Helper.GetCommandInstance<CompleteTaskCommand>().CanChange(TaskObject);

        internal void MoveToSection(AdditionalSectionViewModel newSectionViewModel)
        {
            var additionalSection = TaskObject.AdditionalSection;

            if (!Helper.IsMasterSection(additionalSection) && Helper.MainViewModel.FindSectionViewModel(additionalSection) is AdditionalSectionViewModel additionalSectionViewModel)
                additionalSectionViewModel.RemoveTaskViewModel(this);

            if (!Helper.IsMasterSection(newSectionViewModel.Section))
                newSectionViewModel!.AddTaskViewModel(this);
        }

        /// <summary>Добавить подзадачу к задаче (с соответствующей моделью представления)</summary>
        internal void AddChildViewModel(TaskObjectViewModel childViewModel)
        {
            TaskObject.AddChild(childViewModel.TaskObject);

            ChildrenViewModels.Add(childViewModel);
            childViewModel.ParentViewModel = this;

            //var currentSection = Helper.MainViewModel.SelectedSectionViewModel;

            //Children.Add(child);
            //child.Parent = this;
        }

        internal TaskObjectViewModel GetRootTaskViewModel()
            => ParentViewModel is null ? this : ParentViewModel.GetRootTaskViewModel();
    }
}
