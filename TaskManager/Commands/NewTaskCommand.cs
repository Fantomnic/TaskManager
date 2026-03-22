using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    public class NewTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var newTaskViewModel = new TaskObjectViewModel();

            var newTaskWindow = new NewTaskWindow(newTaskViewModel);
            newTaskWindow.OpenEditDescription();

            if (newTaskWindow.ShowDialog() != true)
                return;

            var currentSection = Helper.MainViewModel.SelectedSectionViewModel;
            currentSection.AddTask(newTaskViewModel);

            if (parameter is ListBox tasksList)
                tasksList.SelectedItem = newTaskViewModel;

            // Прим.: Получение элемента списка из объекта другого типа
            //var taskItem = (ListBoxItem)tasksList.ItemContainerGenerator.ContainerFromItem(tasksList.Items[0]);
        }
    }
}
