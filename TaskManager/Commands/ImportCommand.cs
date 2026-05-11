using TaskManager.Helpers;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class ImportCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var importWindow = new ImportWindow();

            if (importWindow.ShowDialog() == true)
                Helper.MasterSectionViewModel.MidnightUpdateTasks();
        }
    }
}
