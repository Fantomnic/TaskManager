using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModels
{
    /// <summary>Модель представления основного раздела</summary>
    internal class MasterSectionViewModel(Section section) : SectionViewModel(section)
    {
        /// <summary>Список моделей представления всех задач</summary>
        public ObservableCollection<TaskObjectViewModel> AllTasksViewModels { get; } = [];

        internal override void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            if (Section.AddTask(newTaskViewModel.TaskObject))
                AllTasksViewModels.Add(newTaskViewModel);
        }

        internal override bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel, bool removeChildren = false)
            => Section.RemoveTask(taskViewModel.TaskObject)
                && taskViewModel is not null
                && AllTasksViewModels.Remove(taskViewModel);

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
