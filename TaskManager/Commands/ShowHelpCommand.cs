using System.Diagnostics;
using TaskManager.Helpers;

namespace TaskManager.Commands
{
    public class ShowHelpCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var processStartInfo = new ProcessStartInfo("Resources\\Планировщик задач – Справка.pdf");
            processStartInfo.UseShellExecute = true;

            Process.Start(processStartInfo);

            UIHelper.MainWindow.ResetMenuButtonsFocus();
        }
    }
}
