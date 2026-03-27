using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public class ShowSectionPropertyCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not AdditionalSectionViewModel sectionViewModel)
                return;

            var windowProperty = new SectionPropertyWindow(sectionViewModel);

            windowProperty.ShowDialog();
        }
    }
}
