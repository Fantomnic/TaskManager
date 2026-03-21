using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : WindowWithBottomButtons
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Owner = Helper.MainWindow;
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
            => SettingsViewModel.IncrementTaskName = taskName.IsEnabled = taskName.IsEnabled = false;
    }
}
