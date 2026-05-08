using TaskManager.Helpers;

namespace TaskManager.Commands
{
    public class ShowHelpCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            
            UIHelper.MainWindow.ResetMenuButtonsFocus();
        }
    }
}
