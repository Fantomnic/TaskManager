using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления неосновного раздела</summary>
    internal class AdditionalSectionViewModel(Section section) : SectionViewModel(section)
    {
        public ObservableCollection<TaskObjectViewModel> RootTasksViewModels { get; } = [];

        internal override void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            var newTask = newTaskViewModel.TaskObject;

            // TODO: убрать логику проверки внутрь
            if (Section.Tasks.Contains(newTask))
                return;

            Helper.MasterSectionViewModel.AddTaskViewModel(newTaskViewModel);

            Section.AddTask(newTask);

            // Если добавляем из основного раздела
            if (newTask.Parent is null)
                RootTasksViewModels.Add(newTaskViewModel);
        }

        internal override bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel)
        {
            return false;
            //taskViewModel ??= FindTaskViewModel(taskObject);

            //return Section.RemoveTask(taskObject)
            //    && taskViewModel is not null
            //    && TasksViewModels.Remove(taskViewModel);
        }

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject)
        {
            throw new NotImplementedException();
        }

        //internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => TasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
