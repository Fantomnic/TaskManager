using System.Collections.ObjectModel;
using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskObject = TaskManager.Model.TaskObject;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления раздела</summary>
    internal class SectionViewModel : BaseViewModel
    {
        private Section _section;
        private TaskObject? _selectedObject;
        private TaskObjectViewModel? _taskObjectViewModel;
        private Visibility _visibilityEmptyTaskPropertyImage;

        public SectionViewModel(string name, bool baseSection = false)
        {
            _section = baseSection ? new BaseSection(name) : new Section(name);
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SetVisibilityEmptyTaskImage();
        }

        internal Section Section => _section;

        /// <summary>Список задач раздела</summary>
        public ObservableCollection<TaskObject> Tasks => Section.Tasks;

        /// <summary>Наименование раздела</summary>
        public string Name
        {
            get => _section.Name;
            set
            {
                _section.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>Окно свойтсв для текущей выбранной задачи</summary>
        public TaskObjectViewModel? TaskObjectViewModel
        {
            get => _taskObjectViewModel;
            set
            {
                _taskObjectViewModel = value;
                OnPropertyChanged(nameof(TaskObjectViewModel));
            }
        }

        /// <summary>Выбранный объект в списке объектов</summary>
        public TaskObject? SelectedObject
        {
            get => _selectedObject;
            set
            {
                _selectedObject = value;
                TaskObjectViewModel = _selectedObject is null ? null : new(_selectedObject);

                SetVisibilityEmptyTaskImage();
                OnPropertyChanged(nameof(SelectedObject));
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

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedObject is null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Пересчитать видимость команды контекстного меню "Изменить раздел" для указанной задачи</summary>
        internal static void RefreshChangeSectionEnabled(TaskObject taskObject)
        {
            var taskSection = taskObject.AdditionalSection;
            var mainViewModel = Helper.MainViewModel;
            var availableSections = mainViewModel.GetSectionsForChanging(taskSection);

            // --- Доступность ---
            // Из основного раздела:
            // - Должны быть неосновные разделы, в которых не содержится переданная задача
            // Из неосновного раздела:
            // - Всегда
            taskObject.ChangeSectionEnabled = !Helper.IsBaseSection(mainViewModel.SelectedSection) || availableSections.Count > 0;
        }
    }
}
