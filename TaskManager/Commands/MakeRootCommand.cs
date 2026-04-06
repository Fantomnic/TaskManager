using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Helpers;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class MakeRootCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel || taskObjectViewModel.ParentViewModel is not TaskObjectViewModel parentViewModel)
                return;

            parentViewModel.RemoveChildViewModel(taskObjectViewModel);

            var currentSectionViewModel = Helper.MainViewModel.SelectedSectionViewModel;
            currentSectionViewModel.AddTaskViewModel(taskObjectViewModel);
        }
    }
}
