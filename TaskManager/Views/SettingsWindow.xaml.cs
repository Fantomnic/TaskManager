using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : WindowWithBottomButtons
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = SettingsViewModel = new();

            if (SettingsViewModel.SetDefaultSectionName != true)
                SetDefaultSectionNameUnchecked();

            if (SettingsViewModel.SetDefaultTaskName != true)
                SetDefaultTaskNameUnchecked();

            UIHelper.SetFocus(cancelButton);
        }

        internal SettingsViewModel SettingsViewModel { get; }

        private void SetDefaultSectionNameChecked(object sender, RoutedEventArgs e)
        {
            incrementSectionName.IsEnabled = true;
            sectionName.IsReadOnly = false;
        }

        private void SetDefaultSectionNameUnchecked(object sender, RoutedEventArgs e) => SetDefaultSectionNameUnchecked();

        private void SetDefaultSectionNameUnchecked()
        {
            SettingsViewModel.IncrementSectionName = incrementSectionName.IsEnabled = false;
            sectionName.IsReadOnly = true;
        }

        private void SetDefaultTaskNameChecked(object sender, RoutedEventArgs e)
        {
            incrementTaskName.IsEnabled = true;
            taskName.IsReadOnly = false;
        }

        private void SetDefaultTaskNameUnchecked(object sender, RoutedEventArgs e) => SetDefaultTaskNameUnchecked();

        private void SetDefaultTaskNameUnchecked()
        {
            SettingsViewModel.IncrementTaskName = incrementTaskName.IsEnabled = false;
            taskName.IsReadOnly = true;
        }

        private void ResetToDefaultClick(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault();

            DialogResult = null;
            Close();
        }
    }
}
