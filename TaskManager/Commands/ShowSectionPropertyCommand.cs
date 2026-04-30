using TaskManager.ViewModels;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class ShowSectionPropertyCommand : BaseCommand
    {
        // Параметр легче задать в коде, а при реализации через xaml придётся добавлять свойство у vm, которое возвращает this
        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not SectionViewModel sectionViewModel)
                return;

            var windowProperty = new SectionPropertyWindow(sectionViewModel);

            windowProperty.ShowDialog();
        }
    }
}
