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
            if (parameter is not TabItem tabItem || Helper.GetSectionViewModelFromTabItem(tabItem) is not SectionViewModel sectionViewModel)
                return;

            var mainWindow = UIHelper.MainWindow;
                
            if (mainWindow.MainViewModel.RemoveSectionViewModel(sectionViewModel))
                mainWindow.sections.Items.Remove(tabItem);
        }
    }
}
