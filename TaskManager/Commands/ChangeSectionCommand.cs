using TaskManager.Model;
using TaskManager.View;

namespace TaskManager.Commands
{
    public class ChangeSectionCommand : BaseCommand
    {
        // Прим.: Обработка доступности команды реализована через событие открытия контекстного меню, т.к.
        // для элементов списка повторно не вызывается CanExecute, если объект уже в фокусе

        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObject taskObject)
                return;

            var taskSectionManagerWindow = new ChangeSectionWindow(taskObject);

            if (taskSectionManagerWindow.ShowDialog() != true)
                return;

            taskObject.MoveToSection(taskSectionManagerWindow.NewSection);
        }
    }
}
