using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public class DeleteSectionCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            // TODO: Добавить уведомление?
            if (parameter is not TabItem tabItem)
                return;

            var mainWindow = UIHelper.MainWindow;
            mainWindow.sections.Items.Remove(tabItem);

            if (Helper.GetSectionViewModelFromTabItem(tabItem) is SectionViewModel sectionViewModel)
                mainWindow.MainViewModel.DeleteSection(sectionViewModel);
        }
    }
}
