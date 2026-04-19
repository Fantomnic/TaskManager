using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModels
{
    /// <summary>Модель представления основного раздела</summary>
    internal class MasterSectionViewModel : SectionViewModel
    {
        internal MasterSectionViewModel(MasterSection section) : base(section)
        {
            AllTasksViewModels = [.. section.Tasks.Select(CreateTaskViewModel)];
        }

        /// <summary>Список моделей представления всех задач</summary>
        internal List<TaskObjectViewModel> AllTasksViewModels { get; }

        /// <summary>Список отображаемых моделей представления задач (с учётом фильтров)</summary>
        public ObservableCollection<TaskObjectViewModel> VisibleTasksViewModels { get; private set; } = [];

        internal override void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            if (!Section.AddTask(newTaskViewModel.TaskObject))
                return;

            AllTasksViewModels.Add(newTaskViewModel);
            RefreshVisibleTaskViewModels();
        }

        internal override bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel, bool removeChildren = false)
        {
            if (!Section.RemoveTask(taskViewModel.TaskObject))
                return false;

            if (taskViewModel is not null && AllTasksViewModels.Remove(taskViewModel))
                RefreshVisibleTaskViewModels();

            return true;
        }

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => AllTasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);

        /// <summary>Пересчитать видимость команды контекстного меню "Изменить раздел" для указанной задачи</summary>
        internal static void RefreshChangeSectionEnabled(TaskObjectViewModel taskObjectViewModel)
        {
            var mainViewModel = Helper.MainViewModel;
            var availableSections = mainViewModel.GetSectionsViewModelsForChanging(taskObjectViewModel.TaskObject);

            taskObjectViewModel.ChangeSectionEnabled = availableSections.Count > 0;
        }

        internal override void RefreshVisibleTaskViewModels()
        {
            var currentTask = SelectedTaskViewModel?.TaskObject;
            var newCollection = GetFilteredTaskViewModels(AllTasksViewModels);

            VisibleTasksViewModels.Clear();

            foreach (var taskViewModel in newCollection)
                VisibleTasksViewModels.Add(taskViewModel);

            SelectedTaskViewModel = VisibleTasksViewModels.FirstOrDefault(vm => vm.TaskObject == currentTask);
        }
    }
}
