using System.Collections.ObjectModel;
using TaskManager.Commands;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskPriorities;
using TaskManager.Model.TaskStatuses;
using static TaskManager.Helpers.Enums;

namespace TaskManager.ViewModels
{
    /// <summary>Модель представления свойств задачи</summary>
    public class TaskObjectViewModel : BaseViewModel
    {
        private AdditionalSectionViewModel? _additionalSectionViewModel;
        private bool _changeSectionEnabled;
        private TaskObjectViewModel? _parentViewModel;
        private ObservableCollection<TaskObjectViewModel> _childrenViewModels = [];
        private bool _isSelected;
        private bool _calendarIsEnabled;
        private bool _isExpired;
        private TaskPriorityBase _visibleTaskPriority;

        internal TaskObjectViewModel(TaskObject taskObject)
        {
            TaskObject = taskObject;

            try
            {
                ChangeMainPriority = false;
                ChangePriority(taskObject.Priority);
            }
            finally
            {
                ChangeMainPriority = true;
            }

            RefreshCalendarIsEnabled();
        }

        internal TaskObject TaskObject { get; }

        public List<TaskStatusBase> StatusList => TaskObject.Status.Transitions;

        internal static bool ChangeMainPriority { get; set; } = true;

        #region Свойства привязки

        public string Name
        {
            get => TaskObject.Name;
            set
            {
                TaskObject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DateTime CreationDate
        {
            get => TaskObject.CreationDate;
            set
            {
                TaskObject.CreationDate = value;
                OnPropertyChanged(nameof(CreationDate));
            }
        }

        public DateTime EndDate
        {
            get => TaskObject.EndDate;
            set
            {
                TaskObject.EndDate = value;
                IsExpired = TaskObject.EndDate < DateTime.Now.Date;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        // Прим.: Технически, можно настроить связь в событии SelectionChanged - например, если контрол принимает объекты другого типа
        public TaskPriorityBase TaskPriority
        {
            get => TaskObject.Priority;
            set
            {
                TaskObject.Priority = value;
                OnPropertyChanged(nameof(TaskPriority));
            }
        }

        public TaskPriorityBase VisibleTaskPriority
        {
            get => _visibleTaskPriority;
            set
            {
                _visibleTaskPriority = value;

                if (ChangeMainPriority)
                    TaskPriority = value;

                OnPropertyChanged(nameof(VisibleTaskPriority));
            }
        }

        public TaskStatusBase TaskStatus
        {
            get => TaskObject.Status;
            set
            {
                var oldValue = TaskObject.Status;

                TaskObject.Status = value;
                OnPropertyChanged(nameof(TaskStatus));

                if (oldValue != value)
                    RefreshAfterChangeStatus();
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

        public string Description
        {
            get => TaskObject.Description;
            set
            {
                TaskObject.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Comment
        {
            get => TaskObject.Comment;
            set
            {
                TaskObject.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public AdditionalSectionViewModel? AdditionalSectionViewModel
        {
            get => _additionalSectionViewModel;
            set
            {
                _additionalSectionViewModel = value;
                OnPropertyChanged(nameof(AdditionalSectionViewModel));
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

        public bool IsExpired
        {
            get => _isExpired;
            set
            {
                _isExpired = value;
                OnPropertyChanged(nameof(IsExpired));
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

        public ObservableCollection<TaskObjectViewModel> ChildrenViewModels
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

        public bool CalendarIsEnabled
        {
            get => _calendarIsEnabled;
            set
            {
                _calendarIsEnabled = value;
                OnPropertyChanged(nameof(CalendarIsEnabled));
            }
        }

        #endregion Свойства привязки

        public bool HasChildren => ChildrenViewModels.Count > 0;

        public bool AcceptCommandVisibility => Helper.GetCommandInstance<AcceptTaskCommand>().CanChange(TaskObject);

        public bool RejectCommandVisibility => Helper.GetCommandInstance<RejectTaskCommand>().CanChange(TaskObject);

        public bool DeferCommandVisibility => Helper.GetCommandInstance<DeferTaskCommand>().CanChange(TaskObject);

        public bool DoneCommandVisibility => Helper.GetCommandInstance<DoneTaskCommand>().CanChange(TaskObject);

        public bool CompleteCommandVisibility => Helper.GetCommandInstance<CompleteTaskCommand>().CanChange(TaskObject);

        private void RefreshAfterChangeStatus()
        {
            RefreshCalendarIsEnabled();

            if (TaskStatus.ResetEndDate)
                ResetEndDate();
        }

        internal void ResetEndDate(int offsetDays = 0)
        {
            EndDate = DateTime.Now.AddDays(offsetDays).Date;
        }

        private void RefreshCalendarIsEnabled()
        {
            CalendarIsEnabled = TaskType != TaskType.LongTime && TaskStatus.CalendarIsEnabled;
        }

        internal void SetAdditionalSectionViewModel(AdditionalSectionViewModel? newSectionViewModel)
        {
            var newSection = newSectionViewModel?.Section as AdditionalSection;

            if (TaskObject.AdditionalSection != newSection)
                TaskObject.AdditionalSection = newSection;

            AdditionalSectionViewModel = newSectionViewModel;
        }

        /// <summary>Переместить задачу в другой раздел</summary>
        /// <remarks>Задача перемещается вместе со всеми подзадачами</remarks>
        internal void MoveToSection(SectionViewModel newSectionViewModel, bool transferFullChain = false)
        {
            var sourceTaskViewModel = transferFullChain ? GetRootTaskViewModel() : this;
            bool toMasterSection = Helper.IsMasterSection(newSectionViewModel.Section);

            // Если переносим из неосновного раздела, то удаляем отсюда всё
            if (AdditionalSectionViewModel is not null)
            {
                AdditionalSectionViewModel.RemoveTaskViewModel(sourceTaskViewModel, toMasterSection);

                if (sourceTaskViewModel.ParentViewModel is TaskObjectViewModel parentViewModel)
                {
                    if (toMasterSection)
                        parentViewModel.RemoveAllChildrenViewModels();
                    else
                        parentViewModel.RemoveChildViewModel(sourceTaskViewModel);
                }
            }

            // Если переносим в неосновной раздел, то добавляем сюда всё
            if (!toMasterSection)
                newSectionViewModel.AddTaskViewModel(sourceTaskViewModel);
        }

        private void SetParentViewModel(TaskObjectViewModel? newParentViewModel)
        {
            var newParent = newParentViewModel?.TaskObject;

            if (TaskObject.Parent != newParent)
                TaskObject.Parent = newParent;

            ParentViewModel = newParentViewModel;
        }

        /// <summary>Добавить подзадачу к задаче</summary>
        internal bool AddChildViewModel(TaskObjectViewModel childViewModel, AddChildrenSettings? addSettings = null)
        {
            addSettings ??= new();

            var child = childViewModel.TaskObject;

            if (child == TaskObject || ChildrenViewModels.Contains(childViewModel))
                return false;

            if (!TaskObject.AddChild(child, addSettings.CheckAllParents, addSettings.ThrowOnError) && addSettings.ReturnIfModelAddIsFalse)
                return false;

            if (addSettings.RemoveFromRoot && childViewModel.AdditionalSectionViewModel is AdditionalSectionViewModel currentSectionViewModel)
                currentSectionViewModel.RemoveRootTaskViewModel(childViewModel, false);

            if (childViewModel.ParentViewModel is TaskObjectViewModel parentViewModel)
                parentViewModel.RemoveChildViewModel(childViewModel);

            ChildrenViewModels.Add(childViewModel);

            childViewModel.SetParentViewModel(this);

            return true;
        }

        /// <summary>Удалить подзадачу</summary>
        /// <remarks>Подзадачи удалённой подзадачи остаются у неё</remarks>
        internal bool RemoveChildViewModel(TaskObjectViewModel childViewModel)
        {
            bool result;

            if (result = TaskObject.RemoveChild(childViewModel.TaskObject))
            {
                ChildrenViewModels.Remove(childViewModel);
                childViewModel.SetParentViewModel(null);
            }

            return result;
        }

        internal void RemoveAllChildrenViewModels(bool removeParent = false, bool recursive = true)
        {
            foreach (var childViewModel in ChildrenViewModels)
            {
                if (recursive)
                    childViewModel.RemoveAllChildrenViewModels(true);
                else
                    childViewModel.SetParentViewModel(null);
            }

            RemoveAllChildrenCore();

            if (removeParent)
                SetParentViewModel(null);
        }

        private void RemoveAllChildrenCore()
        {
            TaskObject.Children.Clear();
            ChildrenViewModels.Clear();
        }

        internal TaskObjectViewModel GetRootTaskViewModel()
            => ParentViewModel is null ? this : ParentViewModel.GetRootTaskViewModel();

        /// <summary>Получить модель представления указанной подзадачи на любом уровне вложенности, если она есть</summary>
        internal TaskObjectViewModel? GetChildTaskViewModel(TaskObject taskObject)
        {
            foreach (var childViewModel in ChildrenViewModels)
            {
                var childTaskObject = childViewModel.TaskObject;

                if (childTaskObject == taskObject)
                    return childViewModel;

                if (childViewModel.GetChildTaskViewModel(taskObject) is TaskObjectViewModel result)
                    return result;
            }

            return null;
        }

        internal void ChangePriority(TaskPriorityBase newPriority)
        {
            if (TaskPriority != newPriority)
                TaskPriority = newPriority;

            VisibleTaskPriority = TaskPrioritiesInstances.GetVisiblePriority(newPriority);
        }

        public static bool operator ==(TaskObjectViewModel? taskObjectViewModel1, TaskObjectViewModel? taskObjectViewModel2)
            => taskObjectViewModel1?.TaskObject == taskObjectViewModel2?.TaskObject;

        public static bool operator !=(TaskObjectViewModel? taskObjectViewModel1, TaskObjectViewModel? taskObjectViewModel2)
            => !(taskObjectViewModel1 == taskObjectViewModel2);
    }
}
