using System.Collections.ObjectModel;
using System.Windows;
using TaskManager.Model;
using TaskObject = TaskManager.Model.TaskObject;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления раздела</summary>
    internal class SectionViewModel : BaseViewModel
    {
        private TaskObject _selectedObject;
        private Section _section;
        private TaskObjectViewModel _taskObjectViewModel;
        private Visibility _visibilityEmptyTaskPropertyImage;

        public SectionViewModel(string name)
        {
            _section = new(name);
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            Tasks =
                [
                    new() { Name = "Test 1", CreationDate = DateTime.Now },
                    new() { Name = "Test 2", CreationDate = new DateTime(2000, 8, 15) }
                ];

            SetVisibilityEmptyTaskImage();
        }

        /// <summary>Окно свойтсв для текущей выбранной задачи</summary>
        public TaskObjectViewModel TaskObjectViewModel
        {
            get => _taskObjectViewModel;
            set
            {
                _taskObjectViewModel = value;
                OnPropertyChanged(nameof(TaskObjectViewModel));
            }
        }

        /// <summary>Выбранный объект в списке объектов</summary>
        public TaskObject SelectedObject
        {
            get => _selectedObject;
            set
            {
                _selectedObject = value;
                TaskObjectViewModel = new(_selectedObject);
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

        /// <summary>Список задач раздела</summary>
        public ObservableCollection<TaskObject> Tasks { get; set; }

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedObject is null ? Visibility.Visible : Visibility.Collapsed;
    }
}
