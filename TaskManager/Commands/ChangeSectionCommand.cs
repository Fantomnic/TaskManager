using TaskManager.Views;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class ChangeSectionCommand : BaseCommand
    {
        // Прим.: Обработка доступности команды реализована через событие открытия контекстного меню, т.к.
        // для элементов списка повторно не вызывается CanExecute, если объект уже в фокусе

        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel)
                return;

            var changeSectionWindow = new ChangeSectionWindow(taskObjectViewModel);

            if (changeSectionWindow.ShowDialog() != true)
                return;

            taskObjectViewModel.MoveToSection(changeSectionWindow.NewSectionViewModel, changeSectionWindow.TransferFullChain);
        }
    }
}
