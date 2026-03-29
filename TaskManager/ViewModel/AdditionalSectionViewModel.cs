using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления неосновного раздела</summary>
    internal class AdditionalSectionViewModel(Section section) : SectionViewModel(section)
    {
        public ObservableCollection<TaskObjectViewModel> RootTasksViewModels { get; } = [];

        internal override void AddTask(TaskObject newTask, TaskObjectViewModel? newTaskViewModel = null)
        {
            if (Section.Tasks.Contains(newTask))
                return;

            Helper.MasterSectionViewModel.AddTask(newTask, newTaskViewModel);

            Section.AddTask(newTask);

            newTaskViewModel ??= new TaskObjectViewModel(newTask);

            if (newTask.Parent is null)
                AddTaskViewModel(newTaskViewModel);
        }

        private void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            RootTasksViewModels.Add(newTaskViewModel);
        }

        internal override bool RemoveTask(TaskObject taskObject, TaskObjectViewModel? taskViewModel = null)
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
