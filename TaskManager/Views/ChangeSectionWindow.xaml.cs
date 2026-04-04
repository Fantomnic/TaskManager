using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for ChangeSectionWindow.xaml
    /// </summary>
    public partial class ChangeSectionWindow : WindowWithBottomButtons
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        internal ChangeSectionWindow(TaskObjectViewModel taskObjectViewModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            InitializeComponent();

            DataContext = taskObjectViewModel;

            var mainViewModel = Helper.MainViewModel;
            
            var availableSections = mainViewModel.GetSectionsViewModelsForChanging(taskObjectViewModel.TaskObject);
            bool hasSectionsForChanging = availableSections.Count > 0;
            bool isAdditionalSection = !Helper.IsMasterSection(mainViewModel.SelectedSectionViewModel.Section);

            if (hasSectionsForChanging)
                newSectionsList.ItemsSource = availableSections;
            
            changeButton.IsEnabled = hasSectionsForChanging;
            deleteButton.IsChecked = !hasSectionsForChanging && isAdditionalSection;
            deleteButton.IsEnabled = isAdditionalSection;
        }

        internal SectionViewModel NewSectionViewModel;

        protected override void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            bool checkedChanging = changeButton.IsChecked == true;
            var newSectionViewModelFromList = newSectionsList.SelectedItem as AdditionalSectionViewModel;

            if (!ValidateNewSection())
                return;

#pragma warning disable CS8601 // Possible null reference assignment.
            NewSectionViewModel = checkedChanging ? newSectionViewModelFromList : Helper.MainViewModel.MasterSectionViewModel;
#pragma warning restore CS8601 // Possible null reference assignment.
            DialogResult = true;
            Close();

            bool ValidateNewSection()
            {
                if (checkedChanging && newSectionViewModelFromList is null)
                {
                    MessageBox.Show("Укажите новый раздел");
                    return false;
                }

                return true;
            }
        }
    }
}
