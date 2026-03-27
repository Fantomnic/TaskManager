using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления неосновного раздела</summary>
    internal class SectionViewModel(Section section) : TemplateSectionViewModel(section)
    {
        public ObservableCollection<TaskObjectViewModel> RootTasksViewModels { get; } = [];

        /// <summary>Список моделей представления задач раздела</summary>
        public ObservableCollection<TaskObjectViewModel> TasksViewModels { get; } = [];

        /// <summary>Добавить задачу в раздел (с соответствующими моделями представления)</summary>
        internal void AddTask(TaskObject newTask, TaskObjectViewModel? newTaskViewModel = null)
        {
            Section.AddTask(newTask);

            newTaskViewModel ??= new TaskObjectViewModel(newTask);
            AddTaskViewModel(newTaskViewModel);
        }

        private void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            if (!IsMasterSection)
            {
                if (!Helper.GetAllTasksViewModels().Contains(newTaskViewModel))
                    Helper.BaseSectionViewModel.AddTaskViewModel(newTaskViewModel);

                newTaskViewModel.AdditionalSection = this;
            }

            TasksViewModels.Add(newTaskViewModel);

            if (newTaskViewModel.ParentViewModel is null)
                RootTasksViewModels.Add(newTaskViewModel);
        }

        /// <summary>Удалить задачу из раздела (с соответствующей моделью представления)</summary>
        internal bool RemoveTask(TaskObject taskObject, TaskObjectViewModel? taskViewModel = null)
        {
            taskViewModel ??= FindTaskViewModel(taskObject);

            return Section.RemoveTask(taskObject)
                && taskViewModel is not null
                && TasksViewModels.Remove(taskViewModel);
        }

        // TODO: Переделать
        internal override void RefreshChangeSectionEnabled(TaskObjectViewModel taskObjectViewModel)
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
