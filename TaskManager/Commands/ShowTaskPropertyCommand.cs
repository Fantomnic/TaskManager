using TaskManager.ViewModels;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class ShowTaskPropertyCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel)
                return;

            var windowProperty = new TaskPropertyWindow(taskObjectViewModel);

            windowProperty.ShowDialog();
        }
    }
}
