using System.Windows;
using TaskManager.Model;
using TaskManager.Model.BaseClasses;
using TaskManager.Model.TaskPriorities;
using static TaskManager.Helpers.Enums;

namespace TaskManager.ViewModels
{
    /// <summary>Общий класс для моделей представления разделов</summary>
    internal abstract class SectionViewModel : BaseViewModel
    {
        private TaskObjectViewModel? _selectedTaskViewModel;
        private Visibility _visibilityEmptyTaskPropertyImage;

        public SectionViewModel(Section section)
        {
            Section = section;
            SetVisibilityEmptyTaskImage();
        }

        internal Section Section { get; }

        internal bool IsMasterSection => Section.IsMasterSection;

        public bool IsNew { get; internal set; } = true;

        /// <summary>Наименование раздела</summary>
        public string Name
        {
            get => Section.Name;
            set
            {
                Section.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public TaskType DefaultTaskType
        {
            get => Section.DefaultTaskType;
            set
            {
                Section.DefaultTaskType = value;
                OnPropertyChanged(nameof(DefaultTaskType));
            }
        }

        public TaskPriorityBase DefaultPriority
        {
            get => Section.DefaultPriority;
            set
            {
                Section.DefaultPriority = value;
                OnPropertyChanged(nameof(DefaultPriority));
            }
        }

        public string Comment
        {
            get => Section.Comment;
            set
            {
                Section.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        /// <summary>Выбранная задача (модель представления)</summary>
        public TaskObjectViewModel? SelectedTaskViewModel
        {
            get => _selectedTaskViewModel;
            set
            {
                if (_selectedTaskViewModel == value)
                    return;

                // Нужно для снятия фокуса при щелчке по пустому месту
                if (value is null)
                    SetTaskIsSelected(false);

                _selectedTaskViewModel = value;

                // Нужно для установки фокуса при добавлении задачи
                if (!IsMasterSection)
                    SetTaskIsSelected(true);

                SetVisibilityEmptyTaskImage();
                OnPropertyChanged(nameof(SelectedTaskViewModel));
            }
        }

        // Отображается в свойствах, в момент, когда нельзя изменять кол-во задач
        public string TasksCount => Section.Tasks.Count.ToString();

        private void SetTaskIsSelected(bool isSelected)
        {
            if (_selectedTaskViewModel is not null)
                _selectedTaskViewModel.IsSelected = isSelected;
        }

        /// <summary>Видимость "пустого" окна свойств</summary>
        public Visibility VisibilityEmptyTaskImage
        {
            get => _visibilityEmptyTaskPropertyImage;
            set
            {
                _visibilityEmptyTaskPropertyImage = value;
                OnPropertyChanged(nameof(VisibilityEmptyTaskImage));
            }
        }

        /// <summary>Создать новую задачу без привязки к разделу (с соответствующей моделью представления)</summary>
        internal TaskObjectViewModel CreateTask(string? name = null)
        {
            var newTask = name is null ? Section.CreateTask() : Section.CreateTask(name);
            return CreateTaskViewModel(newTask);
        }

        internal TaskObjectViewModel CreateTaskViewModel(TaskObject taskObject) => new(taskObject);

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedTaskViewModel is null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Добавить задачу в раздел</summary>
        internal abstract void AddTaskViewModel(TaskObjectViewModel newTaskViewModel);

        /// <summary>Удалить задачу из раздела</summary>
        /// <param name="removeChildren">Удалить все подзадачи из раздела (при этом они останутся как подзадачи для родительской задачи)</param>
        internal abstract bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel, bool removeChildren = false);

        internal abstract TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject);

        internal abstract void RefreshVisibleTaskViewModels();

        protected List<TaskObjectViewModel> GetFilteredTaskViewModels(List<TaskObjectViewModel> sourceList)
        {
            return sourceList.FindAll(t => t.TaskStatus.TaskVisible
                && (!Settings.Instanse.ShowTodayTasks || t.TaskObject.CreationDate.Date == DateTime.Now.Date));
        }

        public static bool operator ==(SectionViewModel sectionViewModel1, SectionViewModel sectionViewModel2) => sectionViewModel1?.Section == sectionViewModel2?.Section;

        public static bool operator !=(SectionViewModel sectionViewModel1, SectionViewModel sectionViewModel2) => sectionViewModel1?.Section != sectionViewModel2?.Section;
    }
}
