using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.Commands
{
    public class DeleteSectionCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            // TODO: Добавить уведомление?
            if (parameter is not TabItem tabItem || Helper.GetSectionFromTabItem(tabItem) is not Section section)
                return;

            var mainWindow = UIHelper.MainWindow;
                
            if (mainWindow.MainViewModel.RemoveSection(section))
                mainWindow.sections.Items.Remove(tabItem);
        }
    }
}
