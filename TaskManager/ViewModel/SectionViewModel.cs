using System.Windows;
using TaskManager.Model;

namespace TaskManager.ViewModel
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
                if (_selectedTaskViewModel == value)
                    return;

                // Нужно для снятия фокуса при щелчке по пустому месту
                if (value is null)
                    SetTaskIsSelected(false);

                _selectedTaskViewModel = value;

                // Нужно для установки фокуса при добавлении задачи
                SetTaskIsSelected(true);

                SetVisibilityEmptyTaskImage();
                OnPropertyChanged(nameof(SelectedTaskViewModel));
            }
        }

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
        internal static TaskObjectViewModel CreateTask(string? name = null)
        {
            var newTask = name is null ? Section.CreateTask() : Section.CreateTask(name);
            return new(newTask);
        }

        private void SetVisibilityEmptyTaskImage()
            => VisibilityEmptyTaskImage = SelectedTaskViewModel is null ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>Добавить задачу в раздел (с соответствующей моделью представления)</summary>
        internal abstract void AddTaskViewModel(TaskObjectViewModel newTaskViewModel);

        /// <summary>Удалить задачу из раздела (с соответствующей моделью представления)</summary>
        internal abstract bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel);

        internal abstract TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject);
    }
}
