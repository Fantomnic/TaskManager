using System.Collections.ObjectModel;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModels
{
    /// <summary>Модель представления неосновного раздела</summary>
    internal class AdditionalSectionViewModel(Section section) : SectionViewModel(section)
    {
        internal List<TaskObjectViewModel> AllRootTasksViewModels { get; } = [];

        public ObservableCollection<TaskObjectViewModel> VisibleRootTasksViewModels { get; } = [];

        internal override void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        {
            var newTask = newTaskViewModel.TaskObject;

            Section.AddTask(newTask);

            Helper.MasterSectionViewModel.AddTaskViewModel(newTaskViewModel);

            newTaskViewModel.SetAdditionalSectionViewModel(this);

            if (newTaskViewModel.ParentViewModel is null)
                AllRootTasksViewModels.Add(newTaskViewModel);

            foreach (var childViewModel in newTaskViewModel.ChildrenViewModels)
                AddTaskViewModel(childViewModel);

            RefreshVisibleTaskViewModels();
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

            if (result = AllRootTasksViewModels.Remove(taskViewModel) && removeFromSection)
                taskViewModel.SetAdditionalSectionViewModel(null);

            RefreshVisibleTaskViewModels();

            return result;
        }

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject)
        {
            throw new NotImplementedException();
        }

        internal override void RefreshVisibleTaskViewModels()
        {
            var currentTask = SelectedTaskViewModel?.TaskObject;
            TaskObjectViewModel? rootTaskViewModel = null;

            // Задаём, если выбранная задача есть, но она не корневая
#pragma warning disable CS8604 // Possible null reference argument.
            if (currentTask is not null && !VisibleRootTasksViewModels.Contains(SelectedTaskViewModel))
                rootTaskViewModel = SelectedTaskViewModel.GetRootTaskViewModel();
#pragma warning restore CS8604 // Possible null reference argument.

            var newCollection = GetFilteredTaskViewModels(AllRootTasksViewModels);

            VisibleRootTasksViewModels.Clear();

            foreach (var taskViewModel in newCollection)
                VisibleRootTasksViewModels.Add(taskViewModel);

            if (currentTask is null)
                return;

            if (rootTaskViewModel is null)
                SelectedTaskViewModel = VisibleRootTasksViewModels.FirstOrDefault(vm => vm.TaskObject == currentTask);
            else if (VisibleRootTasksViewModels.Contains(rootTaskViewModel))
                SelectedTaskViewModel = rootTaskViewModel.GetChildTaskViewModel(currentTask);
        }

        //internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => TasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
