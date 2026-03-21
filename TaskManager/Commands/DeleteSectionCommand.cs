using System.Windows;
using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;

namespace TaskManager.Commands
{
    public class DeleteSectionCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            // TODO: Добавить уведомление?
            if (parameter is not TabItem tabItem)
                return;

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.sections.Items.Remove(tabItem);

            if (Helper.GetSectionFromTabItem(tabItem) is Section section)
                mainWindow.MainViewModel.RemoveSection(section);
        }
    }
}
