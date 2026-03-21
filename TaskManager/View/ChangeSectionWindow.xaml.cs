using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for ChangeSectionWindow.xaml
    /// </summary>
    public partial class ChangeSectionWindow : WindowWithBottomButtons
    {
        private MainViewModel _mainViewModel;

        // TODO: Добавить обработку из основного раздела
        internal ChangeSectionWindow(List<Section> availableSections)
        {
            InitializeComponent();

            _mainViewModel = Helper.MainViewModel;
            Owner = Helper.MainWindow;
            bool hasSectionsForChanging = availableSections.Count > 0;
            bool isAdditionalSection = !Helper.IsBaseSection(_mainViewModel.SelectedSection);

            if (hasSectionsForChanging)
                newSectionsList.ItemsSource = availableSections;
            
            changeButton.IsEnabled = hasSectionsForChanging;
            deleteButton.IsChecked = !hasSectionsForChanging && isAdditionalSection;
            deleteButton.IsEnabled = isAdditionalSection;
        }

        internal Section? NewSection;

        protected override void ButtonOKClick(object sender, RoutedEventArgs e)
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
    }
}
