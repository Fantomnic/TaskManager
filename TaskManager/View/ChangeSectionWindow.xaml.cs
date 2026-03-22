using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for ChangeSectionWindow.xaml
    /// </summary>
    public partial class ChangeSectionWindow : WindowWithBottomButtons
    {
        internal ChangeSectionWindow(TaskObjectViewModel taskObjectViewModel)
        {
            InitializeComponent();

            var mainViewModel = Helper.MainViewModel;
            
            var availableSections = mainViewModel.GetSectionsViewModelsForChanging(taskObjectViewModel.AdditionalSection);
            bool hasSectionsForChanging = availableSections.Count > 0;
            bool isAdditionalSection = !Helper.IsBaseSection(mainViewModel.SelectedSectionViewModel);

            if (hasSectionsForChanging)
                newSectionsList.ItemsSource = availableSections;
            
            changeButton.IsEnabled = hasSectionsForChanging;
            deleteButton.IsChecked = !hasSectionsForChanging && isAdditionalSection;
            deleteButton.IsEnabled = isAdditionalSection;
        }

        internal SectionViewModel? NewSectionViewModel;

        protected override void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            bool checkedChanging = changeButton.IsChecked == true;
            var newSectionViewModelFromList = newSectionsList.SelectedItem as SectionViewModel;

            if (!ValidateNewSection())
                return;

            NewSectionViewModel = checkedChanging ? newSectionViewModelFromList : null;
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
