using TaskManager.Views;

namespace TaskManager.Commands
{
    public class ExportCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var exportWindow = new ExportWindow();
            exportWindow.ShowDialog();
        }
    }
}
