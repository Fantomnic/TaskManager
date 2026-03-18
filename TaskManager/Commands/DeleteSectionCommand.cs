using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;

namespace TaskManager.Commands
{
    public class DeleteSectionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // TODO: Добавить уведомление?
            if (parameter is not TabItem tabItem)
                return;

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.sections.Items.Remove(tabItem);

            if (Helper.GetSectionFromTabItem(tabItem) is Section section)
                mainWindow.RemoveSection(section);
        }
    }
}
