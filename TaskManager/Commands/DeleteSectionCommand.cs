using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class DeleteSectionCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            // TODO: Добавить уведомление?
            if (parameter is not SectionViewModel sectionViewModel || UIHelper.GetTabItemWithSectionViewModel(sectionViewModel) is not TabItem tabItem)
                return;

            var mainWindow = UIHelper.MainWindow;

            if (mainWindow.MainViewModel.RemoveSectionViewModel(sectionViewModel))
                mainWindow.sections.Items.Remove(tabItem);
        }
    }
}
