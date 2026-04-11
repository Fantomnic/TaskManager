using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class OpenSettingsCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var settingsWindow = new SettingsWindow();

            if (settingsWindow.ShowDialog() != true)
                return;

            Settings.FillFromViewModel(settingsWindow.SettingsViewModel);
        }
    }
}
