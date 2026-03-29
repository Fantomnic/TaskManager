using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.View;

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

            if (parameter is ListBox tasksList)
                tasksList.SelectedItem = newTaskViewModel;

            // Прим.: Получение элемента списка из объекта другого типа
            //var taskItem = (ListBoxItem)tasksList.ItemContainerGenerator.ContainerFromItem(tasksList.Items[0]);
        }
    }
}
