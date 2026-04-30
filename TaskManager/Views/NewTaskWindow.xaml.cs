using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
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
            var newTaskObjectViewModel = currentSectionViewModel.CreateTask();
            DataContext = NewTaskObjectViewModel = newTaskObjectViewModel;

            if (currentSectionViewModel.IsMasterSection || currentSectionViewModel.SelectedTaskViewModel is null)
                addAsChild.IsChecked = addAsChild.IsEnabled = false;
        }

        internal TaskObjectViewModel NewTaskObjectViewModel { get; }

        internal bool AddAsChild => addAsChild.IsChecked == true;

        protected override bool ValidateOK()
        {
            string name = taskName.Text;
            return Helper.CheckSaveTaskName(name);
        }

        internal void OpenEditDescription() => taskProperty.OpenEditDescription();
    }
}
