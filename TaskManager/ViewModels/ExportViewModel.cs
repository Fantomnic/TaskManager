using System.Collections.ObjectModel;
using TaskManager.Helpers;

namespace TaskManager.ViewModels
{
    internal class ExportViewModel : BaseViewModel
    {
        public ObservableCollection<TaskObjectViewModel> TasksViewModels { get; private set; } = [];

        internal void RefreshTasksViewModels(IEnumerable<SectionViewModel> sectionViewModels)
        {
            var allTasksViewModels = Helper.MasterSectionViewModel.AllTasksViewModels;
            var newCollection = new List<TaskObjectViewModel>();

            if (sectionViewModels.Any(s => s.IsMasterSection))
            {
                newCollection = allTasksViewModels;
            }
            else
            {
                foreach (var sectionViewModel in sectionViewModels)
                    newCollection.AddRange(allTasksViewModels.Where(t => t.AdditionalSectionViewModel == sectionViewModel));

                newCollection = [.. newCollection.Distinct()];
            }

            var sortedCollection = Helper.GetFilteredTaskViewModels(newCollection);

            SetNewTasksTasksViewModels(sortedCollection);
        }

        private void SetNewTasksTasksViewModels(List<TaskObjectViewModel> newTasksViewModels)
        {
            TasksViewModels.Clear();

            foreach (var taskViewModel in newTasksViewModels)
                TasksViewModels.Add(taskViewModel);
        }
    }
}
