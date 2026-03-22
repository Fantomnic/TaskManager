using System.Collections.ObjectModel;
using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления раздела</summary>
    internal class SectionViewModel : BaseViewModel
    {
        private TaskObjectViewModel? _selectedTaskViewModel;
        private Visibility _visibilityEmptyTaskPropertyImage;

        public SectionViewModel(Section section)
        {
            Section = section;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SetVisibilityEmptyTaskImage();
        }

        internal Section Section { get; }

        internal bool IsBaseSection => Section.IsBaseSection;

        /// <summary>Список моделей представления задач раздела</summary>
        public ObservableCollection<TaskObjectViewModel> TasksViewModels { get; } = [];

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

        /// <summary>Выбранная задача (модель представления)</summary>
        public TaskObjectViewModel? SelectedTaskViewModel
        {
            get => _selectedTaskViewModel;
            set
            {
                _selectedTaskViewModel = value;
                SetVisibilityEmptyTaskImage();
                OnPropertyChanged(nameof(SelectedTaskViewModel));
            }
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
        internal static TaskObjectViewModel CreateTask(string? name = null)
        {
            var newTask = name is null ? Section.CreateTask() : Section.CreateTask(name);
            return new(newTask);
        }

        /// <summary>Добавить задачу в раздел (с соответствующими моделями представления)</summary>
        internal void AddTask(TaskObject newTask, TaskObjectViewModel? newTaskViewModel = null)
        {
            Section.AddTask(newTask);

            newTaskViewModel ??= new TaskObjectViewModel(newTask);
            AddTaskViewModel(newTaskViewModel);
        }

        private void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            if (!IsBaseSection)
            {
                if (!Helper.GetAllTasksViewModels().Contains(newTaskViewModel))
                    Helper.BaseSectionViewModel.AddTaskViewModel(newTaskViewModel);

                newTaskViewModel.AdditionalSection = this;
            }

            TasksViewModels.Add(newTaskViewModel);
        }

        /// <summary>Удалить задачу из раздела (с соответствующей моделью представления)</summary>
        internal bool RemoveTask(TaskObject taskObject, TaskObjectViewModel? taskViewModel = null)
        {
            taskViewModel ??= FindTaskViewModel(taskObject);

            return Section.RemoveTask(taskObject)
                && taskViewModel is not null
                && TasksViewModels.Remove(taskViewModel);
        }

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedTaskViewModel is null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Пересчитать видимость команды контекстного меню "Изменить раздел" для указанной задачи</summary>
        internal static void RefreshChangeSectionEnabled(TaskObjectViewModel taskObjectViewModel)
        {
            var mainViewModel = Helper.MainViewModel;
            var availableSections = mainViewModel.GetSectionsViewModelsForChanging(taskObjectViewModel.TaskObject);

            // --- Доступность ---
            // Из основного раздела:
            // - Должны быть неосновные разделы, в которых не содержится переданная задача
            // Из неосновного раздела:
            // - Всегда
            taskObjectViewModel.ChangeSectionEnabled = !Helper.IsBaseSection(mainViewModel.SelectedSectionViewModel.Section) || availableSections.Count > 0;
        }

        private TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => TasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
