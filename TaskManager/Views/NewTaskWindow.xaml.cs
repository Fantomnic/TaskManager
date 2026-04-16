using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : WindowWithBottomButtons
    {
        internal NewTaskWindow(SectionViewModel currentSectionViewModel)
        {
            InitializeComponent();
            var newTaskObjectViewModel = SectionViewModel.CreateTask();
            DataContext = NewTaskObjectViewModel = newTaskObjectViewModel;

            if (currentSectionViewModel.IsMasterSection || currentSectionViewModel.SelectedTaskViewModel is null)
                addAsChild.IsChecked = addAsChild.IsEnabled = false;
        }

        internal TaskObjectViewModel NewTaskObjectViewModel { get; }

        internal bool AddAsChild => addAsChild.IsChecked == true;

        protected override bool ValidateOK()
        {
            string name = taskName.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                UIHelper.ShowMessage("Наименование задачи не может быть пустым", MessageBoxImage.Warning);
                return false;
            }

            if (Helper.GetAllTasks().Select(t => t.Name).Contains(name))
            {
                UIHelper.ShowMessage($"Задача с наименованием \"{name}\" уже существует", MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        internal void OpenEditDescription() => taskProperty.OpenEditDescription();
    }
}
