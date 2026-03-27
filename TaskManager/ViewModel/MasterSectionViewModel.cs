using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class MasterSectionViewModel(Section section) : SectionViewModel(section)
    {
        /// <summary>Список моделей представления всех задач</summary>
        public ObservableCollection<TaskObjectViewModel> AllTasksViewModels { get; } = [];

        internal override void AddTask(TaskObject newTask, TaskObjectViewModel? newTaskViewModel = null)
        {
            if (Section.Tasks.Contains(newTask))
                return;

            Section.AddTask(newTask);

            newTaskViewModel ??= new TaskObjectViewModel(newTask);
            AddTaskViewModel(newTaskViewModel);
        }

        private void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            AllTasksViewModels.Add(newTaskViewModel);
        }

        internal override bool RemoveTask(TaskObject taskObject, TaskObjectViewModel? taskViewModel = null)
        {
            taskViewModel ??= FindTaskViewModel(taskObject);

            return Section.RemoveTask(taskObject)
                && taskViewModel is not null
                && AllTasksViewModels.Remove(taskViewModel);
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

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => AllTasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
