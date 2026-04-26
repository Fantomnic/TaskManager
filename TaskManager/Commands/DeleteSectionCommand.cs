using System.IO;
using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Resources;
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

            if (Settings.ConfirmDeleteSection && !UIHelper.ShowMessage($"Удалить раздел \"{sectionViewModel.Name}\"?", System.Windows.MessageBoxImage.Question))
                return;

            var mainWindow = UIHelper.MainWindow;

            if (mainWindow.MainViewModel.RemoveSectionViewModel(sectionViewModel))
            {
                mainWindow.sections.Items.Remove(tabItem);

                string targetDirectory = Helper.GetDataDirectory(Enums.DataDirectory.Root);
                string fileName = Path.Combine(targetDirectory, sectionViewModel.Section.Guid + Constants.DataExtension);

                File.Delete(fileName);
            }
        }
    }
}
