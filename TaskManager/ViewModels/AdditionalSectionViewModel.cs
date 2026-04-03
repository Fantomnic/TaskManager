using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModels
{
    /// <summary>Модель представления неосновного раздела</summary>
    internal class AdditionalSectionViewModel(Section section) : SectionViewModel(section)
    {
        public ObservableCollection<TaskObjectViewModel> RootTasksViewModels { get; } = [];

        internal override void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            Logger.ExecuteWithTryCatch(() =>
            {
                var newTask = newTaskViewModel.TaskObject;

                Section.AddTask(newTask);

                Helper.MasterSectionViewModel.AddTaskViewModel(newTaskViewModel);

                newTaskViewModel.AdditionalSectionViewModel = this;

                // Если добавляем из основного раздела
                if (newTask.Parent is null)
                    RootTasksViewModels.Add(newTaskViewModel);
            });
        }

        internal override bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel)
        {
            return false;
            //taskViewModel ??= FindTaskViewModel(taskObject);

            //return Section.RemoveTask(taskObject)
            //    && taskViewModel is not null
            //    && TasksViewModels.Remove(taskViewModel);
        }

        internal bool RemoveRootTaskViewModel(TaskObjectViewModel taskViewModel) => RootTasksViewModels.Remove(taskViewModel);

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject)
        {
            throw new NotImplementedException();
        }

        //internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => TasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
