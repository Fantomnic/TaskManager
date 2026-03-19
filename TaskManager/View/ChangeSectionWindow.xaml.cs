using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for ChangeSectionWindow.xaml
    /// </summary>
    public partial class ChangeSectionWindow : Window
    {
        private MainViewModel _mainViewModel;

        // TODO: Добавить обработку из основного раздела
        internal ChangeSectionWindow(Section currentSection, List<Section> availableSections)
        {
            InitializeComponent();

            _mainViewModel = Helper.MainViewModel;
            bool hasSectionsForChanging = availableSections.Count > 0;

            if (hasSectionsForChanging)
                newSectionsList.ItemsSource = availableSections;
            
            changeButton.IsEnabled = hasSectionsForChanging;
            deleteButton.IsChecked = deleteButton.IsEnabled = !Helper.IsBaseSection(currentSection);
        }

        internal Section? NewSection;

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            bool checkedChanging = changeButton.IsChecked == true;
            var newSectionFromList = newSectionsList.SelectedItem as Section;

            if (!ValidateNewSection())
                return;

            NewSection = checkedChanging ? newSectionFromList : null;
            DialogResult = true;
            Close();

            bool ValidateNewSection()
            {
                if (checkedChanging && newSectionFromList is null)
                {
                    MessageBox.Show("Укажите новый раздел");
                    return false;
                }

                return true;
            }
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
