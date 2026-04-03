using System.Windows;
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
        }

        internal SettingsViewModel SettingsViewModel { get; }

        private void SetDefaultSectionNameChecked(object sender, RoutedEventArgs e)
            => incrementSectionName.IsEnabled = sectionName.IsEnabled = true;

        private void SetDefaultSectionNameUnchecked(object sender, RoutedEventArgs e)
            => SettingsViewModel.IncrementSectionName = incrementSectionName.IsEnabled = sectionName.IsEnabled = false;

        private void SetDefaultTaskNameChecked(object sender, RoutedEventArgs e)
            => incrementTaskName.IsEnabled = taskName.IsEnabled = true;

        private void SetDefaultTaskNameUnchecked(object sender, RoutedEventArgs e)
            => SettingsViewModel.IncrementTaskName = incrementTaskName.IsEnabled = taskName.IsEnabled = false;
    }
}
