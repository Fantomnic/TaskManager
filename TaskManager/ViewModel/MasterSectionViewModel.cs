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

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => AllTasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);

        /// <summary>Пересчитать видимость команды контекстного меню "Изменить раздел" для указанной задачи</summary>
        internal static void RefreshChangeSectionEnabled(TaskObjectViewModel taskObjectViewModel)
        {
            var mainViewModel = Helper.MainViewModel;
            var availableSections = mainViewModel.GetSectionsViewModelsForChanging(taskObjectViewModel.TaskObject);

            taskObjectViewModel.ChangeSectionEnabled = availableSections.Count > 0;
        }
    }
}
