using TaskManager.Helpers;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class NewTaskCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var currentSectionViewModel = Helper.MainViewModel.SelectedSectionViewModel;

            var newTaskWindow = new NewTaskWindow(currentSectionViewModel);
            newTaskWindow.OpenEditDescription();

            if (newTaskWindow.ShowDialog() == true)
            {
                var newTaskViewModel = newTaskWindow.NewTaskObjectViewModel;
                currentSectionViewModel.AddTaskViewModel(newTaskViewModel);

                // Проверка на null в конструкторе NewTaskWindow
                if (newTaskWindow.AddAsChild)
                    currentSectionViewModel.SelectedTaskViewModel!.AddChildViewModel(newTaskViewModel);

                currentSectionViewModel.SelectedTaskViewModel = newTaskViewModel;
            }

            UIHelper.MainWindow.ResetMenuButtonsFocus();

            // Прим.: Получение элемента списка из объекта другого типа
            //var taskItem = (ListBoxItem)tasksList.ItemContainerGenerator.ContainerFromItem(tasksList.Items[0]);
        }
    }
}
