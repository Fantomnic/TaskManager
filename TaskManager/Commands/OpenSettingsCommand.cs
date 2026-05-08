using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class OpenSettingsCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var settingsWindow = new SettingsWindow();

            if (settingsWindow.ShowDialog() == true)
                Settings.FillFromViewModel(settingsWindow.SettingsViewModel);

            UIHelper.MainWindow.ResetMenuButtonsFocus();
        }
    }
}
