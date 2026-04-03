using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class NewTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var newTaskWindow = new NewTaskWindow();
            newTaskWindow.OpenEditDescription();

            if (newTaskWindow.ShowDialog() != true)
                return;

            var newTaskViewModel = newTaskWindow.NewTaskObjectViewModel;

            var currentSection = Helper.MainViewModel.SelectedSectionViewModel;
            currentSection.AddTaskViewModel(newTaskViewModel);

            currentSection.SelectedTaskViewModel = newTaskViewModel;

            // Прим.: Получение элемента списка из объекта другого типа
            //var taskItem = (ListBoxItem)tasksList.ItemContainerGenerator.ContainerFromItem(tasksList.Items[0]);
        }
    }
}
