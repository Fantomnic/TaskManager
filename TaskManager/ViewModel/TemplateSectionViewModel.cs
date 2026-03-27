using System.Windows;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal abstract class TemplateSectionViewModel : BaseViewModel
    {
        private TaskObjectViewModel? _selectedTaskViewModel;
        private Visibility _visibilityEmptyTaskPropertyImage;

        public TemplateSectionViewModel(Section section)
        {
            Section = section;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SetVisibilityEmptyTaskImage();
        }

        internal Section Section { get; }

        internal bool IsMasterSection => Section.IsMasterSection;

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

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedTaskViewModel is null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Пересчитать видимость команды контекстного меню "Изменить раздел" для указанной задачи</summary>
        internal abstract void RefreshChangeSectionEnabled(TaskObjectViewModel taskObjectViewModel);
    }
}
