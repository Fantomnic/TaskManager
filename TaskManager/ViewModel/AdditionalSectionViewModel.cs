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
            //AddTaskViewModel(newTaskViewModel);
        }

        //private void AddTaskViewModel(TaskObjectViewModel newTaskViewModel)
        //{
        //    if (!IsMasterSection)
        //    {
        //        if (!Helper.GetAllTasksViewModels().Contains(newTaskViewModel))
        //            Helper.MasterSectionViewModel.AddTaskViewModel(newTaskViewModel);

        //        newTaskViewModel.AdditionalSection = this;
        //    }

        //    TasksViewModels.Add(newTaskViewModel);

        //    if (newTaskViewModel.ParentViewModel is null)
        //        RootTasksViewModels.Add(newTaskViewModel);
        //}

        internal override bool RemoveTask(TaskObject taskObject, TaskObjectViewModel? taskViewModel = null)
        {
            return false;
            //taskViewModel ??= FindTaskViewModel(taskObject);

            //return Section.RemoveTask(taskObject)
            //    && taskViewModel is not null
            //    && TasksViewModels.Remove(taskViewModel);
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

        internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject)
        {
            throw new NotImplementedException();
        }

        //internal override TaskObjectViewModel? FindTaskViewModel(TaskObject taskObject) => TasksViewModels.FirstOrDefault(vm => vm.TaskObject == taskObject);
    }
}
