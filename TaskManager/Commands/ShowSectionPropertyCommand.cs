using TaskManager.Helpers;
using TaskManager.View;

namespace TaskManager.Commands
{
    public class ShowSectionPropertyCommand : BaseCommand
    {
        // Параметр легче задать в коде, а при реализации через xaml придётся добавлять свойство у vm, которое возвращает this
        internal override void ExecuteImplement(object? parameter)
        {
            var sectionViewModel = Helper.MainViewModel.SelectedSectionViewModel;
            var windowProperty = new SectionPropertyWindow(sectionViewModel);

            if (windowProperty.ShowDialog() == true)
                sectionViewModel.Name = windowProperty.NewSectionName;
        }
    }
}
