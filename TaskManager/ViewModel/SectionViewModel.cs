using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления раздела</summary>
    internal class SectionViewModel : BaseViewModel
    {
        private TaskObjectViewModel? _selectedTask;
        private Visibility _visibilityEmptyTaskPropertyImage;

        public SectionViewModel(string name, bool baseSection = false)
        {
            Section = baseSection ? new BaseSection(name) : new Section(name);
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SetVisibilityEmptyTaskImage();
        }

        internal Section Section { get; }

        internal bool IsBaseSection => Section.IsBaseSection;

        /// <summary>Список задач раздела</summary>
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

        /// <summary>Выбранная задача</summary>
        public TaskObjectViewModel? SelectedTaskViewModel
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
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

        internal void AddTask(TaskObjectViewModel newTaskViewModel)
        {
            if (!IsBaseSection)
            {
                if (!Helper.GetAllTasksViewModels().Contains(newTaskViewModel))
                    Helper.BaseSectionViewModel.AddTask(newTaskViewModel);

                newTaskViewModel.AdditionalSection = this;
            }

            TasksViewModels.Add(newTaskViewModel);
            Section.AddTask(newTaskViewModel.TaskObject);
        }

        internal void RemoveTask(TaskObjectViewModel taskViewModel)
        {
            if (TasksViewModels.Remove(taskViewModel))
                taskViewModel.AdditionalSection = null;
        }

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedTaskViewModel is null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Пересчитать видимость команды контекстного меню "Изменить раздел" для указанной задачи</summary>
        internal static void RefreshChangeSectionEnabled(TaskObjectViewModel taskObjectViewModel)
        {
            var taskSectionViewModel = taskObjectViewModel.AdditionalSection;
            var mainViewModel = Helper.MainViewModel;
            var availableSections = mainViewModel.GetSectionsViewModelsForChanging(taskSectionViewModel);

            // --- Доступность ---
            // Из основного раздела:
            // - Должны быть неосновные разделы, в которых не содержится переданная задача
            // Из неосновного раздела:
            // - Всегда
            taskObjectViewModel.ChangeSectionEnabled = !Helper.IsBaseSection(mainViewModel.SelectedSectionViewModel) || availableSections.Count > 0;
        }
    }
}
