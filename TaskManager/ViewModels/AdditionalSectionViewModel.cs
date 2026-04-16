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
            var newTask = newTaskViewModel.TaskObject;

            Section.AddTask(newTask);

            Helper.MasterSectionViewModel.AddTaskViewModel(newTaskViewModel);

            newTaskViewModel.SetAdditionalSectionViewModel(this);

            if (newTaskViewModel.ParentViewModel is null)
                RootTasksViewModels.Add(newTaskViewModel);

            foreach (var childViewModel in newTaskViewModel.ChildrenViewModels)
                AddTaskViewModel(childViewModel);
        }

        internal override bool RemoveTaskViewModel(TaskObjectViewModel taskViewModel, bool removeChildren = false)
        {
            if (!Section.RemoveTask(taskViewModel.TaskObject))
                return false;

            if (removeChildren)
            {
                foreach (var child in taskViewModel.ChildrenViewModels)
                    RemoveTaskViewModel(child, true);
            }

            if (!RemoveRootTaskViewModel(taskViewModel))
                taskViewModel.SetAdditionalSectionViewModel(null);

            return true;
        }

        internal bool RemoveRootTaskViewModel(TaskObjectViewModel taskViewModel, bool removeFromSection = true)
        {
            bool result;

            if (result = RootTasksViewModels.Remove(taskViewModel) && removeFromSection)
                taskViewModel.SetAdditionalSectionViewModel(null);

            return result;
        }

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject)
        {
            throw new NotImplementedException();
        }

        //internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => TasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
