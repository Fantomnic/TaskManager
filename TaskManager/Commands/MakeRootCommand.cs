using TaskManager.Helpers;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class MakeRootCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel)
                return;

            var currentSectionViewModel = Helper.MainViewModel.SelectedSectionViewModel;

            MakeRootCore(currentSectionViewModel, taskObjectViewModel);
            currentSectionViewModel.SelectedTaskViewModel = taskObjectViewModel;
        }

        internal static void MakeRootCore(SectionViewModel sectionViewModel, TaskObjectViewModel taskObjectViewModel)
        {
            if (taskObjectViewModel.ParentViewModel is not TaskObjectViewModel parentViewModel)
                return;

            parentViewModel.RemoveChildViewModel(taskObjectViewModel);
            sectionViewModel.AddTaskViewModel(taskObjectViewModel);
        }
    }
}
